using System;
using StorageModels.Models.UserModels;

namespace Services.Interfaces.ServiceTools
{
    public interface ITokenManager
    {
        UserStorageModel GetUserByToken(Guid? token);
    }
}
