﻿using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using StorageModels.Models.HospitalModels;

namespace Repositories.DataBaseRepositories.HospitalRepositories
{
    public class EmptyPlaceByTypeStatisticRepository : AbstractAddAbleDataBaseRepository<EmptyPlaceByTypeStatisticStorageModel>, IEmptyPlaceByTypeStatisticRepository
    {
        public EmptyPlaceByTypeStatisticRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
