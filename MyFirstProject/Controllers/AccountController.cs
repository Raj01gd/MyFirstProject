using MyFirstProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace MyFirstProject.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public void Login(string userName, string passWord)
        {
            using (MyFirstProjectDBEntities Db = new MyFirstProjectDBEntities())
            {
                var securePWD = Crypto.SHA1(passWord);
                var isAuthenticated = Db.SECURITY_DB.Any(item => item.FULL_NAME == userName && item.SECURE_PWD == securePWD);

                if (isAuthenticated)
                {
                    string userData = "ApplicationSpecific data for this user.";

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                      userName,
                      DateTime.Now,
                      DateTime.Now.AddMinutes(30),
                      false,
                      userData,
                      FormsAuthentication.FormsCookiePath);

                    // Encrypt the ticket.
                    string encTicket = FormsAuthentication.Encrypt(ticket);

                    // Create the cookie.
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                    // Redirect back to original URL.
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(userName, false));
                }
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(MyModel user)
        {
            using (MyFirstProjectDBEntities Db = new MyFirstProjectDBEntities())
            {
                var password = Crypto.SHA1(user.Password);
                var newUser = Db.SECURITY_DB.Create();
                newUser.FULL_NAME = user.userName;
                newUser.SECURE_PWD = password;
                newUser.ROLES = "Admin";
                Db.SECURITY_DB.Add(newUser);
                Db.SaveChanges();
                return RedirectToAction("Login");
            }

        }
    }
}