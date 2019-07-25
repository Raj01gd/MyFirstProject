using MyFirstProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MyFirstProject.Common
{
    interface ICustomPrincipal : IPrincipal
    {
        string UserName { get; set; }
        string Role { get; set; }
    }
    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            using (MyFirstProjectDBEntities Db = new MyFirstProjectDBEntities())
            {
                var usersRole = Db.SECURITY_DB.Any(item => item.ROLES == role && item.FULL_NAME == Identity.Name);
                if (usersRole)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public CustomPrincipal(string UserName)
        {
            this.Identity = new GenericIdentity(UserName);
        }

        public string UserName { get; set; }
        public string Role { get; set; }
    }
}