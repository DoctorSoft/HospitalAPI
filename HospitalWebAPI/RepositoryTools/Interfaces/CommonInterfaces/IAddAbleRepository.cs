using StorageModels.Interfaces;

namespace RepositoryTools.Interfaces.CommonInterfaces
{
    public interface IAddAbleRepository<T> : IUpdateAbleRepository<T>
        where T : class, IIdModel
    {
        void Add(T model);
    }
}
