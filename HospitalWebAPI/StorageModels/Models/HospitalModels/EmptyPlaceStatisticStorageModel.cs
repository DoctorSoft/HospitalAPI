﻿using System;
using StorageModels.Interfaces;
using StorageModels.Models.ClinicModels;
using System.Collections.Generic;

namespace StorageModels.Models.HospitalModels
{
    public class EmptyPlaceStatisticStorageModel : IIdModel
    {
        public int Id { get; set; }

        //
        public DateTime Date { get; set; }
        public DateTime? CreateTime { get; set; }

        //

        public HospitalSectionProfileStorageModel HospitalSectionProfile { get; set; }
        public ICollection<EmptyPlaceByTypeStatisticStorageModel> EmptyPlaceByTypeStatistics { get; set; }

        //

        public int HospitalSectionProfileId { get; set; }

    }
}
