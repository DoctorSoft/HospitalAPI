using System;
using StorageModels.Interfaces;

namespace StorageModels.Models.ClinicModels
{
    public class ClinicRegistrationTimeStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime DateCreate { get; set; }

    }
}
