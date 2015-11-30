using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class ClinicBotModelCreator : IClinicBotModelCreator
    {
        public IEnumerable<UserStorageModel> GetList()
        {
            return null;
        }
    }
}
