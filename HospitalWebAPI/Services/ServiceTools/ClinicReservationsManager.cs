using System;
using System.Collections.Generic;
using System.Linq;
using Enums.Enums;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.ClinicModels;

namespace Services.ServiceTools
{
    public class ClinicReservationsManager : IClinicReservationsManager
    {
        private readonly IReservationRepository _reservationRepository;

        public ClinicReservationsManager(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public IEnumerable<ReservationStorageModel> GetReservationsByDate(ClinicStorageModel clinic, DateTime date)
        {
            var reservations = _reservationRepository.GetModels()
                .Where(model => model.ClinicId == clinic.Id && model.ApproveTime.Date == date.Date)
                .Where(model => model.Status == ReservationStatus.Opened)
                .ToList();

            return reservations;
        }

        public IEnumerable<ReservationStorageModel> GetTodaysReservations(ClinicStorageModel clinic)
        {
            var today = DateTime.Now.Date;
            return GetReservationsByDate(clinic, today);
        }
    }
}
