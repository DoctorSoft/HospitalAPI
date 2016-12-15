using System;
using System.Linq;
using DataBaseTools.Interfaces;
using Enums.Enums;
using HandleToolsInterfaces.Converters;
using ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers;
using ServiceModels.ServiceCommands.MainPageCommands;
using Services.Interfaces.MainPageServices;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.MailboxModels;
using StorageModels.Models.UserModels;

namespace Services.MainPageServices
{
    public class MainPageService : IMainPageService
    {
        private readonly IDataBaseContext _context;
        private readonly IUserToAccountTypeConverter _userToAccountTypeConverter;
        private readonly ITokenManager _tokenManager;

        public MainPageService(IUserToAccountTypeConverter userToAccountTypeConverter, ITokenManager tokenManager, IDataBaseContext context)
        {
            _userToAccountTypeConverter = userToAccountTypeConverter;
            _tokenManager = tokenManager;
            _context = context;
        }

        protected virtual UserType GetUserType(UserStorageModel user)
        {
            var userType = _context.Set<UserTypeStorageModel>().FirstOrDefault(model => model.Id == user.UserTypeId);

            return userType.UserType;
        }

        public int? GetCountNewNotices(UserStorageModel user)
        {
            int? countNewNotices =
                _context.Set<MessageStorageModel>()
                    .Where(model => model.UserToId == user.Id 
                        && (model.ShowStatus == TwoSideShowStatus.ToSideOnly || model.ShowStatus == TwoSideShowStatus.Showed))
                        .Count(model => !model.IsRead);
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

            var countNewNotices = GetCountNewNotices(currentUser);

            var answer = new GetClinicUserMainPageInformationCommandAnswer
            {
                Token = (Guid) command.Token,
                UserName = currentUser.Name,
                CountNewNotices = countNewNotices,
                ShowModalWindow = command.ShowModalWindow
            };
            return answer;
        }

        public GetHospitalUserMainPageInformationCommandAnswer GetHospitalUserMainPageInformation(
            GetHospitalUserMainPageInformationCommand command)
        {
            var currentUser = _tokenManager.GetUserByToken(command.Token);

            var countNewNotices = GetCountNewNotices(currentUser);

            var answer = new GetHospitalUserMainPageInformationCommandAnswer
            {
                Token = (Guid)command.Token,
                UserName = currentUser.Name,
                CountNewNotices = countNewNotices,
                ShowModalWindow = command.ShowModalWindow,
                HasDialogMessage = command.HasDialogMessage != null && command.HasDialogMessage.Value,
                DialogMessage = command.DialogMessage
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

        public GetReceptionUserMainPageInformationCommandAnswer GetReceptionUserMainPageInformation(
            GetReceptionUserMainPageInformationCommand command)
        {
            var currentUser = _tokenManager.GetUserByToken(command.Token);

            var answer = new GetReceptionUserMainPageInformationCommandAnswer
            {
                Token = (Guid)command.Token,
                UserName = currentUser.Name
            };
            return answer;
        }
    }
}
