using System.Collections.Generic;
using StorageModels.Interfaces;
using StorageModels.Models.HospitalModels;

namespace StorageModels.Models.UserModels
{
    public class HospitalUserSectionAccessStorageModel : IIdModel, IBlockAbleModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //

        public int HospitalUserId { get; set; }

        public int HospitalSectionProfileId { get; set; }

        //

        public HospitalUserStorageModel HospitalUser { get; set; }

        public HospitalSectionProfileStorageModel HospitalSectionProfile { get; set; }
    }
}
