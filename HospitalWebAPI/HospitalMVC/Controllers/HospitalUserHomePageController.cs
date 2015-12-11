using System;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers;
using ServiceModels.ServiceCommands.MainPageCommands;

namespace HospitalMVC.Controllers
{
    public class HospitalUserHomePageController : Controller
    {
        // GET: HospitalUserHomePage
        [TokenAuthorizationFilter(FunctionIdentityName.EditEmptyPlacesByHospital)]
        public ActionResult Index(GetHospitalUserMainPageInformationCommand command)
        {
            //TODO: Change Guid token to command

            var answer = new GetHospitalUserMainPageInformationCommandAnswer
            {
                Token = (Guid) command.Token
            };
            return View(answer);
        }
    }
}