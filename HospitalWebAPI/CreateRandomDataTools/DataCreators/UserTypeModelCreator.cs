using System;
using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Enums;
using StorageModels.Models.FunctionModels;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class UserTypeModelCreator : IUserTypeModelCreator
    {
        public IEnumerable<UserTypeStorageModel> GetList()
        {
            var models = Enum.GetValues(typeof (UserType))
                .Cast<UserType>()
                .Select(name => new UserTypeStorageModel
                {
                    Id = 0,
                    UserType = name,
                    IsBlocked = false,
                    Name = name.ToString("F")
                });
            return models;
        }
    }
}
