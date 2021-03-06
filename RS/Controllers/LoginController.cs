﻿using RS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace RS.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users U)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (UserValidator.IsValid(U.email, U.password))
            {
                FormsAuthentication.SetAuthCookie(U.email, U.RememberMe);
                FormsAuthentication.RedirectFromLoginPage(U.email, false);
            }
            else
            {
                ModelState.AddModelError("", "Login data is incorrect!");
            }
            return View(U);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}