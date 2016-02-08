using System.Linq;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.ClinicRegistrationsCommands;
using Services.Interfaces.ClinicRegistrationsServices;

namespace HospitalMVC.Controllers
{
    public class MakeHospitalRegistrationPageController : Controller
    {
        private readonly IClinicRegistrationsService _clinicRegistrationsService;

        public MakeHospitalRegistrationPageController(IClinicRegistrationsService clinicRegistrationsService)
        {
            this._clinicRegistrationsService = clinicRegistrationsService;
        }

        // GET: MakeHospitalRegistrationPage
        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserMakeRegistrations)]
        public ActionResult Index(GetMakeHospitalRegistrationsPageInformationCommand command)
        {
            var answer = _clinicRegistrationsService.GetMakeHospitalRegistrationsPageInformation(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserMakeRegistrations)]
        public ActionResult Step2(GetHospitalRegistrationUserFormCommand command)
        {
            var answer = _clinicRegistrationsService.GetHospitalRegistrationUserForm(command);
            return View(answer);
        }

        [HttpPost]
        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserMakeRegistrations)]
        public ActionResult SaveRegistration(SaveHospitalRegistrationCommand command)
        {
            var answer = _clinicRegistrationsService.SaveHospitalRegistration(command);

            if (answer.Errors.Any())
            {
                var model = new SaveHospitalRegistrationCommand
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
                    Diagnosis = command.Diagnosis,
                    DoesAgree = command.DoesAgree
                };
                ViewBag.Errors = answer.Errors;

                return View("Step2", model);
            }

            return RedirectToAction("Index", "ClinicUserHomePage", new { Token = command.Token });
        }
    }
}