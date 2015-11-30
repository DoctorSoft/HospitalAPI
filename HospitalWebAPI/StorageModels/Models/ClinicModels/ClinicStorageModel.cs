using StorageModels.Interfaces;
using StorageModels.Models.UserModels;
using System.Collections.Generic;

namespace StorageModels.Models.ClinicModels
{
    public class ClinicStorageModel : IIdModel, IBlockAbleModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //

        public string Name { get; set; }
        public string Address { get; set; }

        //

        public ICollection<ClinicUserStorageModel> ClinicUsers { get; set; }
        public ICollection<ReservationStorageModel> Reservations { get; set; }
        public ICollection<ClinicHospitalAccessStorageModel> ClinicHospitalAccesses { get; set; }
    }
}
