using CreateRandomDataTools.Interfaces.CommonInterfaces;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.Interfaces.PrivateInterfaces
{
    public interface IBotModelCreator : IRandomModelListCreator<UserStorageModel>
    {
    }
}
