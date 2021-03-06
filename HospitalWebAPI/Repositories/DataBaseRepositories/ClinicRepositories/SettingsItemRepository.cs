﻿using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using StorageModels.Models.ClinicModels;

namespace Repositories.DataBaseRepositories.ClinicRepositories
{
    public class SettingsItemRepository : AbstractAddAbleDataBaseRepository<SettingsItemStorageModel>, ISettingsItemRepository
    {
        public SettingsItemRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
