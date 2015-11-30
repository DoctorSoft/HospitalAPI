using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class UserTypeModelCreator : IUserTypeModelCreator
    {
        public IEnumerable<UserTypeStorageModel> GetList()
        {
            return null;
        }
    }
}
