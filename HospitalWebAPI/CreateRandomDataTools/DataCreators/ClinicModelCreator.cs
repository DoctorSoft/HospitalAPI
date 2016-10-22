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
                    Name = "Гродненская городская поликлиника №4",
                    Address = "г.Гродно, ул. Врублевского, 46/1",
                    IsForChildren = false
                },
                new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 0,
                    Name = "Гродненская городская поликлиника №6",
                    Address = "г.Гродно, ул. Лиможа, 25",
                    IsForChildren = false
                },
               
            };

            return clinicsList;
        }
    }
}
