using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class AdministratorModelCreator : IAdministratorModelCreator
    {
        public IEnumerable<UserStorageModel> GetList()
        {
            return null;
        }
    }
}
