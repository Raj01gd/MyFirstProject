using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstProject.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter User Name")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        public string Password { get; set; }
    }
}