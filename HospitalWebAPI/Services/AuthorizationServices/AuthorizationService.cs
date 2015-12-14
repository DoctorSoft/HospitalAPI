using System;
using System.Collections.Generic;
using System.Linq;
using HandleToolsInterfaces.RepositoryHandlers;
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
        private readonly IBlockAbleHandler _blockAbleHandler;

        private const string ErrorFieldName = "Login";

        public AuthorizationService(IAccountRepository accountRepository, ISessionRepository sessionRepository, IPasswordHashManager passwordHashManager, IBlockAbleHandler blockAbleHandler)
        {
            _accountRepository = accountRepository;
            _sessionRepository = sessionRepository;

            _passwordHashManager = passwordHashManager;
            _blockAbleHandler = blockAbleHandler;
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
                    Title = MainResources.ErrorAuthorization_401,
                    FieldName = ErrorFieldName
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

            return _passwordHashManager.IsCorrectPassword(command.Password, userAccount.HashedPassword);
        }

        protected virtual SessionStorageModel GetNewSession(AccountStorageModel userAccount)
        {
            var newToken = Guid.NewGuid();
            var startTime = DateTime.Now;

            var newSession = new SessionStorageModel
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

        protected bool IsRightCommandData(GetTokenByUserCredentialsCommand command)
        {
            return !(string.IsNullOrWhiteSpace(command.Login) || string.IsNullOrWhiteSpace(command.Password));
        }

        public GetTokenByUserCredentialsCommandAnswer GetTokenByUserCredentials(GetTokenByUserCredentialsCommand command)
        {
            var errorAnswer = GetErrorAnswer();

            if (!IsRightCommandData(command))
            {
                return errorAnswer;
            }

            var userAccount = GetUserAccountByCommand(command);

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

        public LogOutUserByTokenCommandAnswer LogOutUserByToken(LogOutUserByTokenCommand command)
        {
            var token = command.Token;

            var session = _sessionRepository.GetModels().FirstOrDefault(model => model.Token == token);
            _blockAbleHandler.BlockModel(session);

            _sessionRepository.Update(session.Id, session);
            _sessionRepository.SaveChanges();
            
            return new LogOutUserByTokenCommandAnswer();
        }
    }
}
