using System.Collections.Generic;
using System.Linq;
using HandleToolsInterfaces.RepositoryHandlers;
using StorageModels.Interfaces;

namespace HandleTools.RepositoryHandlers
{
    public class BlockAbleHandler<T> : IBlockAbleHandler<T>
        where T: IIdModel, IBlockAbleModel
    {
        public IEnumerable<T> GetAccessAbleModels(IEnumerable<T> list)
        {
            return list.Where(arg => !arg.IsBlocked);
        }

        public IEnumerable<T> GetBlockedModels(IEnumerable<T> list)
        {
            return list.Where(arg => arg.IsBlocked);
        }

        public T BlockModel(T model)
        {
            model.IsBlocked = true;
            return model;
        }

        public T UnBlockModel(T model)
        {
            model.IsBlocked = false;
            return model;
        }
    }
}
