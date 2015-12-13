using System.Web.Mvc;
using System.Web.Routing;
using ServiceModels.ServiceCommands.AuthorizationCommands;
using Services.Interfaces.AuthorizationServices;

namespace HospitalMVC.Controllers
{
    public class LogOutController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public LogOutController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        // GET: LogOut
        public ActionResult Index(LogOutUserByTokenCommand command)
        {
            var answer = _authorizationService.LogOutUserByToken(command);
            return RedirectToAction("Index", "LogIn");
        }
    }
}