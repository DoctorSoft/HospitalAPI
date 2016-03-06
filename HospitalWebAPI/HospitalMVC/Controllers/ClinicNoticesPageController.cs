using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.NoticesCommands;
using Services.Interfaces.NoticesService;

namespace HospitalMVC.Controllers
{
    public class ClinicNoticesPageController : Controller
    {
        private readonly INoticesService _noticesService;

        public ClinicNoticesPageController(INoticesService noticesService)
        {
            _noticesService = noticesService;
        }

        // GET: ClinicNoticesPage
        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess, FunctionIdentityName.ClinicUserShowMessages)]
        public ActionResult Index(GetClinicNoticesPageInformationCommand command)
        {
            var answer = _noticesService.GetClinicNoticesPageInformation(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess, FunctionIdentityName.ClinicUserShowMessages)]
        public ActionResult ReadMessage(GetClinicMessageByIdCommand command)
        {
            var answer = _noticesService.GetClinicMessageById(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess, FunctionIdentityName.ClinicUserShowMessages)]
        public ActionResult RemoveMessage(RemoveClinicMessageByIdCommand command)
        {
            var answer = _noticesService.RemoveClinicMessageById(command);
            return RedirectToAction("Index", new GetClinicNoticesPageInformationCommand { Token = answer.Token });
        }

        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess, FunctionIdentityName.ClinicUserDownloadDischarges)]
        public ActionResult ShowDischargesList(ShowDischargesListCommand command)
        {
            var answer = _noticesService.ShowDischargesList(command);
            return View(answer);
        }
    }
}