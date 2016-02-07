using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.ClinicRegistrationsCommands;
using Services.Interfaces.ClinicRegistrationsServices;

namespace HospitalMVC.Controllers
{
    public class MakeHospitalRegistrationPageController : Controller
    {
        private readonly IClinicRegistrationsService _clinicRegistrationService;

        public MakeHospitalRegistrationPageController(IClinicRegistrationsService clinicRegistrationService)
        {
            this._clinicRegistrationService = clinicRegistrationService;
        }

        // GET: MakeHospitalRegistrationPage
        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserMakeRegistrations)]
        public ActionResult Index(GetMakeHospitalRegistrationsPageInformationCommand command)
        {
            var answer = _clinicRegistrationService.GetMakeHospitalRegistrationsPageInformation(command);
            return View(answer);
        }
    }
}