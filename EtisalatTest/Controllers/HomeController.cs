using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtisalatTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string name)
        {
            return View("Index", new {Name = name}.ToExpando());
        }

        //
        // GET: /Home/

        [HttpPost]
        public ActionResult Record(string name, string wifiOrData)
        {
            var db = new WriteOnlyDB(System.Web.HttpContext.Current);

            db.AddVisit(new Visit(name, "wifi" == wifiOrData, DateTime.Now,Request.UserHostAddress, Utils.ToHeaders(Request.Headers) ));

            return View("ThankYou", new {Name = name}.ToExpando());
        }
    }
}
