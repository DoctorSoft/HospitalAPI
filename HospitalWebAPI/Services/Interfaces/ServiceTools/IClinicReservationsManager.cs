using System;
using System.Collections.Generic;
using StorageModels.Models.ClinicModels;

namespace Services.Interfaces.ServiceTools
{
    public interface IClinicReservationsManager
    {
        IEnumerable<ReservationStorageModel> GetReservationsByDate(ClinicStorageModel clinic, DateTime date);

        IEnumerable<ReservationStorageModel> GetTodaysReservations(ClinicStorageModel clinic);
    }
}
