using StorageModels.Interfaces;

namespace StorageModels.Models.ClinicModels
{
    public class ReservationFileStorageModel: IIdModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ReservationId { get; set; }

        public byte[] File { get; set; }

        public ReservationStorageModel Reservation { get; set; }
    }
}
