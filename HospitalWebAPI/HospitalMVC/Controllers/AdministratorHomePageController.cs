using System;
using System.Web.Mvc;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers;
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

        [TokenAuthorizationFilter()]
        public ActionResult Index(GetAdministratorMainPageInformationCommand command)
        {
            var answer = _mainPageService.GetAdministratorMainPageInformation(command);
            
            return View(answer);
        }
    }
}