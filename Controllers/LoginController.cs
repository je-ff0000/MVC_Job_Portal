using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project_Job_Portal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginPageLoad()
        {
            return View();
        }

        public ActionResult EmployeeHome()
        {
            return View();
        }

        public ActionResult CompanyHome()
        {
            return View();
        }
    }
}