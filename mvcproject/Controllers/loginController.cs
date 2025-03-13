using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;

namespace mvcproject.Controllers
{
    public class LoginController : Controller 
    {
        // Database context
        mvcprojectEntities dbobj = new mvcprojectEntities();

        public ActionResult login_pageload()
        {
            return View();
        }

        public ActionResult UserHome()
        {
            return View();
        }

        public ActionResult job_pageload()
        {
            return View("job_pageload");
 
        }

        public ActionResult Login_Click(login objcls)
        {
            if (ModelState.IsValid)
            {
                var val = dbobj.sp_loginCountId(objcls.Uname, objcls.password).SingleOrDefault();

                if (val == 1)
                {
                    var uid = dbobj.sp_loginId(objcls.Uname, objcls.password).FirstOrDefault();
                    Session["uid"] = uid;
                    var lt = dbobj.sp_loginType(objcls.Uname, objcls.password).FirstOrDefault();

                    if (lt == "user")
                    {
                        return RedirectToAction("UserHome");
                    }
                    else if (lt == "admin")
                    {
                        return RedirectToAction("job_pageload", "job_1");
                    }
                }

                ModelState.AddModelError("", "Invalid login credentials"); 
            }
            return View("login_pageload", objcls);
        }
    }
}
