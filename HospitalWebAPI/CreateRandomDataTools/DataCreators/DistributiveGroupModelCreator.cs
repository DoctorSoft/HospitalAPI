using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.FunctionModels;

namespace CreateRandomDataTools.DataCreators
{
    public class DistributiveGroupModelCreator : IDistributiveGroupModelCreator
    {
        public IEnumerable<DistributiveGroupStorageModel> GetList()
        {
            var firstDistributiveGroup = new DistributiveGroupStorageModel
            {
                Id = 0,
                Name = "DistributiveGroup 1"
            };

            return new List<DistributiveGroupStorageModel>
            {
                firstDistributiveGroup
            };
        }
    }
}
