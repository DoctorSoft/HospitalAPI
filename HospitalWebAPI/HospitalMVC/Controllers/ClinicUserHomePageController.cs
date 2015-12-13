using System;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.MainPageCommands;
using Services.Interfaces.MainPageServices;

namespace HospitalMVC.Controllers
{
    public class ClinicUserHomePageController : Controller
    {
        private readonly IMainPageService _mainPageService;
        private static bool? _reservationStatus;
        private const string StartTimeRegistration = "10:00";

        public ClinicUserHomePageController(IMainPageService mainPageServices)
        {
            _mainPageService = mainPageServices;
        }

        [TokenAuthorizationFilter(FunctionIdentityName.MakeEmptyPlaceReservation)]
        public ActionResult Index(GetClinicUserMainPageInformationCommand command)
        {
            var answer = _mainPageService.GetClinicUserMainPageInformation(command);

            if (_reservationStatus == null || (bool) !_reservationStatus)
            {
               CheckReservationStatus();
            }
            return View(answer);
        }

        public void CheckReservationStatus()
        {
            var start = TimeSpan.Parse(StartTimeRegistration);
            var now = DateTime.Now.TimeOfDay;

            _reservationStatus = now >= start;
        }
    }
}