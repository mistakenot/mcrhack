using BookByText.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace BookByText.Controllers
{
    public class SmsController : Controller
    {
        private readonly IBookService bookService;
        private readonly ISmsService smsService;

        public SmsController()
        {
            bookService = MvcApplication.BookService;
            smsService = MvcApplication.SmsService;
        }

        public ActionResult Receive()
        {
            SmsModel model = new SmsModel()
            {
                content = Request["content"],
                from = long.Parse(Request["from"]),
                to = long.Parse(Request["to"])
            };
            var command = model.content.ToLower().Trim();
            var text = "";

            switch(command)
            {
                case "next":
                    text = bookService.GetNext(model.from.ToString());
                    smsService.Send(model.from.ToString(), text);
                    return Json(new object());
                case "previous":
                    text = bookService.GetPrevious(model.from.ToString());
                    smsService.Send(model.from.ToString(), text);
                    return Json(new object());
                default:
                    throw new Exception("Invalid message.");
            }
        }
    }
}
