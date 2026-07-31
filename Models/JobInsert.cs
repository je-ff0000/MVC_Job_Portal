using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project_Job_Portal.Models
{
    public class JobInsert
    {
        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string JobDescription { get; set; }

        [Required]
        public string SkillsRequired { get; set; }

        [Required]
        public int ExpRequired { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Select Job Type")]
        public string JobType { get; set; } //dropdown

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime LastDate { get; set; }
        public string Message { get; set; }
    }
}