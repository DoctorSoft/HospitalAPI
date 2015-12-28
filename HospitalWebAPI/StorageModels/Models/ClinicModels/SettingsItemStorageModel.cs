using System;
using StorageModels.Interfaces;

namespace StorageModels.Models.ClinicModels
{
    public class SettingsItemStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public DateTime DateCreate { get; set; }

    }
}
