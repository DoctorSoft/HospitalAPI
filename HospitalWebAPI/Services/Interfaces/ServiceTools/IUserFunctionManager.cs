using System;
using System.Collections.Generic;
using StorageModels.Models.FunctionModels;

namespace Services.Interfaces.ServiceTools
{
    public interface IUserFunctionManager
    {
        IEnumerable<FunctionStorageModel> GetFunctionsByToken(Guid? token);
    }
}
