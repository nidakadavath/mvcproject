using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using mvcproject.Models; // Replace with your actual namespace

public class JobController : Controller
{
     mvcprojectEntities db = new mvcprojectEntities(); 

    public ActionResult JobList()
    {
        JobSearch model = new JobSearch();
        model.selectjob = db.GetActiveJobs().Select(job => new job_1
        {
            job_title = job.job_title,  // Match the correct property names
            location = job.location,
            salary = job.salary,
             job_description = job.job_description,
            experience=job.experience,
            skills=job.skills,
            job_type=job.job_type,
            _job_status=job.job_status,
            start_date=job.start_date,
            end_date=job.end_date

        }).ToList();

        return View(model);
    }
    public ActionResult Searchjob_click(JobSearch clsobj)
    {
        string qry = "";

        if (!string.IsNullOrWhiteSpace(clsobj.insertse.experience))
        {
            qry += " and Experience like '%" + clsobj.insertse.experience + "%'";
        }

        if (!string.IsNullOrWhiteSpace(clsobj.insertse.skills))
        {
            qry += " and Skills like '%" + clsobj.insertse.skills + "%'";
        }
        if (!string.IsNullOrWhiteSpace(clsobj.insertse.location))
        {
            qry += " and location like '%" + clsobj.insertse.location + "%'";
        }

        return View("JobList", getdata1(clsobj, qry));
    }
    private JobSearch getdata1(JobSearch clsobj, string qry)
    {
        using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["test"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_jobsearches", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@qry", qry);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            var joblist = new JobSearch();
            while (dr.Read())
            {
                var jobcls = new job_1();
                jobcls.comp_id = Convert.ToInt32(dr["Comp_Id"].ToString());
                jobcls.skills = dr["skills"].ToString();
                jobcls.experience = dr["experience"].ToString();
                jobcls._job_status = dr["job_status"].ToString();
                jobcls.location = dr["location"].ToString();
                jobcls.end_date = Convert.ToDateTime(dr["end_date"]);

                joblist.selectjob.Add(jobcls);
            }
            con.Close();
            return joblist;
        }
    }


}