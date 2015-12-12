using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Enums.Enums;
using HandleToolsInterfaces.Converters;
using HandleToolsInterfaces.RepositoryHandlers;
using HelpingTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers;
using ServiceModels.ServiceCommands.MainPageCommands;
using Services.Interfaces.MainPageServices;
using StorageModels.Models.UserModels;

namespace Services.MainPageServices
{
    public class MainPageService : IMainPageService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserTypeRepository _userTypeRepository;

        private readonly IUserToAccountTypeConverter _userToAccountTypeConverter;
        private readonly IBlockAbleHandler _blockAbleHandler;


        public MainPageService(ISessionRepository sessionRepository, IAccountRepository accountRepository, IUserTypeRepository userTypeRepository, IUserToAccountTypeConverter userToAccountTypeConverter, IBlockAbleHandler blockAbleHandler)
        {
            _sessionRepository = sessionRepository;
            _accountRepository = accountRepository;
            _userTypeRepository = userTypeRepository;
            _userToAccountTypeConverter = userToAccountTypeConverter;
            _blockAbleHandler = blockAbleHandler;
        }

        protected virtual SessionStorageModel GetSession(GetUserMainPageInformationCommand command)
        {
            var currentSession = _blockAbleHandler.GetAccessAbleModels(_sessionRepository.GetModels())
                .FirstOrDefault(model => model.Token == command.Token);

            return currentSession;
        }

        protected virtual UserStorageModel GetUserBySession(SessionStorageModel session)
        {
            var currentAccount = _blockAbleHandler.GetAccessAbleModels(((IDbSet<AccountStorageModel>)
            _accountRepository.GetModels())
            .Include(model => model.User))
            .FirstOrDefault(model => model.Id == session.AccountId);

            if (currentAccount == null)
            {
                return null;
            }

            return currentAccount.User;
        }

        protected virtual UserType GetUserType(UserStorageModel user)
        {
            var userType = _userTypeRepository.GetModels().FirstOrDefault(model => model.Id == user.UserTypeId);

            return userType.UserType;
        }

        public GetUserMainPageInformationCommandAnswer GetUserMainPageInformation(GetUserMainPageInformationCommand command)
        {
            var currentSession = GetSession(command);

            if (currentSession == null)
            {
                return new GetUserMainPageInformationCommandAnswer
                {
                    UserType = UserAccountType.None,
                };
            }

            var currentUser = GetUserBySession(currentSession);

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
            var answer = new GetClinicUserMainPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
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
