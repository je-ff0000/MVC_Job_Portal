using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Project_Job_Portal.Models
{
    public class CompanyInsert
    {
        [Required(ErrorMessage = "*Company Name Required")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "*Enter valid email id")]
        [Required(ErrorMessage = "*Company Email Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Username Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*Password Required")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password mismatch")]
        public string ComparePassword { get; set; }

        [Required(ErrorMessage = "*Phone Number Required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "*Website Required")]
        public string Website { get; set; }

        [Required(ErrorMessage = "*Description Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "*Location Required")]
        public string Location { get; set; }
        public string Message { get; set; }

    }
}