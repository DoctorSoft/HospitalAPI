using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class HospitalUserModelCreator : IHospitalUserModelCreator
    {
        public IEnumerable<HospitalUserStorageModel> GetList()
        {
            return null;
        }
    }
}
