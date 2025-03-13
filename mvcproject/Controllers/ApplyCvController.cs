using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;

namespace mvcproject.Controllers
{

    public class ApplyCvController : Controller
    {
        mvcprojectEntities dbobj = new mvcprojectEntities();
        // GET: ApplyCv
        public ActionResult ApplyCV_Load(int cid, int jid)
        {
            TempData["cid"] = cid;
            TempData["jid"] = jid;
            return View();
        }

        public ActionResult Insert_click(insertcv clsobj, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    var s = Server.MapPath("~/resume");
                    string pa = Path.Combine(s, fname);
                    file.SaveAs(pa);

                    var fullpath = Path.Combine("~\\resume", fname);
                    clsobj.resume = fullpath; // set
                }
                dbobj.sp_applycv(Convert.ToInt32(TempData["jid"]), Convert.ToInt32(Session["uid"]), Convert.ToInt32(TempData["cid"]), clsobj.resume, DateTime.Now,"applied");
                clsobj.appmsg = "Application Submitted";
                return View("ApplyCV_Load", clsobj);
            }

            return View("ApplyCV_Load", clsobj);


        }
    }
}
