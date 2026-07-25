using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project_Job_Portal.Models
{
    public class JobInsert
    {
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string SkillsRequired { get; set; }
        public int ExpRequired { get; set; }
        public string Qualification { get; set; }
        public decimal Salary { get; set; }
        public string JobType { get; set; } //dropdown
        public string Location { get; set; }
        public DateTime LastDate { get; set; }
        public DateTime PostedDate { get; set; }

    }
}