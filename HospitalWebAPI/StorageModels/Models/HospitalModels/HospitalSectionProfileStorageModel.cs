using System;
using StorageModels.Interfaces;
using System.Collections.Generic;
using StorageModels.Models.UserModels;

namespace StorageModels.Models.HospitalModels
{
    public class HospitalSectionProfileStorageModel : IIdModel, IBlockAbleModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //

        public string Name { get; set; }

        public bool HasGenderFactor { get; set; }

        //

        public HospitalStorageModel Hospital { get; set; }
        public ICollection<EmptyPlaceStatisticStorageModel> EmptyPlaceStatistics { get; set; }
        public SectionProfileStorageModel SectionProfile { get; set; }
        public ICollection<HospitalUserSectionAccessStorageModel> HospitalUserSectionAccesses { get; set; }

        //

        public int HospitalId { get; set; }
        public int SectionProfileId { get; set; }
    }
}
