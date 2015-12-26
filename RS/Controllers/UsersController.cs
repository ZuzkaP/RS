using RS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RS.Controllers
{
    
    public class UserController : Controller
    {
        public object RegisterSuccessfull { get; private set; }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users U)
        {
            if (ModelState.IsValid)
            {
                using (Database1Entities db = new Database1Entities())
                {
                    var count = db.Users.Count(u => u.email == U.email);
                    if (count == 0)
                    {
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
            return View(U);
        }

    }
}