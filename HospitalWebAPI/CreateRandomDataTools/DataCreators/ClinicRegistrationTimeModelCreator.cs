using System;
using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.ClinicModels;

namespace CreateRandomDataTools.DataCreators
{
    public class ClinicRegistrationTimeModelCreator : IClinicRegistrationTimeModelCreator

    {
        public IEnumerable<ClinicRegistrationTimeStorageModel> GetList()
        {
            var clinicRegistrationTimeList = new List<ClinicRegistrationTimeStorageModel>
            {
                new ClinicRegistrationTimeStorageModel
                {
                    DateCreate = DateTime.Now,
                    StartTime = "10:00",
                    EndTime = "19:00"
                }
            };

            return clinicRegistrationTimeList;
        }
    }
}
