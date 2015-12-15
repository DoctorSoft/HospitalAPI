using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.ClinicRegistrationsCommands;
using Services.Interfaces.ClinicRegistrationsServices;

namespace HospitalMVC.Controllers
{
    public class BreakClinicRegistrationPageController : Controller
    {
        private readonly IClinicRegistrationsService _clinicRegistrationsService;

        public BreakClinicRegistrationPageController(IClinicRegistrationsService clinicRegistrationsService)
        {
            _clinicRegistrationsService = clinicRegistrationsService;
        }

        // GET: BreakClinicRegistrationPage
        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess, FunctionIdentityName.ClinicUserBreakRegistrations)]
        public ActionResult Index(GetBreakClinicRegistrationsPageInformationCommand command)
        {
            var answer = _clinicRegistrationsService.GetBreakClinicRegistrationsPageInformation(command);
            return View(answer);
        }
    }
}