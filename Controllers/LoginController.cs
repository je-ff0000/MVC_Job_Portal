using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Project_Job_Portal.Models;

namespace MVC_Project_Job_Portal.Controllers
{
    public class LoginController : Controller
    {
        MVC_Project_Job_PortalEntities dbobj = new MVC_Project_Job_PortalEntities();
        // GET: Login
        public ActionResult LoginPageLoad()
        {
            return View();
        }


        public ActionResult LoginClick(UserLogin clsobj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var val = dbobj.sp_LoginCountId(clsobj.Username, clsobj.Password).FirstOrDefault();
                    if(val == 1)
                    {
                        var regid = dbobj.sp_GetRegId(clsobj.Username, clsobj.Password).FirstOrDefault();
                        Session["RegId"] = regid;

                        var logtype = dbobj.sp_LoginType(clsobj.Username, clsobj.Password).FirstOrDefault();
                        if(logtype == "Employee")
                        {
                            return RedirectToAction("EmployeeHome", "Employee");
                        }
                        else if (logtype == "Company")
                        {
                            return RedirectToAction("CompanyHome", "Company");
                        }
                        else
                        {
                            TempData["Message"] = "Can't find user type";
                            return RedirectToAction("LoginPageLoad");
                        }
                    }
                    else
                    {
                        clsobj.Message = "Invalid Username or Password";
                        return View("LoginPageLoad", clsobj);
                    }
                }
                catch(Exception ex)
                {
                    clsobj.Message = ex.Message;
                    return View("LoginPageLoad", clsobj);
                }
            }
            return View("LoginPageLoad", clsobj);
        }
    }
}