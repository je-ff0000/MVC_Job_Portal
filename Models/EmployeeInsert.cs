using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Project_Job_Portal.Models
{
    public class CheckBoxListHelper
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool isChecked { get; set; }
    }
    public class EmployeeInsert
    {
        public List<CheckBoxListHelper> MyQual { get; set; }

        [Required(ErrorMessage = "*Required*")]
        public string[] selectedQual { get; set; }
        
        [Required(ErrorMessage = "*Required*")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Required*")]
        public string LastName { get; set; }

        [Range(18, 50, ErrorMessage = "Age between 18 and 50 required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "*Required*")]
        public string Address { get; set; }

        [Required(ErrorMessage = "*Required*")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Required*")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*Required*")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password mismatch")]
        public string ComparePassword { get; set; }

        [Required(ErrorMessage = "*Required*")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "*Required*")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "*Required*")]
        public DateTime DOB { get; set; }

        public string Qual { get; set; }

        [Required(ErrorMessage = "*Required*")]
        public string Skills { get; set; }

        
        public string ResumePath { get; set; }
        public string ProfileImage { get; set; }

        public string Message { get; set; }
    }
}