using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Project_Job_Portal.Models;

namespace MVC_Project_Job_Portal.Controllers
{
    public class CompanyController : Controller
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

        public ActionResult CompanyHome()
        {
            int regId = Convert.ToInt32(Session["RegId"]);
            var companyName = dbobj.sp_GetCompanyName(regId).FirstOrDefault();
            ViewBag.CompanyName = companyName;
            return View();
        }

        public List<SelectListItem> GetJobTypes()
        {
            List<SelectListItem> jobtypes = new List<SelectListItem>
            {
                new SelectListItem {Text = "--Select Job Type", Value=""},
                new SelectListItem {Text = "Full Time", Value = "Full Time"},
                new SelectListItem {Text = "Part Time", Value = "Part Time"},
                new SelectListItem {Text = "Internship", Value = "Internship"},
                new SelectListItem {Text = "Contract", Value = "Contract"}
            };

            return jobtypes;
        }
        public ActionResult InsertJob_PageLoad()
        {
            JobInsert job = new JobInsert();
            if(TempData["Message"] != null)
            {
                job.Message = TempData["Message"].ToString();
            }
            job.LastDate = DateTime.Today;
            ViewBag.JobTypes = GetJobTypes();
            return View(job);
        }
        
        public ActionResult InsertJob_Click(JobInsert clsobj)
        {
            int companyId = Convert.ToInt32(Session["RegId"]);

            if (ModelState.IsValid)
            {
                try
                {   
                    dbobj.sp_InsertJob(companyId, clsobj.JobTitle, clsobj.JobDescription, clsobj.SkillsRequired, clsobj.ExpRequired, clsobj.Qualification, clsobj.Salary, clsobj.JobType, clsobj.Location, clsobj.LastDate);
                    TempData["Message"] = "Job posted successfully";
                    return RedirectToAction("InsertJob_PageLoad");
                }
                catch(Exception ex)
                {
                    ViewBag.JobTypes = GetJobTypes();
                    TempData["Message"] = ex.Message;
                    return View("InsertJob_PageLoad", clsobj);
                }

            }
            return View("InsertJob_PageLoad", clsobj);
        }

        public ActionResult ViewJobs_PageLoad()
        {
            int CompanyId = Convert.ToInt32(Session["RegId"]);
            var jobs = dbobj.sp_ViewPostedJobs(CompanyId).ToList();

            if(jobs.Count == 0)
            {
                ViewBag.Message = "No Posted Jobs";
            }

            return View(jobs);
        }

    }
}