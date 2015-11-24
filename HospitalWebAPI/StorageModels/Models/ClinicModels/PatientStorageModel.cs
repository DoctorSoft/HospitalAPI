using StorageModels.Interfaces;

namespace StorageModels.Models.ClinicModels
{
    public class PatientStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public string Code { get; set; }

        //

        public ReservationStorageModel Reservation { get; set; }
    }
}
