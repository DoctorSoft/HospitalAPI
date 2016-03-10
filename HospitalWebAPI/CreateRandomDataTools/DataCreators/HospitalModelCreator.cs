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
                    Name = "Гродненская областная детская клиническая больница",
                    Address = "г. Гродно, ул. Островского, 22",
                    IsForChildren = true
                },
            };

            return hospitalsList;
        }
    }
}
