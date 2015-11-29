using RepositoryTools.Interfaces.CommonInterfaces;
using StorageModels.Models.AnotherModels.RemoteAPIModels;

namespace RepositoryTools.Interfaces.PrivateInterfaces.AnotherRepositories.RemoteAPIRepositories
{
    public interface IPersonDataAPIRepository : IReadOnlyRepository<PersonDataAPIStorageModel>
    {
    }
}
