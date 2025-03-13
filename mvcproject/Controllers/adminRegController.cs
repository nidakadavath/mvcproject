using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;

namespace mvcproject.Controllers
{
    public class adminRegController : Controller
    {
        // GET: adminReg
        mvcprojectEntities dbobj = new mvcprojectEntities();
        public ActionResult compageload()
        {
            return View();
        }
        public ActionResult Insert_click(comp clsobj, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    var s = Server.MapPath("~/comp");
                    string pa = Path.Combine(s, fname);
                    file.SaveAs(pa);

                    var fullpath = Path.Combine("~\\comp", fname);
                    clsobj.photo = fullpath; // set
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
                dbobj.sp_compreg(regid, clsobj.name,  clsobj.address,clsobj.phone, clsobj.email,clsobj.web, clsobj.photo);
                dbobj.sp_loginsert(regid, clsobj.username, clsobj.pass, "admin");

                clsobj.msg = "successfully inserted";
                return View("compageload", clsobj);


            }

            return View("compageload", clsobj);
        }
    }
}