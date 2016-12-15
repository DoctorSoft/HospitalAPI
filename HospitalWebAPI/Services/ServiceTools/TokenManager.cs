using System;
using System.Data.Entity;
using System.Linq;
using DataBaseTools.Interfaces;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.UserModels;

namespace Services.ServiceTools
{
    public class TokenManager : ITokenManager
    {
        private readonly IDataBaseContext _context;

        public TokenManager(IDataBaseContext context)
        {
            _context = context;
        }

        protected virtual SessionStorageModel GetSession(Guid? token)
        {
            var unblockedSessions = _context.Set<SessionStorageModel>().Where(model => !model.IsBlocked);
            var currentSession = unblockedSessions.FirstOrDefault(model => model.Token == token);

            return currentSession;
        }

        protected virtual UserStorageModel GetUserBySession(SessionStorageModel session)
        {
            var currentAccount = _context.Set<AccountStorageModel>()
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
