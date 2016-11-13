using Enums.Enums;
using HandleToolsInterfaces.RepositoryHandlers;
using StorageModels.Interfaces;

namespace HandleTools.RepositoryHandlers
{
    public class TwoSideShowingHandler<T> : ITwoSideShowingHandler<T>
        where T: IIdModel, IShowStatusModel
    {
        public T HideModeFromFromSide(T model)
        {
            if (model.ShowStatus == TwoSideShowStatus.Showed)
            {
                model.ShowStatus = TwoSideShowStatus.ToSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.FromSideOnly)
            {
                model.ShowStatus = TwoSideShowStatus.Hidden;
            }

            return model;
        }

        public T HideModelFromToSide(T model)
        {
            if (model.ShowStatus == TwoSideShowStatus.Showed)
            {
                model.ShowStatus = TwoSideShowStatus.FromSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.ToSideOnly)
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

        public T ShowModelForFromSide(T model)
        {
            if (model.ShowStatus == TwoSideShowStatus.Hidden)
            {
                model.ShowStatus = TwoSideShowStatus.FromSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.ToSideOnly)
            {
                model.ShowStatus = TwoSideShowStatus.Showed;
            }

            return model;
        }

        public T ShowModelForToSide(T model)
        {
            if (model.ShowStatus == TwoSideShowStatus.Hidden)
            {
                model.ShowStatus = TwoSideShowStatus.ToSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.FromSideOnly)
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
