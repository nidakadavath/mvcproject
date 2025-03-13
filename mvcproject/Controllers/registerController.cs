using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;

namespace mvcproject.Controllers
{
    public class registerController : Controller
    {
        // GET: register
        public ActionResult register()
        {
            return View();
        }
    }
}