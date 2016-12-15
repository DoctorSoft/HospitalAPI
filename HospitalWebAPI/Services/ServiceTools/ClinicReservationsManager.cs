using System;
using System.Collections.Generic;
using System.Linq;
using DataBaseTools.Interfaces;
using Enums.Enums;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.ClinicModels;

namespace Services.ServiceTools
{
    public class ClinicReservationsManager : IClinicReservationsManager
    {
        private readonly IDataBaseContext _context;

        public ClinicReservationsManager(IDataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<ReservationStorageModel> GetReservationsByDate(ClinicStorageModel clinic, DateTime date)
        {
            var reservations = _context.Set<ReservationStorageModel>()
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
