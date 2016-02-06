using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.StatisticCommands;
using Services.Interfaces.StatisticServices;

namespace HospitalMVC.Controllers
{
    public class ReviewerShowStatisticPageController : Controller
    {
        private readonly IStatisticService _statisticService;

        public ReviewerShowStatisticPageController(IStatisticService statisticService)
        {
            this._statisticService = statisticService;
        }

        // GET: ReviewerShowStatisticPage
        [TokenAuthorizationFilter(FunctionIdentityName.ReviewerWatchStatistic)]
        public ActionResult Index(GetReviewerStatisticPageInformationCommand command)
        {
            var answer = this._statisticService.GetReviewerStatisticPageInformation(command);
            return View(answer);
        }
    }
}