using System;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.MainPageCommands;
using Services.Interfaces.MainPageServices;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.UserModels;

namespace HospitalMVC.Controllers
{
    public class ClinicUserHomePageController : Controller
    {
        private readonly IMainPageService _mainPageService;
        private readonly ITokenManager _tokenManager;

        private static bool? _reservationStatus;
        private const string StartTimeRegistration = "10:00";
        private const string EndTimeRegistration = "19:00";

        public ClinicUserHomePageController(IMainPageService mainPageServices, ITokenManager tokenManager)
        {
            _mainPageService = mainPageServices;
            _tokenManager = tokenManager;
        }

        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess)]
        public ActionResult Index(GetClinicUserMainPageInformationCommand command)
        {
            var answer = _mainPageService.GetClinicUserMainPageInformation(command);

            if (_reservationStatus == null || (bool) !_reservationStatus)
            {
               CheckReservationStatus();
            }

            var user = _tokenManager.GetUserByToken(command.Token);

            SetViewBagUserName(user);
            SetViewBagTimeRegistration();
            
            return View(answer);
        }

        public void CheckReservationStatus()
        {
            var startTime = TimeSpan.Parse(StartTimeRegistration);
            var endTime = TimeSpan.Parse(StartTimeRegistration);
            var now = DateTime.Now.TimeOfDay;

            _reservationStatus = now >= startTime && now <= endTime;
            ViewBag.reservationStatus = _reservationStatus;
        }

        public void SetViewBagTimeRegistration()
        {
            ViewBag.startTimeReservation = StartTimeRegistration;
            ViewBag.endTimeReservation = EndTimeRegistration;
        }
        public void SetViewBagUserName(UserStorageModel user)
        {
            ViewBag.UserName = user.Name;
        }
    }
}