using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;

namespace mvcproject.Controllers
{
    public class job_1Controller : Controller
    {
        private mvcprojectEntities db = new mvcprojectEntities();

        // GET: job_1
        public ActionResult job_pageload()
        {
            var jobs = db.job_1.ToList();

            if (jobs == null || !jobs.Any())
            {
                ViewBag.Message = "No job listings found.";
                return View(new List<job_1>()); // Return an empty list to avoid null reference errors.
            }

            return View(jobs);
        }

        // GET: job_1/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            job_1 job = db.job_1.Find(id);

            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }

        // GET: job_1/Create
        public ActionResult Create()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: job_1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "job_title,job_description,salary,location,experience,skills,job_type,job_status,start_date,end_date")] job_1 job)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    job.comp_id = Convert.ToInt32(Session["uid"]); // Ensure comp_id is set
                    db.job_1.Add(job);
                    db.SaveChanges();
                    return RedirectToAction("job_pageload");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "An error occurred while saving the job. Please try again.";
                }
            }

            return View(job);
        }

        // GET: job_1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            job_1 job = db.job_1.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            if (job.comp_id != Convert.ToInt32(Session["uid"]))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Unauthorized action.");
            }

            return View(job);
        }

        // POST: job_1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "job_id,job_title,job_description,salary,location,experience,skills,job_type,job_status,start_date,end_date")] job_1 job)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var existingJob = db.job_1.Find(job.job_id);
            if (existingJob == null)
            {
                return HttpNotFound();
            }

            if (existingJob.comp_id != Convert.ToInt32(Session["uid"]))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Unauthorized action.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    existingJob.job_title = job.job_title;
                    existingJob.job_description = job.job_description;
                    existingJob.salary = job.salary;
                    existingJob.location = job.location;
                    existingJob.experience = job.experience;
                    existingJob.skills = job.skills;
                    existingJob.job_type = job.job_type;
                    existingJob.job_status = job.job_status;
                    existingJob.start_date = job.start_date;
                    existingJob.end_date = job.end_date;

                    db.SaveChanges();
                    return RedirectToAction("job_pageload");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "An error occurred while updating the job.";
                }
            }
            return View(job);
        }

        // GET: job_1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            job_1 job = db.job_1.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            if (job.comp_id != Convert.ToInt32(Session["uid"]))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Unauthorized action.");
            }

            return View(job);
        }

        // POST: job_1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var job = db.job_1.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            if (job.comp_id != Convert.ToInt32(Session["uid"]))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Unauthorized action.");
            }

            try
            {
                db.job_1.Remove(job);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "An error occurred while deleting the job.";
                return View(job);
            }

            return RedirectToAction("job_pageload");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
