using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.FunctionModels;

namespace CreateRandomDataTools.DataCreators
{
    public class UserFunctionModelCreator : IUserFunctionModelCreator
    {
        public IEnumerable<UserFunctionStorageModel> GetList()
        {
            return null;
        }
    }
}
