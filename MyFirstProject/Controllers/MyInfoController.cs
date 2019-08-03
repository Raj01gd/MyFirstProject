using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstProject.Controllers
{
    public class MyInfoController : Controller
    {
        // GET: MyInfo
        public ActionResult Interests()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            else
            {
                return Redirect("~/Home/Contact");
            }
        }
        public ActionResult FavTvSeries()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            else
            {
                return Redirect("~/Home/Contact");
            }
        }
        public ActionResult FavGadjets()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            else
            {
                return Redirect("~/Home/Contact");
            }
        }
    }
}