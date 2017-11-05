using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class FirstController : Controller
    {
        // GET: First
        public ActionResult Index()
        {
            ViewBag.Number = 8;
            ViewBag.Message = "This is an index page";
            ViewBag.Slarks = new List<string> { "Slark1", "Slark2", "Slark3"};
            return View();
        }

        //public string Index(string id)
        //{
        //    return "This is first controller Index. <br/> Your id is : " + id;
        //}

        public string Another()
        {
            return "This is first contoller another page.";
        }
    }
}