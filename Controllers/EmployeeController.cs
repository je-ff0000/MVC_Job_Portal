using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Project_Job_Portal.Models;

namespace MVC_Project_Job_Portal.Controllers
{
    public class EmployeeController : Controller
    {
        MVC_Project_Job_PortalEntities dbobj = new MVC_Project_Job_PortalEntities();
        // GET: EmployeeReg
        public ActionResult InsertEmployee_PageLoad()
        {
            EmployeeInsert emp = new EmployeeInsert();
            if (TempData["Message"] != null)
            {
                emp.Message = TempData["Message"].ToString();
            }
            emp.MyQual = getQualificationData();
            emp.DOB = DateTime.Today;
            return View(emp);
        }

        public List<CheckBoxListHelper> getQualificationData()
        {
            List<CheckBoxListHelper> quals = new List<CheckBoxListHelper>()
            {
                new CheckBoxListHelper{Value = "Higher Secondary", Text="Higher Secondary", isChecked=false},
                new CheckBoxListHelper{Value = "B.Tech", Text="B.Tech", isChecked=false},
                new CheckBoxListHelper{Value = "BCA", Text="BCA", isChecked=false},
                new CheckBoxListHelper{Value = "MCA", Text="MCA", isChecked=false},
                new CheckBoxListHelper{Value = "PhD", Text="PhD", isChecked=false},
            };

            return quals;
        }

        public ActionResult InsertEmployee_Click(EmployeeInsert clsobj, HttpPostedFileBase profileFile, HttpPostedFileBase resumeFile, FormCollection form)
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

                    if (profileFile != null && profileFile.ContentLength > 0)
                    {
                        string extension = Path.GetExtension(profileFile.FileName);
                        string filename = newId + "_Profile" + extension;
                        string folder = Server.MapPath("~/Photos");
                        string path = Path.Combine(folder, filename);
                        profileFile.SaveAs(path);
                        clsobj.ProfileImage = Path.Combine("~\\Photos", filename);
                    }

                    if(resumeFile != null && resumeFile.ContentLength > 0)
                    {
                        string extension = Path.GetExtension(resumeFile.FileName);
                        string filename = newId + "_Resume" + extension;
                        string folder = Server.MapPath("~/Resumes");
                        string path = Path.Combine(folder, filename);
                        resumeFile.SaveAs(path);
                        clsobj.ResumePath = Path.Combine("~\\Resumes", filename);
                    }

                    clsobj.Qual = clsobj.selectedQual != null
                                    ? string.Join(",", clsobj.selectedQual)
                                    : "";
                    clsobj.MyQual = getQualificationData();

                    dbobj.sp_InsertEmployee(newId, clsobj.FirstName, clsobj.LastName, clsobj.Age, clsobj.Address, clsobj.Email, clsobj.Phone, clsobj.Gender, clsobj.DOB, clsobj.Qual, clsobj.Skills, clsobj.ResumePath, clsobj.ProfileImage);
                    dbobj.sp_Login(newId, clsobj.Username, clsobj.Password, "Employee");
                    clsobj.Message = "Successfully Inserted";

                    var employee = dbobj.Employee_Tab
                                   .FirstOrDefault(e => e.EmployeeId == newId);
                    var login = dbobj.Login_Tab
                                   .FirstOrDefault(e => e.RegId == newId);
                    

                    if (employee != null && login != null)
                    {
                        TempData["Message"] = "Employee Registered Successfully.";
                        return RedirectToAction("InsertEmployee_PageLoad");
                    }
                    else
                    {
                        clsobj.Message = "Registration failed.";
                        clsobj.MyQual = getQualificationData();
                        return View("InsertEmployee_PageLoad", clsobj);
                    }
                }
                catch(Exception ex)
                {
                    clsobj.Message = ex.Message;
                    clsobj.MyQual = getQualificationData();
                    return View("InsertEmployee_PageLoad", clsobj);
                }
            }
            clsobj.MyQual = getQualificationData();
            return View("InsertEmployee_PageLoad", clsobj);
        }
    }
}