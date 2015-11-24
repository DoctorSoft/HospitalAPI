using StorageModels.Interfaces;
using System.Collections.Generic;

namespace StorageModels.Models.HospitalModels
{
    public class SectionProfileStorageModel : IIdModel, IBlockableModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //

        public string Name { get; set; }

        //

        public IEnumerable<HospitalSectionProfileStorageModel> HospitalSectionProfiles { get; set; }
        public SectionStorageModel Section { get; set; }
    }
}
