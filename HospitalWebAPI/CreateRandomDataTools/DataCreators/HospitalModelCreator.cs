using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.HospitalModels;

namespace CreateRandomDataTools.DataCreators
{
    public class HospitalModelCreator : IHospitalModelCreator
    {
        public IEnumerable<HospitalStorageModel> GetList()
        {
            var hospitalsList = new List<HospitalStorageModel>
            {
                new HospitalStorageModel
                {
                    IsBlocked = false,
                    Id = 0,
                    Name = "Городская клиническая больница №3",
                    Address = "г. Гродно, ул. БЛК, 59",
                    IsForChildren = true
                },
            };

            return hospitalsList;
        }
    }
}
