using System;
using System.Web.Mvc;
using HospitalMVC.Filters;

namespace HospitalMVC.Controllers
{
    public class HospitalUserHomePageController : Controller
    {
        // GET: HospitalUserHomePage
        [TokenAuthorizationFilter]
        public ActionResult Index(Guid token)
        {
            //TODO: Change Guid token to command
            return View();
        }
    }
}