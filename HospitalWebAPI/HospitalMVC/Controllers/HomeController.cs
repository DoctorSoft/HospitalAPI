using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HospitalMVC.Controllers
{
    public class HomeController : Controller
    {
        private const string RedirectControllerName = "LogInRedirect";
        private const string RedirectActionName = "RedirectToMainPage";

        // GET: Home
        public ActionResult Index(Guid? Token)
        {
            return RedirectToAction(RedirectActionName, RedirectControllerName, new { Token = Token });
        }
    }
}