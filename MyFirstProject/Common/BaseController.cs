using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstProject
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Check if this action has NotAuthorizeAttribute
            object[] attributes = filterContext.ActionDescriptor.GetCustomAttributes(true);
            if (attributes.Any(a => a is AllowAnonymousAttribute)) return;

            else
            {
                Response.Redirect("~/Account/Login");
            }
        }
    }
}