using StorageModels.Interfaces;

namespace RepositoryTools.Interfaces
{
    public interface IAddAbleRepository<T> : IUpdateAbleRepository<T>
        where T : IIdModel
    {
        void Add(T model);
    }
}
