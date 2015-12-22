using System.Collections.Generic;
using System.Web.Mvc;
using Enums.Enums;
using ServiceModels.ServiceCommands.MainPageCommands;
using Services.Interfaces.MainPageServices;

namespace HospitalMVC.Controllers
{
    public class LogInRedirectController : Controller
    {
        private readonly IMainPageService _mainPageService;

        private readonly Dictionary<UserAccountType, KeyValuePair<string, string>> _actionDictionary;

        private const string LogInActionName = "Index";
        private const string LogInControllerName = "LogIn";

        public LogInRedirectController(IMainPageService mainPageService)
        {
            _mainPageService = mainPageService;

            // This controller action library. If you want to rename or remove any actions from home controllers then make changes here too.
            _actionDictionary = new Dictionary<UserAccountType, KeyValuePair<string, string>>
            {
                {UserAccountType.HospitalUserAccount, new KeyValuePair<string, string>("HospitalUserHomePage", "Index")},
                {UserAccountType.ClinicUserAccount, new KeyValuePair<string, string>("ClinicUserHomePage", "Index")},
                {UserAccountType.AdministratorAccount, new KeyValuePair<string, string>("AdministratorHomePage", "Index")},
                {UserAccountType.ReviewerAccount, new KeyValuePair<string, string>("ReviewerHomePage", "Index")},
                {UserAccountType.ReceptionUserAccount, new KeyValuePair<string, string>("ReceptionUserHomePage", "Index")},
                {UserAccountType.None, new KeyValuePair<string, string>("LogIn", "Index")}
            };
        }

        // GET: SingInRedirect
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