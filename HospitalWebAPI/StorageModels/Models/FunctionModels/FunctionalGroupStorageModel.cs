using StorageModels.Enums;
using StorageModels.Interfaces;
using System.Collections.Generic;
using StorageModels.Models.UserModels;

namespace StorageModels.Models.FunctionModels
{
    public class FunctionalGroupStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public string Name { get; set; }

        //

        public ICollection<GroupFunctionStorageModel> GroupFunctions { get; set; }
        public UserTypeStorageModel UserType { get; set; }

        //

        public int UserTypeId { get; set; }
    }
}
