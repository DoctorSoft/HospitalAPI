using StorageModels.Interfaces;

namespace StorageModels.Models.FunctionModels
{
    public class GroupFunctionStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public FunctionalGroupStorageModel FunctionalGroup { get; set; }
        public FunctionStorageModel Function { get; set; }

        //

        public int FunctionalGroupId { get; set; }
        public int FunctionId { get; set; }
    }
}
