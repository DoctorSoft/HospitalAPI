using System;
using System.Linq;
using Enums.Enums;
using HandleToolsInterfaces.Converters;
using RepositoryTools.Interfaces.PrivateInterfaces.MailboxRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers;
using ServiceModels.ServiceCommands.MainPageCommands;
using Services.Interfaces.MainPageServices;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.UserModels;

namespace Services.MainPageServices
{
    public class MainPageService : IMainPageService
    {
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IMessageRepository _messageRepository;

        private readonly IUserToAccountTypeConverter _userToAccountTypeConverter;
        private readonly ITokenManager _tokenManager;


        public MainPageService(IUserTypeRepository userTypeRepository, IUserToAccountTypeConverter userToAccountTypeConverter, ITokenManager tokenManager, IMessageRepository messageRepository)
        {
            _userTypeRepository = userTypeRepository;
            _userToAccountTypeConverter = userToAccountTypeConverter;
            _tokenManager = tokenManager;
            _messageRepository = messageRepository;
        }

        protected virtual UserType GetUserType(UserStorageModel user)
        {
            var userType = _userTypeRepository.GetModels().FirstOrDefault(model => model.Id == user.UserTypeId);

            return userType.UserType;
        }
        public bool GetReservationStatus(string startTimeRegistration, string endTimeRegistration)
        {
            var startTime = TimeSpan.Parse(startTimeRegistration);
            var endTime = TimeSpan.Parse(endTimeRegistration);
            var now = DateTime.Now.TimeOfDay;

            return now >= startTime && now <= endTime;
        }
        public int? GetCountNewNotices(UserStorageModel user)
        {
            int? countNewNotices =
                _messageRepository.GetModels()
                    .Where(model => model.UserToId == user.Id).Count(model => !model.IsRead);
            return countNewNotices;
        }
        public GetUserMainPageInformationCommandAnswer GetUserMainPageInformation(GetUserMainPageInformationCommand command)
        {
            var currentUser = _tokenManager.GetUserByToken(command.Token);

            if (currentUser == null)
            {
                return new GetUserMainPageInformationCommandAnswer
                {
                    UserType = UserAccountType.None,
                };
            }

            var userType = GetUserType(currentUser);
            var resultUserType = _userToAccountTypeConverter.Convert(userType);

            return new GetUserMainPageInformationCommandAnswer
            {
                UserType = resultUserType,
                Token = (Guid)command.Token
            };
        }

        public GetAdministratorMainPageInformationCommandAnswer GetAdministratorMainPageInformation(
            GetAdministratorMainPageInformationCommand command)
        {
            var answer = new GetAdministratorMainPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };

            return answer;
        }

        public GetClinicUserMainPageInformationCommandAnswer GetClinicUserMainPageInformation(
            GetClinicUserMainPageInformationCommand command)
        {
            var currentUser = _tokenManager.GetUserByToken(command.Token);
            const string startTimeRegistration = "10:00";
            const string endTimeRegistration = "19:00";

            var reservationStatus = GetReservationStatus(startTimeRegistration, endTimeRegistration);

            var countNewNotices = GetCountNewNotices(currentUser);

            var answer = new GetClinicUserMainPageInformationCommandAnswer
            {
                Token = (Guid) command.Token,
                UserName = currentUser.Name,
                StartTimeReservation = startTimeRegistration,
                EndTimeReservation = endTimeRegistration,
                ReservationStatus = reservationStatus,
                CountNewNotices = countNewNotices
            };
            return answer;
        }

        public GetHospitalUserMainPageInformationCommandAnswer GetHospitalUserMainPageInformation(
            GetHospitalUserMainPageInformationCommand command)
        {
            var answer = new GetHospitalUserMainPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };
            return answer;
        }

        public GetReviewerMainPageInformationCommandAnswer GetReviewerMainPageInformation(GetReviewerMainPageInformationCommand command)
        {
            var answer = new GetReviewerMainPageInformationCommandAnswer
            {
                Token = (Guid) command.Token
            };
            return answer;
        }
    }
}
