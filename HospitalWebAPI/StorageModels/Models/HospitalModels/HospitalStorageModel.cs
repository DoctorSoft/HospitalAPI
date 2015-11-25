using StorageModels.Interfaces;
using StorageModels.Models.UserModels;
using System.Collections.Generic;

namespace StorageModels.Models.HospitalModels
{
    public class HospitalStorageModel : IIdModel, IBlockAbleModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //

        public string Name { get; set; }
        public string Address { get; set; }

        //

        public ICollection<HospitalUserStorageModel> HospitalUsers { get; set; }
        public ICollection<HospitalSectionProfileStorageModel> HospitalSectionProfiles { get; set; }

    }
}
