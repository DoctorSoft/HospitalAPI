using System;
using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using Enums.Enums;
using StorageModels.Models.FunctionModels;

namespace CreateRandomDataTools.DataCreators
{
    public class FunctionModelCreator : IFunctionModelCreator
    {
        public IEnumerable<FunctionStorageModel> GetList()
        {
            return Enum.GetValues(typeof (FunctionIdentityName))
                .Cast<FunctionIdentityName>()
                .Select(name => new FunctionStorageModel
                {
                    FunctionIdentityName = name,
                    IsBlocked = false,
                    Name = name.ToString("F")
                });
        }
    }
}
