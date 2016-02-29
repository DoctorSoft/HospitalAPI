using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.ClinicModels;

namespace CreateRandomDataTools.DataCreators
{
    public class ClinicModelCreator : IClinicModelCreator
    {
        public IEnumerable<ClinicStorageModel> GetList()
        {
            var clinicsList = new List<ClinicStorageModel>
            {
                new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 0,
                    Name = "Гродненская детская поликлиника №1",
                    Address = "г. Гродно, ул. Даватора, 23"
                },
            };

            return clinicsList;
        }
    }
}
