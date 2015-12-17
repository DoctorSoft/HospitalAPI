using Enums.Enums;
using StorageModels.Models.FunctionModels;

namespace ServiceModels.ModelTools.Entities
{
    public class FunctionAccess
    {
        public FunctionStorageModel FunctionStorageModel { get; set; }

        public AccessType AccessType { get; set; } 
    }
}
