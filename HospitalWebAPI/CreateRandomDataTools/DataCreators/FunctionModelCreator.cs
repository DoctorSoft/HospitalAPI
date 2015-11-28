using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.FunctionModels;

namespace CreateRandomDataTools.DataCreators
{
    public class FunctionModelCreator : IFunctionModelCreator
    {
        public IEnumerable<FunctionStorageModel> GetList()
        {
            var firstFunction = new FunctionStorageModel
            {
                IsBlocked = false,
                Id = 0,
                Name = "Function 1"
            };

            return new List<FunctionStorageModel>
            {
                firstFunction
            };
        }
    }
}
