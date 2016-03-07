using System.Web;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.NoticesCommands;
using Services.Interfaces.NoticesService;

namespace HospitalMVC.Controllers
{
    public class HospitalNoticesPageController : Controller
    {
        private readonly INoticesService _noticesService;

        public HospitalNoticesPageController(INoticesService noticesService)
        {
            _noticesService = noticesService;
        }

        // GET: HospitalNoticesPage
        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserShowMessages)]
        public ActionResult Index(GetHospitalNoticesPageInformationCommand command)
        {
            var answer = _noticesService.GetHospitalNoticesPageInformation(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserShowMessages)]
        public ActionResult ReadMessage(GetHospitalMessageByIdCommand command)
        {
            var answer = _noticesService.GetHospitalMessageById(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserShowMessages)]
        public ActionResult RemoveMessage(RemoveHospitalMessageByIdCommand command)
        {
            var answer = _noticesService.RemoveHospitalMessageById(command);
            return RedirectToAction("Index", new GetHospitalNoticesPageInformationCommand { Token = answer.Token });
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserSendDischarges)]
        public ActionResult ShowPageToSendDischange(ShowPageToSendDischangeCommand command)
        {
            var answer = _noticesService.ShowPageToSendDischange(command);
            return View(answer);
        }

        [HttpPost]
        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserSendDischarges)]
        public ActionResult SaveDischarge(SaveDischargeCommand command, HttpPostedFileBase discharge)
        {
            command.File = discharge.InputStream;
            command.FileName = discharge.FileName;
            command.Size = discharge.ContentLength;
            command.ContentType = discharge.ContentType;

            var answer = _noticesService.SaveDischarge(command);
            return RedirectToAction("Index", "HospitalUserHomePage", new GetHospitalNoticesPageInformationCommand { Token = answer.Token });
        }
    }
}