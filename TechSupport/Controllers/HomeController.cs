using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechSupport.BLL.Interfaces;
using TechSupport.Utils;

namespace TechSupport.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfService DB;
        public HomeController(IUnitOfService db)
        {
            this.DB = db;
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //Test1 t1 = new Test1();
            //t1.Update();
           // var t = DB.RequestService.GetList();
            var p = DB.EmployeeService.GetEmployees();

            return View();
        }
    }
}
