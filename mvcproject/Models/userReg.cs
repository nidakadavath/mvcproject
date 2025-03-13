using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcproject.Models
{
    public class userReg
    {
        public int uid { set; get; }

        [Required(ErrorMessage = "Enter the name")]
        public string name { set; get; }

        [Range(18, 50, ErrorMessage = "Enter the age")]
        public int age { set; get; }

        [Required(ErrorMessage = "Enter the address")]
        public string address { set; get; }

        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter valid number")]
        public long phone { get; set; }

        [EmailAddress(ErrorMessage = "Enter valid email id")]
        public string email { set; get; }

        [Required(ErrorMessage = "Gender is required")]
        public string gen { get; set; }
        public string qualification { get; set; }

        [Required(ErrorMessage = "Enter the experience")]
        public string exp { get; set; }
        [Required(ErrorMessage = "Enter the skills")]
        public string skills{ get; set; }

        public string resume { set; get; }
        public string photo { set; get; }

        [Required(ErrorMessage = "Enter the current location")]
        public string locat { set; get; }
        public string username { set; get; }
        public string pass { set; get; }

        [Compare("pass", ErrorMessage = "Password mismatch")]
        public string cpassword { set; get; }

        public string usermsg { set; get; }

    }
}