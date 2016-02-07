using System;
using Enums.Enums;
using StorageModels.Interfaces;
using StorageModels.Models.HospitalModels;
using StorageModels.Models.UserModels;

namespace StorageModels.Models.ClinicModels
{
    public class ReservationStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public ReservationStatus Status { get; set; }
        public DateTime ApproveTime { get; set; }
        public DateTime? CancelTime { get; set; }
        public string Diagnosis { get; set; }

        //

        public EmptyPlaceByTypeStatisticStorageModel EmptyPlaceByTypeStatistic { get; set; }
        public ClinicStorageModel Clinic { get; set; }
        public PatientStorageModel Patient { get; set; }
        public UserStorageModel Reservator { get; set; }
        public UserStorageModel BehalfReservator { get; set; }

        //

        public int ClinicId { get; set; }
        public int EmptyPlaceByTypeStatisticId { get; set; }
        public int ReservatorId { get; set; }
        public int? BehalfReservatorId { get; set; }

    }
}
