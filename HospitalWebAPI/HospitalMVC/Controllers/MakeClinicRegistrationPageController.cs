using System.Linq;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers;
using ServiceModels.ServiceCommands.ClinicRegistrationsCommands;
using Services.Interfaces.ClinicRegistrationsServices;

namespace HospitalMVC.Controllers
{
    using HelpingTools.ExtentionTools;

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
                var model = new GetClinicRegistrationUserFormCommandAnswer
                {
                    Sex = command.Sex,
                    DateValue = command.DateValue,
                    Date = command.DateValue.ToCorrectDateString(),
                    CurrentHospitalId = command.CurrentHospitalId,
                    Token = answer.Token,
                    SectionProfileId = command.SectionProfileId,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    SexId = command.SexId,
                    SectionProfile = command.SectionProfile,
                    Years = command.Years,
                    Months = command.Months,
                    Weeks = command.Weeks,
                    Code = command.Code,
                    CurrentHospital = command.CurrentHospital,
                    PhoneNumber = command.PhoneNumber,
                    Diagnosis = command.Diagnosis,
                    MedicalExaminationResult = command.MedicalExaminationResult,
                    MedicalConsultion = command.MedicalConsultion,
                    ReservationPurpose = command.ReservationPurpose,
                    OtherInformation = command.OtherInformation,
                    Errors = answer.Errors,
                    AgeCategoryId = command.AgeCategoryId
                };

                return View("Step3", model);
            }

            return RedirectToAction("Step2", "MakeClinicRegistrationPage", 
                new
                {
                    Token = command.Token, 
                    Sex = command.SexId, 
                    SectionProfileId = command.SectionProfileId, 
                    answer.DialogMessage,
                    answer.HasDialogMessage
                });
        }
    }
}