using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.ClinicModels;

namespace CreateRandomDataTools.DataCreators
{
    public class ClinicModelCreator : IClinicModelCreator
    {
        public IEnumerable<ClinicStorageModel> GetList()
        {
            var firstClinic = new ClinicStorageModel
            {
                IsBlocked = false,
                Id = 0,
                Name = "Clinic 1",
                Address = "Address Clinic 1"
            };

            return new List<ClinicStorageModel>
            {
                firstClinic
            };
        }
    }
}

/*
 * Гродненская центральная городская поликлиника - г. Гродно, ул. Транспортная, 3
 * Городская поликлиника №1 - г. Гродно, ул. Лермонтова, 13
 * Городская поликлиника №3 - г. Гродно, ул. Пестрака, 4
*/