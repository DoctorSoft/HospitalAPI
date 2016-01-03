using System;
using System.Collections.Generic;
using Enums.Enums;
using ServiceModels.ModelTools.Entities;
using StorageModels.Models.FunctionModels;

namespace Services.Interfaces.ServiceTools
{
    public interface IUserFunctionManager
    {
        IEnumerable<FunctionAccess> GetFunctionsByToken(Guid? token);
        bool IsFunctionsEnabled(FunctionIdentityName function, Guid token);
    }
}
