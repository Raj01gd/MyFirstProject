﻿using MyFirstProject.Common;
using MyFirstProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MyFirstProject.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string userName, string passWord)
        {
            using (MyFirstProjectDBEntities Db = new MyFirstProjectDBEntities())
            {
                var securePWD = Crypto.SHA1(passWord);
                var isAuthenticated = Db.SECURITY_DB.Any(item => item.FULL_NAME == userName && item.SECURE_PWD == securePWD);
                var userInfo = Db.SECURITY_DB.Where(item => item.FULL_NAME == userName).FirstOrDefault();

                if (isAuthenticated)
                {
                    var userModel = new UserModel()
                    {
                        UserName = userInfo.FULL_NAME,
                        Role = userInfo.ROLE.ROLE_NAME
                    };
                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    string userData = serializer.Serialize(userModel);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                      userName,
                      DateTime.Now,
                      DateTime.Now.AddMinutes(30),
                      false,
                      userData,
                      FormsAuthentication.FormsCookiePath);

                    // Encrypt the ticket.
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    Response.BufferOutput = true;
                    // Create the cookie.
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                    // Redirect back to original URL.
                    return Redirect(FormsAuthentication.GetRedirectUrl(userName, false));
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
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
                newUser.ROLE_ID = 3;
                Db.SECURITY_DB.Add(newUser);
                Db.SaveChanges();
                return RedirectToAction("Login");
            }

        }
    }
}