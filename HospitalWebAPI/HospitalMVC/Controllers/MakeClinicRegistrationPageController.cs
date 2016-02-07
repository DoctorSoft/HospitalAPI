using System.Linq;
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
            return View(answer);
        }

        [HttpPost]
        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess, FunctionIdentityName.ClinicUserMakeRegistrations)]
        public ActionResult SaveRegistration(SaveClinicRegistrationCommand command)
        {
            var answer = _clinicRegistrationsService.SaveClinicRegistration(command);

            if (answer.Errors.Any())
            {
                var model = new SaveClinicRegistrationCommand
                {
                    Sex = command.Sex,
                    DateValue = command.DateValue,
                    Date = command.Date,
                    CurrentHospitalId = command.CurrentHospitalId,
                    Token = answer.Token,
                    SectionProfileId = command.SectionProfileId,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    SexId = command.SexId,
                    SectionProfile = command.SectionProfile,
                    Age = command.Age,
                    Code = command.Code,
                    CurrentHospital = command.CurrentHospital,
                    PhoneNumber = command.PhoneNumber,
                    Diagnosis = command.Diagnosis
                };
                ViewBag.Errors = answer.Errors;

                return View("Step3", model);
            }

            return RedirectToAction("Index", "ClinicUserHomePage", new { Token = command.Token });
        }
    }
}