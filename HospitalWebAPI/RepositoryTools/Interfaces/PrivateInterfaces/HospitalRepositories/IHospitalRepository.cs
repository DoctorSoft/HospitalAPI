﻿using RepositoryTools.Interfaces.CommonInterfaces;
using StorageModels.Models.HospitalModels;

namespace RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories
{
    public interface IHospitalRepository : IUpdateAbleRepository<HospitalStorageModel>
    {
    }
}
