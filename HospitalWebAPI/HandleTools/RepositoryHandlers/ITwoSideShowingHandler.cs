using System.Collections.Generic;
using StorageModels.Interfaces;

namespace HandleTools.RepositoryHandlers
{
    public interface ITwoSideShowingHandler<T>
        where T: IIdModel, IShowStatusModel
    {
        IEnumerable<T> GetFirstSideModels(IEnumerable<T> list);

        IEnumerable<T> GetSecondSideModels(IEnumerable<T> list);

        IEnumerable<T> GetHiddenModels(IEnumerable<T> list);

        T HideModeFromFirstSide(T model);

        T HideModelFromSecondSide(T model);

        T HideModel(T model);

        T ShowModelForFirstSide(T model);

        T ShowModelForSecondSide(T model);

        T ShowModel(T model);
    }
}
