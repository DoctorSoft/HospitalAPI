using StorageModels.Interfaces;
using System.Collections.Generic;

namespace StorageModels.Models.FunctionModels
{
    public class DistributiveGroupStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public string Name { get; set; }

        //

        public IEnumerable<GroupFunctionStorageModel> GroupFunctions { get; set; }
    }
}
