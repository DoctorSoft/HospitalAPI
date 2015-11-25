using System.Collections.Generic;
using StorageModels.Interfaces;

namespace RepositoryTools.Interfaces
{
    public interface IBlockAbleRepository<T> : IAddAbleRepository<T>
        where T: IIdModel, IBlockAbleModel
    {
        IEnumerable<T> GetBlockedModels();

        void BlockModel(int id);

        void UnBlockModel(int id);
    }
}
