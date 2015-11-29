using StorageModels.Interfaces;
using System.Collections.Generic;

namespace StorageModels.Models.HospitalModels
{
    public class SectionProfileStorageModel : IIdModel, IBlockAbleModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //

        public string Name { get; set; }

        //

        public ICollection<HospitalSectionProfileStorageModel> HospitalSectionProfiles { get; set; }
        public SectionStorageModel Section { get; set; }

        //

        public int SectionId { get; set; }
    }
}
