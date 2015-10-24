using BookByText.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookByText.Controllers
{
    public class SmsController : ApiController
    {
        private readonly IBookService bookService;
        private readonly ISmsService smsService;

        public SmsController()
        {
            bookService = MvcApplication.BookService;
            smsService = MvcApplication.SmsService;
        }

        [HttpGet]
        [Route("sms")]
        public IHttpActionResult Get()
        {
            return Ok("Working.");
        }

        public IHttpActionResult Post()
        {
            SmsModel model = null;
            var command = model.content.ToLower().Trim();
            var text = "";

            switch(command)
            {
                case "next":
                    text = bookService.GetNext(model.from.ToString());
                    smsService.Send(model.from.ToString(), text);
                    return Ok();
                case "previous":
                    text = bookService.GetPrevious(model.from.ToString());
                    smsService.Send(model.from.ToString(), text);
                    return Ok();
                default:
                    return BadRequest();
            }
        }
    }
}
