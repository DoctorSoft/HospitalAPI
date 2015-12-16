using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.MainPageCommands;
using Services.Interfaces.MainPageServices;

namespace HospitalMVC.Controllers
{
    public class ClinicUserHomePageController : Controller
    {
        private readonly IMainPageService _mainPageService;

        public ClinicUserHomePageController(IMainPageService mainPageServices)
        {
            _mainPageService = mainPageServices;
        }

        // TODO: Move logic functions to service
        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess)]
        public ActionResult Index(GetClinicUserMainPageInformationCommand command)
        {
            var answer = _mainPageService.GetClinicUserMainPageInformation(command);
            return View(answer);
        }
    }
}