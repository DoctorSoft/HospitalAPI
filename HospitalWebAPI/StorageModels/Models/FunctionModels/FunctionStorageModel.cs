using StorageModels.Enums;
using StorageModels.Interfaces;
using System.Collections.Generic;

namespace StorageModels.Models.FunctionModels
{
    public class FunctionStorageModel : IIdModel, IBlockAbleModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //

        public string Name { get; set; }
        public FunctionIdentityName FunctionIdentityName { get; set; }

        //

        public ICollection<UserFunctionStorageModel> UserFunctions { get; set; }
        public ICollection<GroupFunctionStorageModel> GroupFunctions { get; set; }
    }
}
