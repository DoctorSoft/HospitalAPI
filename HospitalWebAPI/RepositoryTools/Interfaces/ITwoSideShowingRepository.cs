using System.Collections.Generic;
using StorageModels.Interfaces;

namespace RepositoryTools.Interfaces
{
    public interface ITwoSideShowingRepository<T> : IUpdateAbleRepository<T>
        where T: IIdModel, IShowStatusModel
    {
        IEnumerable<T> GetFirstSideModels();

        IEnumerable<T> GetSecondSideModels();

        IEnumerable<T> GetHiddenModels();

        void HideModeFromFirstSide(int id);

        void HideModelFromSecondSide(int id);

        void HideModel(int id);

        void ShowModelForFirstSide(int id);

        void ShowModelForSecondSide(int id);

        void ShowModel(int id);
    }
}
