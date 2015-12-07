using System;
using System.Web.Mvc;
using HospitalMVC.Filters;

namespace HospitalMVC.Controllers
{
    public class HomeController : Controller
    {
        [TokenAuthorizationFilter]
        public ActionResult Index(Guid token)
        {
            return View();
        }
    }
}