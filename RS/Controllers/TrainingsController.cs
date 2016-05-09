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
using RS.DAO;
using RS.Service;
using System.Web.Security;
using System.Xml;

namespace RS.Controllers
{
    public class TrainingsController : Controller
    {
        public TrainingsController()
        {
            BeanResolver.ResolveBeansForClass(this);
        }

        [Bean(id = "trainingDao")]
        public TrainingDao trainingDao { get; private set; }

        [Bean(id = "usersDao")]
        public UsersDao usersDao { get; private set; }

        [HttpGet]
        public ActionResult Calendar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.Roles = usersDao.GetRoleForUser(User.Identity.Name);
            }
            return View(trainingDao.GetTrainings());
        }

        [HttpGet]
        public JsonResult All()
        {
            var entity = trainingDao.GetRawTrainings().
              Select(e => new
              {
                  e.time,
                  e.kapacita,
                  e.Users.first_name
              });
            var result = entity.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Create(Trainings training)
        {
            trainingDao.CreateTraining(training, usersDao.GetUserByEmail(User.Identity.Name));
            FormsAuthentication.RedirectFromLoginPage(User.Identity.Name, false);
        }
    }
}