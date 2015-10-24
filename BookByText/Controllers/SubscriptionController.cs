using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace BookByText.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly IBookService bookService;
        private readonly ISmsService smsService;

        public SubscriptionController()
        {
            this.bookService = MvcApplication.BookService;
            this.smsService = MvcApplication.SmsService;
        }

        public ActionResult Create()
        {
            var id = Request["Book"];
            var number = Request["Number"];
            
            bookService.CreateSubscription(number, int.Parse(id));
            var next = bookService.GetNext(number);
            smsService.Send(number, next);

            return Redirect("");
        }
    }
}
