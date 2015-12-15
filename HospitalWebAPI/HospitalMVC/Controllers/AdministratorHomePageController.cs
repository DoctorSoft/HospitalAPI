using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.MainPageCommands;
using Services.Interfaces.MainPageServices;

namespace HospitalMVC.Controllers
{
    public class AdministratorHomePageController : Controller
    {
        private readonly IMainPageService _mainPageService;

        public AdministratorHomePageController(IMainPageService mainPageService)
        {
            _mainPageService = mainPageService;
        }

        [TokenAuthorizationFilter(FunctionIdentityName.AdministratorPrimaryAccess)]
        public ActionResult Index(GetAdministratorMainPageInformationCommand command)
        {
            var answer = _mainPageService.GetAdministratorMainPageInformation(command);
            
            return View(answer);
        }
    }
}