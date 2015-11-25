using StorageModels.Interfaces;

namespace RepositoryTools.Interfaces
{
    public interface IUpdateAbleRepository<T> : IReadOnlyRepository<T> 
        where T : IIdModel
    {
        void Update(int id, T model);
    }
}
