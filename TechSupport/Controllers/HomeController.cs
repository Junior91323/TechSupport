using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechSupport.Utils;

namespace TechSupport.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //Test1 t1 = new Test1();
            //t1.Update();
            return View();
        }
    }
}
