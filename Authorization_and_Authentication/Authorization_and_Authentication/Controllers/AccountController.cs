using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Authorization_and_Authentication.Models;

namespace Authorization_and_Authentication.Controllers
{
    [AllowAnonymous]                // now this page can be accessible to user.
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using(var context = new EmployeeEnterpriseEntities())
            {
                bool isValid = context.Users.Any(x => x.UserName ==  user.UserName && x.Password == user.Password);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);  // Set false, if you dont want to save Username
                    return RedirectToAction("Index", "Employee");
                }

                ModelState.AddModelError("", "Invalid Username and Password");
            }
            return View();
        }

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            using (var context = new EmployeeEnterpriseEntities())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            return RedirectToAction("login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");
        }
    }
}