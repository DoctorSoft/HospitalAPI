using System;
using System.Web.Mvc;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers;
using ServiceModels.ServiceCommands.MainPageCommands;
using Services.Interfaces.MainPageServices;

namespace HospitalMVC.Controllers
{
    public class ReviewerHomePageController : Controller
    {
        private readonly IMainPageService _mainPageService;

        public ReviewerHomePageController(IMainPageService mainPageService)
        {
            _mainPageService = mainPageService;
        }
        // GET: ReviewerHomePage
        [TokenAuthorizationFilter]
        public ActionResult Index(GetReviewerMainPageInformationCommand command)
        {
            var answer = _mainPageService.GetReviewerMainPageInformation(command);
         
            return View(answer);
        }
    }
}