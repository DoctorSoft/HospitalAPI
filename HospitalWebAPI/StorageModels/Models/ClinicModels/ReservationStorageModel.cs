using StorageModels.Enums;
using StorageModels.Interfaces;
using StorageModels.Models.HospitalModels;

namespace StorageModels.Models.ClinicModels
{
    public class ReservationStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public ReservationStatus Status { get; set; }

        //

        public EmptyPlaceStatisticStorageModel EmptyPlaceStatistic { get; set; }
        public ClinicStorageModel Clinic { get; set; }
        public PatientStorageModel Patient { get; set; }
    }
}
