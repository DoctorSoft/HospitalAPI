using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.NoticesCommands;
using Services.Interfaces.NoticesService;

namespace HospitalMVC.Controllers
{
    public class HospitalUserSendDistributiveMessagesPageController : Controller
    {
        private readonly INoticesService _noticesService;

        public HospitalUserSendDistributiveMessagesPageController(INoticesService noticesService)
        {
            _noticesService = noticesService;
        }

        // GET: HospitalUserSendDistributiveMessagesPage
        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserSendDistributionMessages)]
        public ActionResult Index(GetHospitalNoticesPageInformationCommand command)
        {
            var answer = _noticesService.GetHospitalNoticesPageInformation(command);
            return View(answer);
        }
    }
}