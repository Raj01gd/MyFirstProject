using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Contact");
            }
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}