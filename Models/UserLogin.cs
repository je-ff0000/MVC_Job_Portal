using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project_Job_Portal.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Enter username")]
        public string Username { set; get; }

        [Required(ErrorMessage = "Enter password")]
        public string Password { set; get; }
        public string Message { set; get; }
        public string UserType { set; get; }
    }
}