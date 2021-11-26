using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Manager;
using WebApp.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;

namespace WebApp.Controllers
{
    public class VisitorController : Controller
    {
        private IDataBaseControler _dataManager;
        public VisitorController(IDataBaseControler IDataBaseControler)
        {
            _dataManager = IDataBaseControler;
        }
        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.visitorList = await _dataManager.GetItems<Visitor>("selectAllVisitor");
            return View();
        }

        [Route("Visitor/AddVisitor")]
        public IActionResult AddVisitor()
        {
            return View();
        }

        public async Task<IActionResult> EditVisitor(string visitorId)
        {
            Dictionary<string, string> myParams = new Dictionary<string, string>();
            myParams.Add("visitorId", visitorId);

            var visitor = await _dataManager.GetItems<Visitor>("selectVisitor", myParams);
            return View(visitor.First());
        }

        public async Task<IActionResult> SaveVisitor(Visitor visitor)
        {
            Dictionary<string, string> MyParams = new Dictionary<string, string>();
            MyParams.Add("name", visitor.Name);
            MyParams.Add("surname", visitor.Surname);
            MyParams.Add("lastname", visitor.LastName);
            MyParams.Add("birthday", visitor.Birthday);
            MyParams.Add("sex", visitor.Sex);
            MyParams.Add("phone", visitor.Phone);
            MyParams.Add("adress", visitor.Adress);

            await _dataManager.SendCommand("createVisitor", MyParams);

            return LocalRedirect($"~/Visitor/AddVisitor");
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistic()
        {
            var fileStreamResult = await GenerateFileStatistic();
            return fileStreamResult;
        }

        public async Task<FileStreamResult> GenerateFileStatistic()
        {
            var count = await _dataManager.GetItems<Statistic>("getStatistic");
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 18);
            //Тут он решил отказаться от возможности печати на кирилице и домутпен ток инглишьььььь
            string str = $"Count of visitors in the system: {count.First().Count}";
            graphics.DrawString(str, font, PdfBrushes.Black, new PointF(0, 0));
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
            fileStreamResult.FileDownloadName = "statistic.pdf";

            return fileStreamResult;
        }
    }
}
