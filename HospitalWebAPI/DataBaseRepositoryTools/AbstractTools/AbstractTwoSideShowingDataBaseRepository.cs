using System.Collections.Generic;
using System.Linq;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces;
using StorageModels.Enums;
using StorageModels.Interfaces;

namespace DataBaseRepositoryTools.AbstractTools
{
    public abstract class AbstractTwoSideShowingDataBaseRepository<T> : AbstractAddAbleDataBaseRepository<T>, ITwoSideShowingRepository<T>
        where T: class, IIdModel, IShowStatusModel
    {
        protected AbstractTwoSideShowingDataBaseRepository(IDataBaseContext context) : base(context)
        {
        }

        public override IEnumerable<T> GetModels()
        {
            return base.GetModels().Where(arg => arg.ShowStatus != TwoSideShowStatus.Hidden);
        }

        public IEnumerable<T> GetFirstSideModels()
        {
            return base.GetModels()
                .Where(arg => arg.ShowStatus == TwoSideShowStatus.FirstSideOnly || arg.ShowStatus == TwoSideShowStatus.Showed);
        }

        public IEnumerable<T> GetSecondSideModels()
        {
            return base.GetModels()
                .Where(arg => arg.ShowStatus == TwoSideShowStatus.SecondSideOnly || arg.ShowStatus == TwoSideShowStatus.Showed);
        }

        public IEnumerable<T> GetHiddenModels()
        {
            return base.GetModels().Where(arg => arg.ShowStatus == TwoSideShowStatus.Hidden);
        }

        public void HideModeFromFirstSide(int id)
        {
            var model = base.GetModelById(id);

            if (model.ShowStatus == TwoSideShowStatus.Showed)
            {
                model.ShowStatus = TwoSideShowStatus.SecondSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.FirstSideOnly)
            {
                model.ShowStatus = TwoSideShowStatus.Hidden;
            }

            base.Update(id, model);
        }

        public void HideModelFromSecondSide(int id)
        {
            var model = base.GetModelById(id);

            if (model.ShowStatus == TwoSideShowStatus.Showed)
            {
                model.ShowStatus = TwoSideShowStatus.FirstSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.SecondSideOnly)
            {
                model.ShowStatus = TwoSideShowStatus.Hidden;
            }

            base.Update(id, model);
        }

        public void HideModel(int id)
        {
            var model = base.GetModelById(id);
            model.ShowStatus = TwoSideShowStatus.Hidden;

            base.Update(id, model);
        }

        public void ShowModelForFirstSide(int id)
        {
            var model = base.GetModelById(id);

            if (model.ShowStatus == TwoSideShowStatus.Hidden)
            {
                model.ShowStatus = TwoSideShowStatus.FirstSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.SecondSideOnly)
            {
                model.ShowStatus = TwoSideShowStatus.Showed;
            }

            base.Update(id, model);
        }

        public void ShowModelForSecondSide(int id)
        {
            var model = base.GetModelById(id);

            if (model.ShowStatus == TwoSideShowStatus.Hidden)
            {
                model.ShowStatus = TwoSideShowStatus.SecondSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.FirstSideOnly)
            {
                model.ShowStatus = TwoSideShowStatus.Showed;
            }

            base.Update(id, model);
        }

        public void ShowModel(int id)
        {
            var model = base.GetModelById(id);
            model.ShowStatus = TwoSideShowStatus.Showed;

            base.Update(id, model);
        }
    }
}
