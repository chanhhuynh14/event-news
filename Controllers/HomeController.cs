using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Hutech.Areas.Admin.Models;
using E_Hutech.Models;

namespace E_Hutech.Controllers
{
    public class HomeController : Controller
    {
            public ActionResult Index()
            {
                return View();
            }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetAll()
        {
            return View();
        }
        
    }
}