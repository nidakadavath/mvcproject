using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;
namespace mvcproject.Controllers
{
    public class userInsertController : Controller
    {
        // GET: userInsert
        mvcprojectEntities dbobj = new mvcprojectEntities();
        public ActionResult user_pageload()
        {
            return View();
        }
        public ActionResult Insert_click(userReg clsobj, HttpPostedFileBase file, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    var s = Server.MapPath("~/photos");
                    string pa = Path.Combine(s, fname);
                    file.SaveAs(pa);

                    var fullpath = Path.Combine("~\\photos", fname);
                    clsobj.photo = fullpath; // set
                }
                if (file1 != null && file1.ContentLength > 0)
                {
                    string[] allowedResumeExtensions = { ".pdf", ".doc", ".docx" };
                    string fileExt = Path.GetExtension(file1.FileName).ToLower();

                    if (allowedResumeExtensions.Contains(fileExt))
                    {
                        string fname = Path.GetFileName(file1.FileName);
                        string savePath = Path.Combine(Server.MapPath("~/resume"), fname);
                        file1.SaveAs(savePath);
                        clsobj.resume = "/resume/" + fname;
                    }
                    else
                    {
                        ModelState.AddModelError("resume", "Only PDF, DOC, and DOCX files are allowed.");
                        return View("user_pageload", clsobj);
                    }
                    var getmaxid = dbobj.sp_MaxIdLogin().FirstOrDefault();
                    int mid = Convert.ToInt32(getmaxid);
                    int regid = 0;
                    if (mid == 0)
                    {
                        regid = 1;
                    }
                    else
                    {
                        regid = mid + 1;
                    }

                    // get
                    dbobj.sp_userreg(regid, clsobj.name, clsobj.age, clsobj.address, clsobj.phone, clsobj.email, clsobj.gen, clsobj.qualification, clsobj.exp, clsobj.skills, clsobj.resume, clsobj.photo, clsobj.locat);
                    dbobj.sp_loginsert(regid, clsobj.username, clsobj.pass, "user");

                    clsobj.usermsg = "successfully inserted";
                    return View("user_pageload", clsobj);

                }

              
            }
            return View("user_pageload", clsobj);



        }
    }
}