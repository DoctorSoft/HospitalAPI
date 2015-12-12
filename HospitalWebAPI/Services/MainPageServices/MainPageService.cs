using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Enums.Enums;
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


        public MainPageService(ISessionRepository sessionRepository, IAccountRepository accountRepository, IUserTypeRepository userTypeRepository)
        {
            _sessionRepository = sessionRepository;
            _accountRepository = accountRepository;
            _userTypeRepository = userTypeRepository;
        }

        protected virtual SessionStorageModel GetSession(GetUserMainPageInformationCommand command)
        {
            var currentSession = _sessionRepository.GetModels().FirstOrDefault(model => model.Token == command.Token);

            return currentSession;
        }

        protected virtual UserStorageModel GetUserBySession(SessionStorageModel session)
        {
            var currentUser = ((IDbSet<AccountStorageModel>)
            _accountRepository.GetModels())
            .Include(model => model.User)
            .FirstOrDefault(model => model.Id == session.AccountId)
            .User;

            return currentUser;
        }

        protected virtual UserType GetUserType(UserStorageModel user)
        {
            var userType = _userTypeRepository.GetModels().FirstOrDefault(model => model.Id == user.UserTypeId);

            return userType.UserType;
        }

        // TODO: Implement this method
        public GetUserMainPageInformationCommandAnswer GetUserMainPageInformation(GetUserMainPageInformationCommand command)
        {
            var currentSession = GetSession(command);

            var currentUser = GetUserBySession(currentSession);
            var userType = GetUserType(currentUser);
            UserAccountType resultUserType;

            switch (userType)
            {

                case UserType.ClinicUser: resultUserType = UserAccountType.ClinicUserAccount; break;
                case UserType.HospitalUser: resultUserType = UserAccountType.HospitalUserAccount; break;
                case UserType.Bot: resultUserType = UserAccountType.None; break;
                case UserType.Administrator: resultUserType = UserAccountType.AdministratorAccount; break;
                case UserType.Reviewer: resultUserType = UserAccountType.ReviewerAccount; break;
                default:
                    resultUserType = UserAccountType.None;
                    break;
            }
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
