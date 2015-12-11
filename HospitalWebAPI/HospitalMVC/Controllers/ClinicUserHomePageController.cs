using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers;
using ServiceModels.ServiceCommands.MainPageCommands;

namespace HospitalMVC.Controllers
{
    public class ClinicUserHomePageController : Controller
    {
        // GET: ClinicUserHomePage
        [TokenAuthorizationFilter(FunctionIdentityName.MakeEmptyPlaceReservation)]
        public ActionResult Index(GetClinicUserMainPageInformationCommand command)
        {
            //TODO: Change Guid token to command
            var answer = new GetClinicUserMainPageInformationCommandAnswer
            {
                Token = (Guid) command.Token
            };
            return View(answer);
        }
    }
}