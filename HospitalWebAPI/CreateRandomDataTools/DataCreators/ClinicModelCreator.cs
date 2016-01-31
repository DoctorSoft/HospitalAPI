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
                    Name = "Гродненская центральная городская поликлиника",
                    Address = "г. Гродно, ул. Транспортная, 3"
                },
                new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 1,
                    Name = "Городская поликлиника №1",
                    Address = "г. Гродно, ул. Лермонтова, 13"
                },
                /*new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 2,
                    Name = "Городская поликлиника №3",
                    Address = "г. Гродно, ул. Пестрака, 4"
                },
                new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 3,
                    Name = "Детская поликлиника № 2",
                    Address = "г. Гродно, ул. Гагарина, 18"
                },
                new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 4,
                    Name = "Городская поликлиника №4",
                    Address = "г. Гродно, ул. Врублевского, 46/1"
                },
                new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 5,
                    Name = "Городская поликлиника №6",
                    Address = "г. Гродно, ул. Горького, 77"
                }*/
            };

            return clinicsList;
        }
    }
}
