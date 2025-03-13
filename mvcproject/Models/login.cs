using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcproject.Models
{
    public class login
    {

        [Required(ErrorMessage = "Enter the user name")]
        public string Uname { set; get; }

        [Required(ErrorMessage = "Enter the password")]
        public string password { set; get; }


        public string ltype { set; get; }
        public string msg { set; get; }

    }
}