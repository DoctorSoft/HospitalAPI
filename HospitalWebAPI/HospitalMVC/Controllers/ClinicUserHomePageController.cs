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
        public ClinicUserHomePageController(IMainPageService mainPageService)
        {
            _mainPageService = mainPageService;
        }

        
        [TokenAuthorizationFilter(FunctionIdentityName.MakeEmptyPlaceReservation)]
        public ActionResult Index(GetClinicUserMainPageInformationCommand command)
        {
           var answer = _mainPageService.GetClinicUserMainPageInformation(command);

           return View(answer);
        }
    }
}