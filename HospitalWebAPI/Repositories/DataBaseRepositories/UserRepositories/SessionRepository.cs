﻿using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.UserModels;

namespace Repositories.DataBaseRepositories.UserRepositories
{
    public class SessionRepository : AbstractAddAbleDataBaseRepository<SessionStorageModel>, ISessionRepository
    {
        public SessionRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
