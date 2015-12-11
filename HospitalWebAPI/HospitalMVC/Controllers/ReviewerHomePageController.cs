using System;
using System.Web.Mvc;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers;
using ServiceModels.ServiceCommands.MainPageCommands;

namespace HospitalMVC.Controllers
{
    public class ReviewerHomePageController : Controller
    {
        // GET: ReviewerHomePage
        [TokenAuthorizationFilter]
        public ActionResult Index(GetReviewerMainPageInformationCommand command)
        {
            //TODO: Change Guid token to command

            var answer = new GetReviewerMainPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };
            return View(answer);
        }
    }
}