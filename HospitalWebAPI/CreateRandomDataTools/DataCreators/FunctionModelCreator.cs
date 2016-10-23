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
            var blocked = new[] {FunctionIdentityName.HospitalUserMakeRegistrations};

            return Enum.GetValues(typeof (FunctionIdentityName))
                .Cast<FunctionIdentityName>()
                .Select(name => new FunctionStorageModel
                {
                    FunctionIdentityName = name,
                    IsBlocked = blocked.Contains(name),
                    Name = name.ToString("F")
                });
        }
    }
}
