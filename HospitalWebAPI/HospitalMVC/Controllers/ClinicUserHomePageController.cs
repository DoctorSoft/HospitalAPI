using System;
using System.Linq;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using RepositoryTools.Interfaces.PrivateInterfaces.MailboxRepositories;
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
        private readonly IMessageRepository _messageRepository;

        private static bool? _reservationStatus;
        private const string StartTimeRegistration = "10:00";
        private const string EndTimeRegistration = "19:00";

        public ClinicUserHomePageController(IMainPageService mainPageServices, ITokenManager tokenManager, IMessageRepository messageRepository)
        {
            _mainPageService = mainPageServices;
            _tokenManager = tokenManager;
            _messageRepository = messageRepository;
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
            var countNewNotices = GetCountNewNotices(user); 

            SetViewBagUserName(user);
            SetViewBagTimeRegistration();
            SetViewBagCountNewNotices(countNewNotices);

            return View(answer);
        }

        public void CheckReservationStatus()
        {
            var startTime = TimeSpan.Parse(StartTimeRegistration);
            var endTime = TimeSpan.Parse(EndTimeRegistration);
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
        public void SetViewBagCountNewNotices(int count)
        {
            ViewBag.CountNewNotices = count;
        }
        public int GetCountNewNotices(UserStorageModel user)
        {
            int countNewNotices =
                _messageRepository.GetModels()
                    .Where(model => model.UserToId == user.Id).Count(model => !model.IsRead);
            return countNewNotices;
        }
    }
}