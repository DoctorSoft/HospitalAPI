using StorageModels.Interfaces;
using StorageModels.Models.UserModels;

namespace StorageModels.Models.FunctionModels
{
    public class UserFunctionStorageModel : IIdModel, IBlockableModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //

        public UserStorageModel User { get; set; }
        public FunctionStorageModel Function { get; set; }
    }
}
