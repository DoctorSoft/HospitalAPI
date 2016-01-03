using System;
using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.ClinicModels;

namespace CreateRandomDataTools.DataCreators
{
    public class SettingsItemCreator : ISettingsItemCreator

    {
        public IEnumerable<SettingsItemStorageModel> GetList()
        {
            var clinicRegistrationTimeList = new List<SettingsItemStorageModel>
            {
                new SettingsItemStorageModel
                {
                    DateCreate = DateTime.Now
                }
            };

            return clinicRegistrationTimeList;
        }
    }
}
