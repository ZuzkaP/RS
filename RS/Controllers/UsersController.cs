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
        private MainDB db = new MainDB();
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


            var users = from s in db.Users
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
            var users = from s in db.Users
                        select s;
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(s => s.first_name.Contains(name) || s.last_name.Contains(name));
            }
            return View(users.ToList());
        }

        //[HttpGet]
        //public ActionResult Edit(Users user, string name, string last_name, string phone_number, string email, ICollection<RS.Models.Roles> userRoles)
        //{
        //    List<Users> editUser = db.Users.ToList();
        //    if (!String.IsNullOrEmpty(name))
        //    {
        //        Users edited = new Users { email = email, first_name = name, last_name = last_name, phone_number = phone_number };
        //    }
        //    return View();
        //}

        [HttpPost]
        public ActionResult Edit(Users user, string name, string last_name, string phone_number, string email, ICollection<RS.Models.Roles> userRoles)
        {
            List<Users> editUser = db.Users.ToList();
            if (!String.IsNullOrEmpty(name))
            {
                Users edited = new Users { email = email, first_name = name, last_name = last_name, phone_number = phone_number };
            }
            return View();
        }

        [HttpPost]
        public void Save(Users user)
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
                    using (MainDB db = new MainDB())
                    {
                        UsersRoles usersRoles = new UsersRoles();
                        var count = db.Users.Count(u => u.email == U.email);
                        if (count == 0)
                        {
                            string hash = Helpers.SHA1.Encode(U.password);
                            string hashConfirm = Helpers.SHA1.Encode(U.ConfirmPassword);
                            U.ConfirmPassword = hashConfirm;
                            U.password = hash;

                            db.Users.Add(U);
                            db.SaveChanges();
                            usersRoles.roles_id = POUZIVATEL;

                            usersRoles.user_id = U.user_id;
                            db.UsersRoles.Add(usersRoles);
                            db.SaveChanges();
                            ModelState.Clear();
                            U = null;
                            TempData["registration"] = "You are successfully registered.";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.Message = "User with this email exists";
                            return View();
                        }
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