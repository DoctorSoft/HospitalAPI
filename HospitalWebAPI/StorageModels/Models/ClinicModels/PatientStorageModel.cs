using StorageModels.Interfaces;

namespace StorageModels.Models.ClinicModels
{
    public class PatientStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public int Code { get; set; }

        //

        public ReservationStorageModel Reservation { get; set; }
    }
}
