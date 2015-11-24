﻿using StorageModels.Interfaces;
using System.Collections.Generic;

namespace StorageModels.Models.HospitalModels
{
    public class HospitalSectionProfileStorageModel : IIdModel, IBlockableModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //

        public HospitalStorageModel Hospital { get; set; }
        public IEnumerable<EmptyPlaceStatisticStorageModel> EmptyPlaceStatistics { get; set; }
        public SectionProfileStorageModel SectionProfile { get; set; }
    }
}
