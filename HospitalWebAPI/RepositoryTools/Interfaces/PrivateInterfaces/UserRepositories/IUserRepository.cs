using RepositoryTools.Interfaces.CommonInterfaces;
using StorageModels.Models.UserModels;

namespace RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories
{
    public interface IUserRepository : IUpdateAbleRepository<UserStorageModel>
    {
    }
}
