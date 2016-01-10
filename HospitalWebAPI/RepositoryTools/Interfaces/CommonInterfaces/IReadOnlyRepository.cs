using System.Linq;
using StorageModels.Interfaces;

namespace RepositoryTools.Interfaces.CommonInterfaces
{
    public interface IReadOnlyRepository<T>
        where T: class, IIdModel
    {
        IQueryable<T> GetModels();

        T GetModelById(int id);
    }
}
