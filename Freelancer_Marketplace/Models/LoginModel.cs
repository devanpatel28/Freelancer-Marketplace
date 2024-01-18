using Freelancer_Marketplace.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Freelancer_Marketplace.Models
{
    public class LoginModel
    {
        public Boolean ChackLogin(LoginViewModel user_data)
        {
            if (user_data != null)
            {
                using (var db = new Freelancer_Marketplace_DBEntities())
                {
                    var user = db.usersdatas.FirstOrDefault(x => x.username == user_data.Username && x.userpassword == user_data.Userpass);
                    if (user != null)
                    {
                        // Add user details to session
                        HttpContext.Current.Session["UserId"] = user.userid;
                        HttpContext.Current.Session["Username"] = user.username;

                        return true;
                    }

                }
            }
            return false;
        }
    }
}