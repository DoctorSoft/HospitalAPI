﻿using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using StorageModels.Models.HospitalModels;

namespace Repositories.DataBaseRepositories.HospitalRepositories
{
    public class SectionProfileRepository : AbstractAddAbleDataBaseRepository<SectionProfileStorageModel>, ISectionProfileRepository
    {
        public SectionProfileRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
