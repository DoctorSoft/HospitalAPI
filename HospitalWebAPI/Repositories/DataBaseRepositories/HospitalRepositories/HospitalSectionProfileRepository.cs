﻿using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using StorageModels.Models.HospitalModels;

namespace Repositories.DataBaseRepositories.HospitalRepositories
{
    public class HospitalSectionProfileRepository : AbstractUpdateAbleDataBaseRepository<HospitalSectionProfileStorageModel>, IHospitalSectionProfileRepository
    {
        public HospitalSectionProfileRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
