using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DataBaseTools.Interfaces;
using HelpingTools.Interfaces;
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
        private readonly IPasswordHashManager _passwordHashManager;

        private const string ErrorFieldName = "Login";

        private readonly IDataBaseContext _context;

        public AuthorizationService(IPasswordHashManager passwordHashManager, IDataBaseContext context)
        {
            _passwordHashManager = passwordHashManager;
            _context = context;
        }

        protected virtual AccountStorageModel GetUserAccountByCommand(GetTokenByUserCredentialsCommand command)
        {
            return _context.Set<AccountStorageModel>().FirstOrDefault(model => model.Login == command.Login);
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
            _context.Set<SessionStorageModel>().Add(newSession);
            _context.SaveChanges();
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

            var session = _context.Set<SessionStorageModel>().FirstOrDefault(model => model.Token == token);
            session.IsBlocked = true;

            _context.Set<SessionStorageModel>().AddOrUpdate(session);
            _context.SaveChanges();
            
            return new LogOutUserByTokenCommandAnswer();
        }
    }
}
