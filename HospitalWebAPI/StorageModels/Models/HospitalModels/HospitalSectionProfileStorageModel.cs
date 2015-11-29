﻿using System;
using StorageModels.Interfaces;
using System.Collections.Generic;

namespace StorageModels.Models.HospitalModels
{
    public class HospitalSectionProfileStorageModel : IIdModel, IBlockAbleModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //

        public DateTime Date { get; set; }

        //

        public HospitalStorageModel Hospital { get; set; }
        public ICollection<EmptyPlaceStatisticStorageModel> EmptyPlaceStatistics { get; set; }
        public SectionProfileStorageModel SectionProfile { get; set; }

        //

        public int HospitalId { get; set; }
        public int SectionProfileId { get; set; }
    }
}
