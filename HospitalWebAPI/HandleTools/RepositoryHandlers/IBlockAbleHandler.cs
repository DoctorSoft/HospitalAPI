using System.Collections.Generic;
using StorageModels.Interfaces;

namespace HandleTools.RepositoryHandlers
{
    public interface IBlockAbleHandler<T>
        where T: IIdModel, IBlockAbleModel
    {
        IEnumerable<T> GetAccessAbleModels(IEnumerable<T> list);
        IEnumerable<T> GetBlockedModels(IEnumerable<T> list);

        T BlockModel(T model);

        T UnBlockModel(T model);
    }
}
