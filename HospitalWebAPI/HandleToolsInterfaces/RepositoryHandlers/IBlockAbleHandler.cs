using System.Collections.Generic;
using StorageModels.Interfaces;

namespace HandleToolsInterfaces.RepositoryHandlers
{
    public interface IBlockAbleHandler  
    {
        IEnumerable<T> GetAccessAbleModels<T>(IEnumerable<T> list) where T : IIdModel, IBlockAbleModel;
        IEnumerable<T> GetBlockedModels<T>(IEnumerable<T> list) where T : IIdModel, IBlockAbleModel;

        T BlockModel<T>(T model) where T : IIdModel, IBlockAbleModel;

        T UnBlockModel<T>(T model) where T : IIdModel, IBlockAbleModel;
    }
}
