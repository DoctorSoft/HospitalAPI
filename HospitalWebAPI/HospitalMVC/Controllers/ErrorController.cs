using System.Web.Mvc;

namespace HospitalMVC.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error404()
        {
            return View();
        }
    }
}