﻿using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.HospitalModels;

namespace DataBaseModelConfigurations.Configurations.HospitalModels
{
    public class HospitalDataBaseConfiguration: EntityTypeConfiguration<HospitalStorageModel>
    {
        public HospitalDataBaseConfiguration()
        {
            
        }
    }
}