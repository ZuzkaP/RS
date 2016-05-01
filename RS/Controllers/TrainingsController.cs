using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RS.Core;
using RS.Models;

namespace RS.Controllers
{
    public class TrainingsController : Controller
    {
        private MainDB db = new MainDB();


        // GET: Trainings/Create
        public ActionResult Calendar()
        {
            return View();
        }
    }
}