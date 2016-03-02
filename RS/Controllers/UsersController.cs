﻿using RS.Models;
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
        public ActionResult Maintanance()
        {

            Database1Entities db = null;
            try
            {
                 db = new Database1Entities();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            return View(db.Users.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users U)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (Database1Entities db = new Database1Entities())
                    {

                        var count = db.Users.Count(u => u.email == U.email);
                        if (count == 0)
                        {
                            string hash = Helpers.SHA1.Encode(U.password);
                            string hashConfirm = Helpers.SHA1.Encode(U.ConfirmPassword);
                            U.ConfirmPassword = hashConfirm;
                            U.password = hash;
                           
                          
                            db.Users.Add(U);
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