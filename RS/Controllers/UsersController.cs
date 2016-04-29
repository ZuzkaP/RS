using RS.Core;
using RS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RS.Controllers
{
    public class UserController : Controller
    {
        private static int TRENER = 2;
        private static int POUZIVATEL = 3;
        public object RegisterSuccessfull { get; private set; }

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


            var users = from s in SQL.Instance.Database.Users
                        select s;
            if (!String.IsNullOrEmpty(first_name))
            {
                users = users.Where(s => s.first_name.Contains(first_name));
            }

            switch (sortOrder)
            {
                case "Meno:":
                    users = users.OrderByDescending(s => s.first_name);
                    break;
                case "Priezvisko:":
                    users = users.OrderByDescending(s => s.last_name);
                    break;
                default:
                    users = users.OrderBy(s => s.last_name);
                    break;
            }
            return View(users.ToList());
        }
        
        [HttpPost]
        public ActionResult Maintanance(string name)
        {
            var users = from s in SQL.Instance.Database.Users
                        select s;
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(s => s.first_name.Contains(name) || s.last_name.Contains(name));
            }
            return View(users.ToList());
        }

        [HttpPost]
        public void Save(Users user, List<Models.Roles> roles)
        {
            Console.WriteLine(user.last_name);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users U)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsersRoles usersRoles = new UsersRoles();
                    var count = SQL.Instance.Database.Users.Count(u => u.email == U.email);
                    if (count == 0)
                    {
                        string hash = Helpers.SHA1.Encode(U.password);
                        string hashConfirm = Helpers.SHA1.Encode(U.ConfirmPassword);
                        U.ConfirmPassword = hashConfirm;
                        U.password = hash;

                        SQL.Instance.Database.Users.Add(U);
                        SQL.Instance.Database.SaveChanges();
                        usersRoles.roles_id = POUZIVATEL;

                        usersRoles.user_id = U.user_id;
                        SQL.Instance.Database.UsersRoles.Add(usersRoles);
                        SQL.Instance.Database.SaveChanges();
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