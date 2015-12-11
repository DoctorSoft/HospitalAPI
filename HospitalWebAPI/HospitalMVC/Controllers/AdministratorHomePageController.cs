using System;
using System.Web.Mvc;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers;
using ServiceModels.ServiceCommands.MainPageCommands;

namespace HospitalMVC.Controllers
{
    public class AdministratorHomePageController : Controller
    {
        [TokenAuthorizationFilter()]
        public ActionResult Index(GetAdministratorMainPageInformationCommand command)
        {
            //TODO: Change Guid token to command

            var answer = new GetAdministratorMainPageInformationCommandAnswer
            {
                Token = (Guid) command.Token
            };
            return View(answer);
        }
    }
}