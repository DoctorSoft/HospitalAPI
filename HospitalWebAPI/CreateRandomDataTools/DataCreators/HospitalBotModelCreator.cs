using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class HospitalBotModelCreator : IHospitalBotModelCreator
    {
        public IEnumerable<UserStorageModel> GetList()
        {
            return null;
        }
    }
}
