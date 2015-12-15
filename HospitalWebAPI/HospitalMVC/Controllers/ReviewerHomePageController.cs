using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
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
        
        [TokenAuthorizationFilter(FunctionIdentityName.ReviewerPrimaryAccess)]
        public ActionResult Index(GetReviewerMainPageInformationCommand command)
        {
            var answer = _mainPageService.GetReviewerMainPageInformation(command);
         
            return View(answer);
        }
    }
}