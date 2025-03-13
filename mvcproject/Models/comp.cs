using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcproject.Models
{
    using System.ComponentModel.DataAnnotations;

    public class comp
    {
        public int uid { get; set; }

        [Required(ErrorMessage = "Enter the name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Enter the address")]
        public string address { get; set; }

        [Required(ErrorMessage = "Enter the Phone")]
        [Range(1000000000, 999999999999, ErrorMessage = "Enter a valid phone number")]
        public long phone { get; set; }

        [Required(ErrorMessage = "Enter an email address")]
        [EmailAddress(ErrorMessage = "Enter a valid email ID")]
        public string email { get; set; }

        [Required(ErrorMessage = "Enter a web address")]
        [Url(ErrorMessage = "Enter a valid web address")]
        public string web { get; set; }

        public string photo { get; set; }

      
        public string username { get; set; }

       
        public string pass { get; set; }

        [Compare("pass", ErrorMessage = "Password mismatch")]
        public string cpassword { get; set; }

        public string msg { get; set; }
    }
}