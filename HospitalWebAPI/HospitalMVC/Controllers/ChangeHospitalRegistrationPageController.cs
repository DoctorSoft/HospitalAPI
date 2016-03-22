using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers;
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
            if (answer.Errors.Any())
            {
                ViewBag.Errors = answer.Errors;
                return View("ChangeHospitalRegistration", new ChangeHospitalRegistrationForSelectedSectionCommandAnswer
                {
                    Date = command.Date,
                    HospitalProfileId = command.HospitalProfileId,
                    Token = command.Token.Value,
                    StatisticItems = command.FreeHospitalSectionsForRegistration
                });
            }

            return RedirectToAction("Step2", new ShowHospitalRegistrationPlacesByDateCommand
            {
                Token = answer.Token,
                Date =  DateTime.ParseExact(command.Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture),
                ShowModalWindow = true
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
                ViewBag.Errors = answer.Errors;
                return View("ChangeHospitalRegistrationForNewSection", new ChangeHospitalRegistrationForNewSectionCommandAnswer
                {
                    Date = command.Date,
                    Token = command.Token.Value,
                    FreeHospitalSectionsForRegistration = answer.FreeHospitalSectionsForRegistration
                });
            }

            return RedirectToAction("Step2", new ShowHospitalRegistrationPlacesByDateCommand
            {
                Token = answer.Token,
                Date =
                    DateTime.ParseExact(command.Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    ShowModalWindow = true
            });
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserChangeEmptyPlaces)]
        public ActionResult ViewDetailedInformationOnRegisteredPatients(ViewDetailedInformationOnRegisteredPatientsCommand command)
        {
            var answer = _hospitalRegistrationsService.GetDetailedInformationOnRegisteredPatients(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserChangeEmptyPlaces)]
        public ActionResult BreakRegistration(BreakHospitalRegistrationCommand command)
        {
            _hospitalRegistrationsService.BreakHospitalRegistration(command);
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
                    ShowModalWundow = true
                });
            }
            else
            {
                return RedirectToAction("ViewDetailedInformationOnRegisteredPatients",
                    new
                    {
                        Token = command.Token,
                        HospitalProfileId = command.HospitalProfileId,
                        Date = command.Date,
                        EmptyPlaceByTypeStatisticId = command.EmptyPlaceByTypeStatisticId,
                        ShowModalWindow = true
                    });
            }
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

            return RedirectToAction("Index", "Home", new { Token = command.Token });
        }
    }
}