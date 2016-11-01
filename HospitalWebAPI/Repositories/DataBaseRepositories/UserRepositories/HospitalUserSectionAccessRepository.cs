﻿using System.Linq;
using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.UserModels;

namespace Repositories.DataBaseRepositories.UserRepositories
{
    public class HospitalUserSectionAccessRepository : AbstractUpdateAbleDataBaseRepository<HospitalUserSectionAccessStorageModel>, IHospitalUserSectionAccessRepository
    {
        public HospitalUserSectionAccessRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
