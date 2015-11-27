using System.Collections.Generic;
using StorageModels.Interfaces;

namespace RepositoryTools.Interfaces.CommonInterfaces
{
    public interface IReadOnlyRepository<T>
        where T: IIdModel
    {
        IEnumerable<T> GetModels();

        T GetModelById(int id);
    }
}
