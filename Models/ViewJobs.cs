using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Project_Job_Portal.Models
{
    public class ViewJobs
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string Location { get; set; }
        public decimal Salary { get; set; }
        public DateTime LastDate { get; set; }
        public string JobType { get; set; }
    }
}