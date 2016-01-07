using System.Collections.Generic;
using StorageModels.Interfaces;

namespace HandleToolsInterfaces.RepositoryHandlers
{
    public interface ITwoSideShowingHandler<T>
        where T: IIdModel, IShowStatusModel
    {
        IEnumerable<T> GetFromSideModels(IEnumerable<T> list);

        IEnumerable<T> GetToSideModels(IEnumerable<T> list);

        IEnumerable<T> GetHiddenModels(IEnumerable<T> list);

        T HideModeFromFromSide(T model);

        T HideModelFromToSide(T model);

        T HideModel(T model);

        T ShowModelForFromSide(T model);

        T ShowModelForToSide(T model);

        T ShowModel(T model);
    }
}
