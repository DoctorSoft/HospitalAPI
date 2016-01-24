using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.ClinicRegistrationsCommands;
using Services.Interfaces.ClinicRegistrationsServices;

namespace HospitalMVC.Controllers
{
    public class MakeClinicRegistrationPageController : Controller
    {
        private readonly IClinicRegistrationsService _clinicRegistrationsService;

        public MakeClinicRegistrationPageController(IClinicRegistrationsService clinicRegistrationsService)
        {
            _clinicRegistrationsService = clinicRegistrationsService;
        }

        // GET: MakeClinicRegistrationPage
        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess, FunctionIdentityName.ClinicUserMakeRegistrations)]
        public ActionResult Index(GetMakeClinicRegistrationsPageInformationCommand command)
        {
            var answer = _clinicRegistrationsService.GetMakeClinicRegistrationsPageInformation(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess, FunctionIdentityName.ClinicUserMakeRegistrations)]
        public ActionResult Step2(GetClinicRegistrationScheduleCommand command)
        {
            var answer = _clinicRegistrationsService.GetClinicRegistrationSchedule(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess, FunctionIdentityName.ClinicUserMakeRegistrations)]
        public ActionResult Step3(GetClinicRegistrationUserFormCommand command)
        {
            var answer = _clinicRegistrationsService.GetClinicRegistrationUserForm(command);
            var model = new SaveClinicRegistrationCommand
            {
                AgeSection = answer.AgeSection,
                Sex = answer.Sex,
                Date = answer.Date,
                CurrentHospitalId = answer.CurrentHospitalId,
                Token = answer.Token,
                SectionProfileId = answer.SectionProfileId,
                AgeSectionId = answer.AgeSectionId,
                Name = answer.Name,
                SexId = answer.SexId,
                SectionProfile = answer.SectionProfile,
                Age = answer.Age,
                Code = answer.Code,
                CurrentHospital = answer.CurrentHospital,
                PhoneNumber = answer.PhoneNumber
            };
            return View(model);
        }

        [HttpPost]
        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess, FunctionIdentityName.ClinicUserMakeRegistrations)]
        public ActionResult SaveRegistration()
        {
            return null;
        }
    }
}