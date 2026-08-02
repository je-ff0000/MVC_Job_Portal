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
                new SelectListItem {Text = "Hybrid", Value = "Hybrid"},
                new SelectListItem {Text = "Remote", Value = "Remote"}

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
                    clsobj.Message = ex.Message;
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

        public ActionResult EditJob(int id)
        {
            var jobobj = dbobj.JobDetails_Tab.Find(id);
            if(jobobj == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobTypes = GetJobTypes();
            return View(jobobj);
        }

        [HttpPost]
        public ActionResult EditJob_Click(JobDetails_Tab jobobj)
        {
            var job = dbobj.JobDetails_Tab.Find(jobobj.JobId);

            if(job != null)
            {
                job.JobTitle = jobobj.JobTitle;
                job.JobDescription = jobobj.JobDescription;
                job.SkillsRequired = jobobj.SkillsRequired;
                job.ExperienceRequired = jobobj.ExperienceRequired;
                job.Qualification = jobobj.Qualification;
                job.Salary = jobobj.Salary;
                job.JobType = jobobj.JobType;
                job.Location = jobobj.Location;

                dbobj.SaveChanges();
                return RedirectToAction("ViewJobs_PageLoad");
            }
            else
            {
                ModelState.AddModelError("", "Job not found.");
            }

            return View("EditJob_PageLoad", jobobj);
        }

        public ActionResult DeleteJob(int id)
        {
            var jobobj = dbobj.JobDetails_Tab.Find(id);
            if (jobobj == null)
            {
                return HttpNotFound();
            }

            return View(jobobj);
        }

        [HttpPost, ActionName("DeleteJob")]
        public ActionResult DeleteJobConfirmed(int id)
        {
            var jobobj = dbobj.JobDetails_Tab.Find(id);
            dbobj.JobDetails_Tab.Remove(jobobj);
            dbobj.SaveChanges();
            return RedirectToAction("ViewJobs_PageLoad");
        }
    }
}