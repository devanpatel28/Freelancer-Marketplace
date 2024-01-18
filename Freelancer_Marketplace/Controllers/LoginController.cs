using Freelancer_Marketplace.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Freelancer_Marketplace.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            LoginViewModel loginModel = new LoginViewModel();
            return View("Login", loginModel);
        }

        [HttpPost]
        public ActionResult Login2(LoginViewModel user_data)
        {
            //user_data.Username
            LoginModel loginModel = new LoginModel();
            if (loginModel.ChackLogin(user_data))
            {
                return Json(new { data= "login Done", isError=false });
            }
            return Json(new { data = "login not Done", isError = true});
        }

    }
}