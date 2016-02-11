using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.HospitalRegistrationsCommands;
using Services.Interfaces.HospitalRegistrationsService;

namespace HospitalMVC.Controllers
{
    public class ChangeHospitalRegistrationPageController : Controller
    {
        private readonly IHospitalRegistrationsService _hospitalRegistrationsService;

        public ChangeHospitalRegistrationPageController(IHospitalRegistrationsService hospitalRegistrationsService)
        {
            _hospitalRegistrationsService = hospitalRegistrationsService;
        }

        // GET: ChangeHospitalRegistrationPage
        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserChangeEmptyPlaces)]
        public ActionResult Index(GetChangeHospitalRegistrationsPageInformationCommand command)
        {
            var answer = _hospitalRegistrationsService.GetChangeHospitalRegistrationsPageInformation(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserChangeEmptyPlaces)]
        public ActionResult Step2(ShowHospitalRegistrationPlacesByDateCommand command)
        {
            var answer = _hospitalRegistrationsService.ShowHospitalRegistrationPlacesByDate(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserChangeEmptyPlaces)]
        public ActionResult ChangeHospitalRegistration(ChangeHospitalRegistrationForSelectedSectionCommand command)
        {
            var answer = _hospitalRegistrationsService.ChangeHospitalRegistrationForSelectedSection(command);
            return View(answer);
        }

        [HttpPost]
        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserChangeEmptyPlaces)]
        public ActionResult ApplyChangesHospitalRegistration(GetChangeHospitalRegistrationCommand command)
        {
            var answer = _hospitalRegistrationsService.ApplyChangesHospitalRegistration(command);
            return RedirectToAction("Step2", new ShowHospitalRegistrationPlacesByDateCommand
            {
                Token = answer.Token,
                Date =  DateTime.ParseExact(command.Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture)
            });
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserChangeEmptyPlaces)]
        public ActionResult ChangeHospitalRegistrationForNewSection(ChangeHospitalRegistrationForNewSectionCommand command)
        {
            var answer = _hospitalRegistrationsService.ChangeHospitalRegistrationForNewSection(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserChangeEmptyPlaces)]
        public ActionResult ApplyChangesNewHospitalRegistration(GetChangeNewHospitalRegistrationCommand command)
        {
            var answer = _hospitalRegistrationsService.ApplyChangesNewHospitalRegistration(command);

            return RedirectToAction("Step2", new ShowHospitalRegistrationPlacesByDateCommand
            {
                Token = answer.Token,
                Date =
                    DateTime.ParseExact(command.Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture)
            });
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserChangeEmptyPlaces)]
        public ActionResult ViewDetailedInformationOnRegisteredPatients(ViewDetailedInformationOnRegisteredPatientsCommand command)
        {
            var answer = _hospitalRegistrationsService.GetDetailedInformationOnRegisteredPatients(command);
            return View(answer);
        }
    }
}