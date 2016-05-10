using RS.Core;
using RS.DAO;
using RS.Models;
using RS.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RS.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public object RegisterSuccessfull { get; private set; }

        public UserController()
        {
            BeanResolver.ResolveBeansForClass(this);
        }

        [Bean(id = "usersDao")]
        public UsersDao usersDao { get; private set; }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Maintanance(string sortOrder, string first_name)
        {
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Meno:" : "";
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Priezvisko:" : "";

            return View(usersDao.GetUsersByName(sortOrder, first_name));
        }
        
        [HttpPost]
        public ActionResult Maintanance(string name)
        {
            return View(usersDao.GetUsersByFirstOrLastName(name));
        }

        [HttpPost]
        public void Save(Users user, List<Models.Roles> roles)
        {
            usersDao.EditUser(user, roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users U)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!usersDao.ExistsUser(U.email))
                    {
                        usersDao.SaveUser(U);
                        ModelState.Clear();

                        TempData["registration"] = "You are successfully registered.";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "User with this email exists";
                        return View();
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
            }
            return View(U);
        }

    }
}