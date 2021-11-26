using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Manager;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class EventController : Controller
    {
        private IDataBaseControler _dataManager;
        public EventController(IDataBaseControler IDataBaseControler)
        {
            _dataManager = IDataBaseControler;
        }

        [Route("Event/EventEdit/{Id}")]
        public async Task<IActionResult> EventEdit(string id)
        {
            List<Visitor> visitors = new List<Visitor>();
            Dictionary<string, string> myParams = new Dictionary<string, string>();
            myParams.Add("id", id.ToString());
            var myEvent = await _dataManager.GetItems<Event>("selectEvents", myParams);

            var listParticipantsEvent = await _dataManager.GetItems<ParticipantsEvents>("getParticipantsEvent");
            var PEv = listParticipantsEvent.Where(P => P.Idevent == myEvent.First().Id);

            foreach (var item in PEv)
            {
                myParams = new Dictionary<string, string>();
                myParams.Add("visitorId", item.Idvisitor.ToString());
                var visitor = await _dataManager.GetItems<Visitor>("selectVisitor", myParams);
                if (visitor == null)
                {

                }
                else
                {
                    visitors.Add(visitor.FirstOrDefault());
                }
            }

            StatusViewModel statusList = new StatusViewModel();
            ViewBag.statusList = new SelectList(statusList.StatusList, "Name", "Name");
            ViewBag.ListVisitors = visitors;
            ViewBag.EventId = id;
            return View(myEvent.First());
        }

        public IActionResult Edit(Event Event)
        {
            Dictionary<string, string> myParams = new Dictionary<string, string>();
            myParams.Add("name", Event.Name);
            myParams.Add("date", Event.Date);
            myParams.Add("description", Event.Description);
            myParams.Add("status", Event.Status);
            myParams.Add("id", Event.Id.ToString());

            _dataManager.SendCommand("updateEvent", myParams);
            return LocalRedirect($"~/Home/Index/");
        }
        public async Task<IEnumerable<Event>> getEventListAsync()
        {
            var listParticipantsEvent = await _dataManager.GetItems<ParticipantsEvents>("getParticipantsEvent");
            IEnumerable<Event> eventList = await _dataManager.GetItems<Event>("selectEvents");
            foreach (var item in eventList)
            {
                var PEv = listParticipantsEvent.Where(P => P.Idevent == item.Id);
                if (PEv == null) continue;
                item.ListVisitors = new List<Visitor>();
                foreach (var item2 in PEv)
                {
                    Dictionary<string, string> myParams = new Dictionary<string, string>();
                    myParams.Add("visitorId", item2.Idvisitor.ToString());
                    var visitor = await _dataManager.GetItems<Visitor>("selectVisitor", myParams);
                    if (visitor == null)
                    {
                        item.ListVisitors = null;
                    }
                    else
                    {
                        item.ListVisitors.Add(visitor.FirstOrDefault());
                    }
                }
            }
            return eventList;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.eventList = await getEventListAsync();

            List<SelectListItem> items = new List<SelectListItem>();
            var list = await _dataManager.GetItems<Visitor>("selectAllVisitor");
            foreach (var item in list)
            {
                items.Add(new SelectListItem
                {
                    Text = $"{item.Name} {item.Surname} {item.LastName}",
                    Value = item.Id.ToString()
                });
            }

            
            VisitorViewModel visitorViewModel = new VisitorViewModel { Visitors = items };
            return View(visitorViewModel);
        }

        public async Task<List<SelectListItem>> GetVisitorsAsync(VisitorViewModel visitor, string EventId)
        {
            Dictionary<string, string> myParam = new Dictionary<string, string>();
            for (int i = 0; i < visitor.VisitorsId.Length; i++)
            {
                myParam.Add("idevent", EventId);
                myParam.Add("idvisitor", visitor.VisitorsId[i].ToString());
                await _dataManager.SendCommand("createParticipantsEvent", myParam);
                myParam = new Dictionary<string, string>();
            }
            List<SelectListItem> items = new List<SelectListItem>();
            var list = await _dataManager.GetItems<Visitor>("selectAllVisitor");
            foreach (var item in list)
            {
                items.Add(new SelectListItem
                {
                    Text = $"{item.Name} {item.Surname} {item.LastName}",
                    Value = item.Id.ToString()
                });
            }
            return items;
        }

        [HttpPost]
        public async Task<IActionResult> Index(VisitorViewModel visitor, string EventId)
        {
            visitor.Visitors = await GetVisitorsAsync(visitor, EventId);

            ViewBag.eventList = await getEventListAsync();

            return View(visitor);

        }

        public async Task<IActionResult> Delete(string VisitorId, string EventId)
        {
            Dictionary<string, string> myParams = new Dictionary<string, string>();
            myParams.Add("idevent", EventId);
            myParams.Add("idvisitor", VisitorId);

            await _dataManager.SendCommand("deleteParticipants", myParams);

            return LocalRedirect($"~/Event/EventEdit/{EventId}");
        }

        public async Task<IActionResult> CloseEvent(string eventId)
        {
            Dictionary<string, string> myParams = new Dictionary<string, string>();
            myParams.Add("idevent", eventId);
            await _dataManager.SendCommand("closeEvent", myParams);

            return LocalRedirect("~/Event/Index");
        }

        public async Task<IActionResult> DeleteEvents()
        {
            var emptyEvents = await _dataManager.GetItems<EmptyEvents>("selectEmptyEvents");

            if(emptyEvents == null)
            {
                return LocalRedirect("~/Event/Index");
            }
            else
            {
                for (int i = 0; i < emptyEvents.Count(); i++)
                {
                    Dictionary<string, string> myParams = new Dictionary<string, string>();
                    myParams.Add("idevent", emptyEvents.ElementAt(i).Id.ToString());
                    await _dataManager.SendCommand("deleteEvent", myParams);
                }
                return LocalRedirect("~/Event/Index");
            }
        }
    }
}
