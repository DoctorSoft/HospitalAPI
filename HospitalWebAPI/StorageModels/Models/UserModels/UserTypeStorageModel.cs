using System.Collections.Generic;
using StorageModels.Enums;
using StorageModels.Interfaces;
using StorageModels.Models.FunctionModels;

namespace StorageModels.Models.UserModels
{
    public class UserTypeStorageModel : IIdModel, IBlockAbleModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //

        public string Name { get; set; }

        public UserType UserType { get; set; }

        //

        public ICollection<UserStorageModel> Users { get; set; }

        public ICollection<FunctionalGroupStorageModel> FunctionalGroups { get; set; }
    }
}
