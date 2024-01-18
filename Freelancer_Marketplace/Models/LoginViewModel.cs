using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Freelancer_Marketplace.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Pls enter user name")]
        public string Username { get; set; }

        public string Userpass { get; set; }
        public string ErrorMsg { get; set; }
    }
}