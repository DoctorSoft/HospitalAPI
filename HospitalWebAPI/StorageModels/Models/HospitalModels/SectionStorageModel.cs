using StorageModels.Interfaces;
using System.Collections.Generic;

namespace StorageModels.Models.HospitalModels
{
    public class SectionStorageModel : IIdModel, IBlockAbleModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        // 

        public string Name { get; set; }

        //

        public IEnumerable<SectionProfileStorageModel> SectionProfiles { get; set; }
    }
}
