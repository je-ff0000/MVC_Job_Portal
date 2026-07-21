using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Project_Job_Portal.Models;

namespace MVC_Project_Job_Portal.Controllers
{
    public class CompanyRegController : Controller
    {
        MVC_Project_Job_PortalEntities dbobj = new MVC_Project_Job_PortalEntities();
        // GET: CompanyReg
        public ActionResult InsertCompany_PageLoad()
        {
            return View();
        }

        public ActionResult InsertCompany_Click(CompanyInsert clsobj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var getmaxid = dbobj.sp_MaxIdLogin().FirstOrDefault();
                    int newId;

                    if (getmaxid == null)
                    {
                        newId = 1001;      
                    }
                    else
                    {
                        newId = (int)getmaxid + 1;
                    }

                    dbobj.sp_InsertCompany(newId, clsobj.Name, clsobj.Email, clsobj.Phone, clsobj.Website, clsobj.Description, clsobj.Location);
                    dbobj.sp_Login(newId, clsobj.Username, clsobj.Password, "Company");
                    clsobj.Message = "Successfully Inserted";

                    var company = dbobj.Company_Tab
                                .FirstOrDefault(c => c.CompanyId == newId);

                    var login = dbobj.Login_Tab
                                  .FirstOrDefault(l => l.RegId == newId);

                    if (company != null && login != null)
                    {
                        clsobj.Message = "Company Registered Successfully.";
                    }
                    else
                    {
                        clsobj.Message = "Registration failed.";
                    }
                }
                catch (Exception ex)
                {
                    clsobj.Message = ex.Message;
                }
                return RedirectToAction("InsertCompany_PageLoad");
            }

            return RedirectToAction("InsertCompany_PageLoad", clsobj);
        }
    }
}