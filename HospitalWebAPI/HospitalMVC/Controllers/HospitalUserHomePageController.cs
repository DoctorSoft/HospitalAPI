using System;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;

namespace HospitalMVC.Controllers
{
    public class HospitalUserHomePageController : Controller
    {
        // GET: HospitalUserHomePage
        [TokenAuthorizationFilter(FunctionIdentityName.EditEmptyPlacesByHospital)]
        public ActionResult Index(Guid token)
        {
            //TODO: Change Guid token to command
            return View();
        }
    }
}