using CreateRandomDataTools.Interfaces.CommonInterfaces;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.Interfaces.PrivateInterfaces
{
    public interface IAdministratorModelCreator : IRandomModelListCreator<UserStorageModel>
    {
    }
}
