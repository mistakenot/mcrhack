using BookByText.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookByText.Controllers
{
    public class IndexController : Controller
    {
        private readonly IBookService bookService;
        private readonly ISmsService smsService;

        public IndexController()
        {
            bookService = MvcApplication.BookService;
            smsService = MvcApplication.SmsService;
        }

        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        public void Post(SubscriptionModel model)
        {
            if (ModelState.IsValid)
            {
                bookService.CreateSubscription(model.Number, model.BookId);
                var text = bookService.GetNext(model.Number.ToString());
                smsService.Send(model.Number.ToString(), text);
            }
        }
    }
}