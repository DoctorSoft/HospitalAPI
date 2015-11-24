using StorageModels.Interfaces;
using StorageModels.Models.ClinicModels;
using System.Collections.Generic;

namespace StorageModels.Models.HospitalModels
{
    public class EmptyPlaceStatisticStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public int ManRoomCount { get; set; }
        public int WoomanRoomCount { get; set; }

        //

        public HospitalSectionProfileStorageModel HospitalSectionProfile { get; set; }
        public IEnumerable<ReservationStorageModel> Reservations { get; set; }
    }
}
