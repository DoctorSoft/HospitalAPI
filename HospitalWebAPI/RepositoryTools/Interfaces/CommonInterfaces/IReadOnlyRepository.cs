using System.Collections.Generic;
using System.Data.Entity;
using StorageModels.Interfaces;

namespace RepositoryTools.Interfaces.CommonInterfaces
{
    public interface IReadOnlyRepository<T>
        where T: class, IIdModel
    {
        IEnumerable<T> GetModels();

        T GetModelById(int id);
    }
}
