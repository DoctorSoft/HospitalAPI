using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalMVC.Filters;

namespace HospitalMVC.Controllers
{
    public class ClinicUserHomePageController : Controller
    {
        // GET: ClinicUserHomePage
        [TokenAuthorizationFilter]
        public ActionResult Index(Guid token)
        {
            //TODO: Change Guid token to command
            return View();
        }
    }
}