using System.Collections.Generic;
using System.Linq;
using HandleToolsInterfaces.RepositoryHandlers;
using StorageModels.Interfaces;

namespace HandleTools.RepositoryHandlers
{
    public class BlockAbleHandler : IBlockAbleHandler
    {
        public IEnumerable<T> GetAccessAbleModels<T>(IEnumerable<T> list) where T : IIdModel, IBlockAbleModel
        {
            return list.Where(arg => !arg.IsBlocked);
        }

        public IEnumerable<T> GetBlockedModels<T>(IEnumerable<T> list) where T : IIdModel, IBlockAbleModel
        {
            return list.Where(arg => arg.IsBlocked);
        }

        public T BlockModel<T>(T model) where T : IIdModel, IBlockAbleModel
        {
            model.IsBlocked = true;
            return model;
        }

        public T UnBlockModel<T>(T model) where T : IIdModel, IBlockAbleModel
        {
            model.IsBlocked = false;
            return model;
        }
    }
}
