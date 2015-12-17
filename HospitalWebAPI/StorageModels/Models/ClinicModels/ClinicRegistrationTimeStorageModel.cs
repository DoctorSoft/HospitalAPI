using System;
using StorageModels.Interfaces;

namespace StorageModels.Models.ClinicModels
{
    public class ClinicRegistrationTimeStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime DateCreate { get; set; }

    }
}
