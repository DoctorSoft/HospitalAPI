using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.MainPageCommands;
using Services.Interfaces.MainPageServices;

namespace HospitalMVC.Controllers
{
    public class ReceptionUserHomePageController : Controller
    {
        private readonly IMainPageService _mainPageService;

        public ReceptionUserHomePageController(IMainPageService mainPageService)
        {
            _mainPageService = mainPageService;
        }

        [TokenAuthorizationFilter(FunctionIdentityName.ReceptionUserPrimaryAccess)]
        public ActionResult Index(GetReceptionUserMainPageInformationCommand command)
        {
            var answer = _mainPageService.GetReceptionUserMainPageInformation(command);

            return View(answer);
        }
    }
}