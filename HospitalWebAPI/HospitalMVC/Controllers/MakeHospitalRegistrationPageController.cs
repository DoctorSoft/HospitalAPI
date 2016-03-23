using System.Linq;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers;
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

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserMakeRegistrations)]
        public ActionResult SaveRegistration(SaveHospitalRegistrationCommand command)
        {
            var answer = _clinicRegistrationsService.SaveHospitalRegistration(command);

            if (answer.Errors.Any())
            {
                var model = new GetHospitalRegistrationUserFormCommandAnswer
                {
                    Date = answer.Date,
                    Token = answer.Token,
                    FirstName = answer.FirstName,
                    LastName = answer.LastName,
                    SexId = answer.SexId,
                    Age = answer.Age,
                    Code = answer.Code,
                    PhoneNumber = answer.PhoneNumber,
                    Diagnosis = answer.Diagnosis,
                    DoesAgree = answer.DoesAgree,
                    UserId = answer.UserId,
                    HospitalSectionProfileId = answer.HospitalSectionProfileId,
                    Sex = answer.Sex,
                    ClinicId = answer.ClinicId,
                    Clinics = answer.Clinics,
                    Users = answer.Users,
                    HospitalSectionProfile = answer.HospitalSectionProfile,
                    Errors = answer.Errors
                };

                return View("Step2", model);
            }

            return RedirectToAction("Index", "HospitalUserHomePage", new { Token = command.Token, ShowModalWindow = true });
        }
    }
}