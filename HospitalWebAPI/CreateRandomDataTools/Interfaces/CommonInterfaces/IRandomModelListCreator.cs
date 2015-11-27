using System.Collections.Generic;
using StorageModels.Interfaces;

namespace CreateRandomDataTools.Interfaces.CommonInterfaces
{
    public interface IRandomModelListCreator<T>
        where T: IIdModel
    {
        IEnumerable<T> GetList();
    }
}
