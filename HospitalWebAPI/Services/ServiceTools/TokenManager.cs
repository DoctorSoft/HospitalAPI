using System;
using System.Data.Entity;
using System.Linq;
using HandleToolsInterfaces.RepositoryHandlers;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using ServiceModels.ServiceCommands.SessionCommands;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.UserModels;

namespace Services.ServiceTools
{
    public class TokenManager : ITokenManager
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IAccountRepository _accountRepository;

        private readonly IBlockAbleHandler _blockAbleHandler;

        public TokenManager(ISessionRepository sessionRepository, IAccountRepository accountRepository, IBlockAbleHandler blockAbleHandler)
        {
            _sessionRepository = sessionRepository;
            _accountRepository = accountRepository;
            _blockAbleHandler = blockAbleHandler;
        }

        protected virtual SessionStorageModel GetSession(Guid? token)
        {
            var unblockedSessions = _blockAbleHandler.GetAccessAbleModels(_sessionRepository.GetModels());
            var currentSession = unblockedSessions.FirstOrDefault(model => model.Token == token);

            return currentSession;
        }

        protected virtual UserStorageModel GetUserBySession(SessionStorageModel session)
        {
            var currentAccount = _blockAbleHandler.GetAccessAbleModels(((IDbSet<AccountStorageModel>)
            _accountRepository.GetModels())
            .Include(model => model.User))
            .FirstOrDefault(model => model.Id == session.AccountId);

            return currentAccount == null ? null : currentAccount.User;
        }

        public UserStorageModel GetUserByToken(Guid? token)
        {
            var session = GetSession(token);

            if (session == null)
            {
                return null;
            }

            var user = GetUserBySession(session);

            return user;
        }
    }
}
