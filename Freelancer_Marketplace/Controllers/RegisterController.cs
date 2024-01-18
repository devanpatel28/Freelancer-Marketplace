using Freelancer_Marketplace.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Freelancer_Marketplace.Entity;

namespace Freelancer_Marketplace.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Register(int id=0)
        {
            usersdata usermodel = new usersdata();
            return View(usermodel);
        }
    }
}