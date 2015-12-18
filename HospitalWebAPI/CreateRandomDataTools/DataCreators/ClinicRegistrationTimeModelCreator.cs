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
                    StartTime = new TimeSpan(0, 10, 0, 0),
                    EndTime = new TimeSpan(0, 19, 0, 0)
                }
            };

            return clinicRegistrationTimeList;
        }
    }
}
