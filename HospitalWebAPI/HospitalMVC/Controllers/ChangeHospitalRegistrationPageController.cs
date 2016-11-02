using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers;
using ServiceModels.ServiceCommands.ClinicRegistrationsCommands;
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
        public ActionResult GetRegistrationScheduleBySection(GetRegistrationScheduleBySectionCommand command)
        {
            var answer = _hospitalRegistrationsService.GetRegistrationScheduleBySection(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserChangeEmptyPlaces)]
        public ActionResult SwitchRegistrationPage(SwitchRegistrationPageCommand command)
        {
            var answer = _hospitalRegistrationsService.SwitchRegistrationPage(command);
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
            if (answer.Errors.Any())
            {
                return View("ChangeHospitalRegistration", new ChangeHospitalRegistrationForSelectedSectionCommandAnswer
                {
                    Date = command.Date,
                    HospitalProfileId = command.HospitalProfileId,
                    Token = command.Token.Value,
                    StatisticItems = command.FreeHospitalSectionsForRegistration,
                    Errors = answer.Errors
                });
            }

            return RedirectToAction("Step2", "ChangeHospitalRegistrationPage", new
            {
                Token = answer.Token,
                Date = DateTime.ParseExact(command.Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture),
                answer.DialogMessage,
                answer.HasDialogMessage
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
            
            if (answer.Errors.Any())
            {
                return View("ChangeHospitalRegistrationForNewSection", new ChangeHospitalRegistrationForNewSectionCommandAnswer
                {
                    Date = command.Date,
                    Token = command.Token.Value,
                    FreeHospitalSectionsForRegistration = answer.FreeHospitalSectionsForRegistration,
                    Errors = answer.Errors
                });
            }

            return RedirectToAction("Step2", "ChangeHospitalRegistrationPage", new
            {
                Token = answer.Token,
                Date = DateTime.ParseExact(command.Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture),
                answer.DialogMessage,
                answer.HasDialogMessage
            });
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserChangeEmptyPlaces)]
        public ActionResult ViewDetailedInformationOnRegisteredPatients(ViewDetailedInformationOnRegisteredPatientsCommand command)
        {
            var answer = _hospitalRegistrationsService.GetDetailedInformationOnRegisteredPatients(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess,
            FunctionIdentityName.HospitalUserChangeEmptyPlaces)]
        public ActionResult BreakRegistration(BreakHospitalRegistrationCommand command)
        {
            var answer = _hospitalRegistrationsService.BreakHospitalRegistration(command);
            if (command.FullInformation != null)
            {
                return RedirectToAction("ViewDetailedInformationOnRegisteredPatients",
                    new
                    {
                        Token = command.Token,
                        FullInformation = command.FullInformation,
                        HospitalProfileId = command.HospitalProfileId,
                        Date = command.Date,
                        EmptyPlaceByTypeStatisticId = command.EmptyPlaceByTypeStatisticId,
                        answer.DialogMessage,
                        answer.HasDialogMessage
                    });
            }
            return RedirectToAction("ViewDetailedInformationOnRegisteredPatients",
                new
                {
                    Token = command.Token,
                    HospitalProfileId = command.HospitalProfileId,
                    Date = command.Date,
                    EmptyPlaceByTypeStatisticId = command.EmptyPlaceByTypeStatisticId,
                    answer.DialogMessage,
                    answer.HasDialogMessage
                });
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserAutocompletePlaces)]
        public ActionResult ShowAutocompletePage(ShowAutocompletePageCommand command)
        {
            var answer = _hospitalRegistrationsService.ShowAutocompletePage(command);

            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserAutocompletePlaces)]
        public ActionResult AutocompleteEmptyPlaces(AutocompleteEmptyPlacesCommand command)
        {
            var answer = _hospitalRegistrationsService.AutocompleteEmptyPlaces(command);

            if (answer.Errors.Any())
            {
                return this.View(
                    "ShowAutocompletePage",
                    new ShowAutocompletePageCommandAnswer
                        {
                            HospitalSectionProfileId = command.HospitalSectionProfileId,
                            SexId = command.SexId,
                            Token = command.Token.Value,
                            CountValue = command.CountValue,
                            Errors = answer.Errors,
                            HasGenderFactor = answer.HasGenderFactor,
                            HospitalSectionProfiles = answer.HospitalSectionProfiles,
                            Sexes = answer.Sexes,
                            DaysOfWeek = answer.DaysOfWeek
                        });
            }

            return RedirectToAction("ShowAutocompletePage", "ChangeHospitalRegistrationPage", new
            {
                Token = command.Token, 
                HospitalSectionProfileId = command.HospitalSectionProfileId,
                SexId = command.SexId, 
                answer.DialogMessage, 
                answer.HasDialogMessage
            });
        }
    }
}