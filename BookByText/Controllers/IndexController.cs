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
        private readonly IBookService service;

        public IndexController()
        {

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
                service.CreateSubscription(model.Number, model.BookId);
            }
        }
    }
}