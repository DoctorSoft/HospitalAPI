using System.Collections.Generic;
using System.Linq;
using Enums.Enums;
using HandleToolsInterfaces.RepositoryHandlers;
using StorageModels.Interfaces;

namespace HandleTools.RepositoryHandlers
{
    public class TwoSideShowingHandler<T> : ITwoSideShowingHandler<T>
        where T: IIdModel, IShowStatusModel
    {
        public IEnumerable<T> GetFirstSideModels(IEnumerable<T> list)
        {
            return list.Where(arg => arg.ShowStatus == TwoSideShowStatus.FirstSideOnly || arg.ShowStatus == TwoSideShowStatus.Showed);
        }

        public IEnumerable<T> GetSecondSideModels(IEnumerable<T> list)
        {
            return list.Where(arg => arg.ShowStatus == TwoSideShowStatus.SecondSideOnly || arg.ShowStatus == TwoSideShowStatus.Showed);
        }

        public IEnumerable<T> GetHiddenModels(IEnumerable<T> list)
        {
            return list.Where(arg => arg.ShowStatus == TwoSideShowStatus.Hidden);
        }

        public T HideModeFromFirstSide(T model)
        {
            if (model.ShowStatus == TwoSideShowStatus.Showed)
            {
                model.ShowStatus = TwoSideShowStatus.SecondSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.FirstSideOnly)
            {
                model.ShowStatus = TwoSideShowStatus.Hidden;
            }

            return model;
        }

        public T HideModelFromSecondSide(T model)
        {
            if (model.ShowStatus == TwoSideShowStatus.Showed)
            {
                model.ShowStatus = TwoSideShowStatus.FirstSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.SecondSideOnly)
            {
                model.ShowStatus = TwoSideShowStatus.Hidden;
            }

            return model;
        }

        public T HideModel(T model)
        {
            model.ShowStatus = TwoSideShowStatus.Hidden;

            return model;
        }

        public T ShowModelForFirstSide(T model)
        {
            if (model.ShowStatus == TwoSideShowStatus.Hidden)
            {
                model.ShowStatus = TwoSideShowStatus.FirstSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.SecondSideOnly)
            {
                model.ShowStatus = TwoSideShowStatus.Showed;
            }

            return model;
        }

        public T ShowModelForSecondSide(T model)
        {
            if (model.ShowStatus == TwoSideShowStatus.Hidden)
            {
                model.ShowStatus = TwoSideShowStatus.SecondSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.FirstSideOnly)
            {
                model.ShowStatus = TwoSideShowStatus.Showed;
            }

            return model;
        }

        public T ShowModel(T model)
        {
            model.ShowStatus = TwoSideShowStatus.Showed;

            return model;
        }
    }
}
