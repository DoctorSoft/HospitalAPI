using StorageModels.Enums;
using StorageModels.Interfaces;
using System.Collections.Generic;

namespace StorageModels.Models.FunctionModels
{
    public class FunctionStorageModel : IIdModel, IBlockableModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //

        public string Name { get; set; }
        public FunctionIdentityName FunctionIdentityName { get; set; }

        //

        public IEnumerable<UserFunctionStorageModel> UserFunctions { get; set; }
        public IEnumerable<GroupFunctionStorageModel> GroupFunctions { get; set; }
    }
}
