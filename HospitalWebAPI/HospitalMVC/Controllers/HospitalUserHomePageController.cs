using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.MainPageCommands;
using Services.Interfaces.MainPageServices;

namespace HospitalMVC.Controllers
{
    public class HospitalUserHomePageController : Controller
    {
        private readonly IMainPageService _mainPageService;

        public HospitalUserHomePageController(IMainPageService mainPageService)
        {
            _mainPageService = mainPageService;
        }

        
        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess)]
        public ActionResult Index(GetHospitalUserMainPageInformationCommand command)
        {
            var answer = _mainPageService.GetHospitalUserMainPageInformation(command);
            
            return View(answer);
        }
    }
}