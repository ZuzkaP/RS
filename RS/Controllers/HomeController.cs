
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Speranza reservation system";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "O nás";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontakt na nás";
            return View();
        }
    }
}