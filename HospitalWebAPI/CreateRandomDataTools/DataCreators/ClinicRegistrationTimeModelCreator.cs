using System;
using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.ClinicModels;

namespace CreateRandomDataTools.DataCreators
{
    public class ClinicRegistrationTimeModelCreator : IClinicRegistrationTimeModelCreator

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
