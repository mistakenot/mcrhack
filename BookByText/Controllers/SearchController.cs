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
    public class SearchController : Controller
    {
        private readonly IBookService bookService;

        public SearchController()
        {
            bookService = MvcApplication.BookService;
        }

        public ActionResult Index()
        {
            var keywords = Request["query"] ?? "";
            var result = bookService
                .Search(keywords)
                .Select(kv => 
                    new BookModel()
                    {
                        Name = kv.Value,
                        Id = kv.Key
                    }
                );

            return View(result);
        }
    }
}
