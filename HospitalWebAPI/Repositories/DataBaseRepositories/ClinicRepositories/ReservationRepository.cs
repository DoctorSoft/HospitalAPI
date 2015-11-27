﻿using DataBaseRepositoryTools.AbstractTools;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using StorageModels.Models.ClinicModels;

namespace Repositories.DataBaseRepositories.ClinicRepositories
{
    public class ReservationRepository : AbstractUpdateAbleDataBaseRepository<ReservationStorageModel>, IReservationRepository
    {
        public ReservationRepository(IDataBaseContext context) : base(context)
        {
        }
    }
}
