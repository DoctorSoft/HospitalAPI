using RepositoryTools.Interfaces.CommonInterfaces;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.UserModels;

namespace RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories
{
    public interface IUserTypeRepository : IUpdateAbleRepository<UserTypeStorageModel>
    {
    }
}
