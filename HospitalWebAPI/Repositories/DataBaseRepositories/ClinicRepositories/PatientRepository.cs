﻿using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using StorageModels.Models.ClinicModels;

namespace Repositories.DataBaseRepositories.ClinicRepositories
{
    public class PatientRepository : AbstractUpdateAbleDataBaseRepository<PatientStorageModel>, IPatientRepository
    {
        public PatientRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
