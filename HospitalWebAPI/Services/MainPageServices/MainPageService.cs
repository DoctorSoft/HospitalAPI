using System;
using System.Linq;
using Enums.Enums;
using HandleToolsInterfaces.Converters;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
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
        private readonly ISettingsManager _settingsManager;
        
        public MainPageService(IUserTypeRepository userTypeRepository, IUserToAccountTypeConverter userToAccountTypeConverter, ITokenManager tokenManager, IMessageRepository messageRepository, ISettingsManager settingsManager)
        {
            _userTypeRepository = userTypeRepository;
            _userToAccountTypeConverter = userToAccountTypeConverter;
            _tokenManager = tokenManager;
            _messageRepository = messageRepository;
            _settingsManager = settingsManager;
        }

        protected virtual UserType GetUserType(UserStorageModel user)
        {
            var userType = _userTypeRepository.GetModels().FirstOrDefault(model => model.Id == user.UserTypeId);

            return userType.UserType;
        }
        public bool GetReservationStatus(TimeSpan startTimeRegistration, TimeSpan endTimeRegistration)
        {
            var now = DateTime.Now.TimeOfDay;

            return now >= startTimeRegistration && now <= endTimeRegistration;
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
            var clinicRegistrationTime = _settingsManager.GetRegistrationSettings();

            var reservationStatus = GetReservationStatus(clinicRegistrationTime.StartTime, clinicRegistrationTime.EndTime);

            var countNewNotices = GetCountNewNotices(currentUser);

            var answer = new GetClinicUserMainPageInformationCommandAnswer
            {
                Token = (Guid) command.Token,
                UserName = currentUser.Name,
                StartTimeReservation = clinicRegistrationTime.StartTime,
                EndTimeReservation = clinicRegistrationTime.EndTime,
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
