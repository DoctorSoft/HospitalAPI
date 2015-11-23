using StorageModels.Interfaces;

namespace StorageModels.Models.FunctionModels
{
    public class UserFunctionStorageModel : IIdModel, IBlockableModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }
    }
}
