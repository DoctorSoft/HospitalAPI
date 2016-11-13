using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.HospitalRegistrationsCommands;
using Services.Interfaces.HospitalRegistrationsService;

namespace HospitalMVC.Controllers
{
    public class ViewHospitalRegistrationsController : Controller
    {
        private readonly IHospitalRegistrationsService _hospitalRegistrationsService;

        public ViewHospitalRegistrationsController(IHospitalRegistrationsService hospitalRegistrationsService)
        {
            _hospitalRegistrationsService = hospitalRegistrationsService;
        }

        // GET: ViewHospitalRegistrations

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserWatchRegisteredUsers)]
        public ActionResult Index(GetComingRecordsCommand command)
        {
            var answer = _hospitalRegistrationsService.GetComingRecords(command);

            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserChangeEmptyPlaces)]
        public ActionResult BreakRegistration(BreakHospitalRegistrationCommand command)
        {
            var answer = _hospitalRegistrationsService.BreakHospitalRegistration(command);
            return RedirectToAction("Index",
            new
            {
                Token = command.Token,
                answer.DialogMessage,
                answer.HasDialogMessage
            });
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserWatchRegisteredUsers)]
        public ActionResult ViewMore(GetHospitalRegistrationRecordCommand command)
        {
            var result = _hospitalRegistrationsService.GetHospitalRegistrationRecord(command);
            return View(result);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserWatchRegisteredUsers)]
        public FileResult DownloadReservationFile(DownloadHospitalReservationFileCommand command)
        {
            var result = _hospitalRegistrationsService.DownloadHospitalReservationFile(command);
            return File(result.File, System.Net.Mime.MediaTypeNames.Application.Octet, result.FileName);
        }


    }
}