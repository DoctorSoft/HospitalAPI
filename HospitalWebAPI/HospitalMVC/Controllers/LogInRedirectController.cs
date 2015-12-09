using System.Collections.Generic;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using Microsoft.Ajax.Utilities;
using ServiceModels.ServiceCommands.MainPageCommands;
using Services.Interfaces.MainPageServices;

namespace HospitalMVC.Controllers
{
    public class LogInRedirectController : Controller
    {
        private readonly IMainPageService _mainPageService;

        private readonly Dictionary<UserType, KeyValuePair<string, string>> _actionDictionary;

        private const string LogInActionName = "Index";
        private const string LogInControllerName = "LogIn";

        public LogInRedirectController(IMainPageService mainPageService)
        {
            _mainPageService = mainPageService;

            // This controller action library. If you want to rename or remove any actions from home controllers then make changes here too.
            _actionDictionary = new Dictionary<UserType, KeyValuePair<string, string>>
            {
                {UserType.ClinicUser, new KeyValuePair<string, string>("HospitalUserHomePage", "Index")},
                {UserType.HospitalUser, new KeyValuePair<string, string>("ClinicUserHomePage", "Index")},
                {UserType.Administrator, new KeyValuePair<string, string>("AdministratorHomePage", "Index")},
                {UserType.Reviewer, new KeyValuePair<string, string>("ReviewerHomePage", "Index")}
            };
        }

        // GET: SingInRedirect
        //[TokenAuthorizationFilter]
        public ActionResult RedirectToMainPage(GetUserMainPageInformationCommand command)
        {
            if (command == null || command.Token == null)
            {
                return RedirectToAction(LogInActionName, LogInControllerName);
            }

            var answer = _mainPageService.GetUserMainPageInformation(command);
            var action = _actionDictionary[answer.UserType];

            return RedirectToAction(action.Value, action.Key, new {Token = command.Token});
        }
    }
}