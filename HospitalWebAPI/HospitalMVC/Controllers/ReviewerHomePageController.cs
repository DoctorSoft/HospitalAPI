using System;
using System.Web.Mvc;
using HospitalMVC.Filters;

namespace HospitalMVC.Controllers
{
    public class ReviewerHomePageController : Controller
    {
        // GET: ReviewerHomePage
        [TokenAuthorizationFilter]
        public ActionResult Index(Guid token)
        {
            //TODO: Change Guid token to command
            return View();
        }
    }
}