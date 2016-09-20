using System;
using System.Web.Mvc;

namespace HospitalMVC.Controllers
{
    public class HomeController : Controller
    {
        private const string RedirectControllerName = "LogInRedirect";
        private const string RedirectActionName = "RedirectToMainPage";

        // GET: Home
        public ActionResult Index(Guid? token)
        {
            //Mock
            return Redirect("https://www.youtube.com/watch?v=d-diB65scQU");

            return RedirectToAction(RedirectActionName, RedirectControllerName, new { Token = token });
        }
    }
}