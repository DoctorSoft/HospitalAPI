using System;
using System.Data.Entity;
using System.Linq;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.UserModels;

namespace Services.ServiceTools
{
    public class TokenManager : ITokenManager
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IAccountRepository _accountRepository;


        public TokenManager(ISessionRepository sessionRepository, IAccountRepository accountRepository)
        {
            _sessionRepository = sessionRepository;
            _accountRepository = accountRepository;
        }

        protected virtual SessionStorageModel GetSession(Guid? token)
        {
            var unblockedSessions = _sessionRepository.GetModels().Where(model => !model.IsBlocked);
            var currentSession = unblockedSessions.FirstOrDefault(model => model.Token == token);

            return currentSession;
        }

        protected virtual UserStorageModel GetUserBySession(SessionStorageModel session)
        {
            var currentAccount = ((IDbSet<AccountStorageModel>)
            _accountRepository.GetModels())
            .Include(model => model.User)
            .Where(model => !model.IsBlocked)
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
