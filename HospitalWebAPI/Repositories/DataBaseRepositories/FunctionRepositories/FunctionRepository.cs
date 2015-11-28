﻿using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using StorageModels.Models.FunctionModels;

namespace Repositories.DataBaseRepositories.FunctionRepositories
{
    public class FunctionRepository : AbstractUpdateAbleDataBaseRepository<FunctionStorageModel>, IFunctionRepository
    {
        public FunctionRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
