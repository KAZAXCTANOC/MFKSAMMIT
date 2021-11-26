using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Manager;
using WebApp.Models;    
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IDataBaseControler _dataManager;
        public HomeController(ILogger<HomeController> logger, IDataBaseControler IDataBaseControler)
        {
            _dataManager = IDataBaseControler;
            _logger = logger;
        }

        public IActionResult Index()
        {
            StatusViewModel statusList = new StatusViewModel();
            ViewBag.statusList = new SelectList(statusList.StatusList, "Name", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Event MyEvent)
        {
            Dictionary<string, string> myParams = new Dictionary<string, string>();

            myParams.Add("name", MyEvent.Name);
            myParams.Add("date", MyEvent.Date);
            myParams.Add("description", MyEvent.Description);
            myParams.Add("status", MyEvent.Status);

            await _dataManager.SendCommand("createEvent", myParams);

            return Index();
        }

        public IActionResult Edit(int id)
        {
            return LocalRedirect($"~/Event/EventEdit/{id}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
