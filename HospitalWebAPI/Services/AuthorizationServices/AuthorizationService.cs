using System;
using System.Collections.Generic;
using System.Linq;
using HelpingTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using Resources;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.AuthorizationCommandAnswers;
using ServiceModels.ServiceCommands.AuthorizationCommands;
using Services.Interfaces.AuthorizationServices;
using StorageModels.Models.UserModels;

namespace Services.AuthorizationServices
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISessionRepository _sessionRepository;

        private readonly IPasswordHashManager _passwordHashManager;
        public AuthorizationService(IAccountRepository accountRepository, ISessionRepository sessionRepository, IPasswordHashManager passwordHashManager)
        {
            _accountRepository = accountRepository;
            _sessionRepository = sessionRepository;

            _passwordHashManager = passwordHashManager;
        }

        protected virtual AccountStorageModel GetUserAccountByCommand(GetTokenByUserCredentialsCommand command)
        {
            return _accountRepository.GetModels().FirstOrDefault(model => model.Login == command.Login);
        }

        protected virtual GetTokenByUserCredentialsCommandAnswer GetErrorAnswer()
        {
            var authorizationError = new List<CommandAnswerError>
            {
                new CommandAnswerError
                {
                    Code = 401,
                    Title = MainResources.ErrorAuthorization_401
                }
            };

            var errorAnswer = new GetTokenByUserCredentialsCommandAnswer
            {
                Errors = authorizationError
            };

            return errorAnswer;
        }

        protected virtual bool IsRightCredentials(GetTokenByUserCredentialsCommand command, AccountStorageModel userAccount)
        {

            if (userAccount == null)
            {
                return false;
            }

            if (!_passwordHashManager.IsCorrectPassword(command.Password, userAccount.HashedPassword))
            {
                return false;
            }

            return true;
        }

        protected virtual SessionStorageModel GetNewSession(AccountStorageModel userAccount)
        {
            var newToken = Guid.NewGuid();
            var startTime = DateTime.Now;

            var newSession = new SessionStorageModel()
            {
                AccountId = userAccount.Id,
                StartTime = startTime,
                Token = newToken
            };

            return newSession;
        }

        protected virtual void SaveNewSession(SessionStorageModel newSession)
        {
            _sessionRepository.Add(newSession);
            _accountRepository.SaveChanges();
        }

        public GetTokenByUserCredentialsCommandAnswer GetTokenByUserCredentials(GetTokenByUserCredentialsCommand command)
        {
            var userAccount = GetUserAccountByCommand(command);

            var errorAnswer = GetErrorAnswer();

            if (!IsRightCredentials(command, userAccount))
            {
                return errorAnswer;
            }

            var newSession = GetNewSession(userAccount);

            SaveNewSession(newSession);

            return new GetTokenByUserCredentialsCommandAnswer
            {
                Token = newSession.Token
            };
        }
    }
}
