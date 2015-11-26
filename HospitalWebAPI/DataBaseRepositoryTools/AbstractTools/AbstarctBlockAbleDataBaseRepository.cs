using System.Collections.Generic;
using System.Linq;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces;
using StorageModels.Interfaces;

namespace DataBaseRepositoryTools.AbstractTools
{
    public abstract class AbstarctBlockAbleDataBaseRepository<T> : AbstractAddAbleDataBaseRepository<T>, IBlockAbleRepository<T>
        where T: class, IIdModel, IBlockAbleModel
    {
        protected AbstarctBlockAbleDataBaseRepository(IDataBaseContext context) : base(context)
        {
        }

        public override T GetModelById(int id)
        {
            var model = base.GetModelById(id);

            return model == null || model.IsBlocked ? null : model;
        }

        public override IEnumerable<T> GetModels()
        {
            return base.GetModels().Where(arg => !arg.IsBlocked);
        }

        public virtual IEnumerable<T> GetBlockedModels()
        {
            return base.GetModels().Where(arg => arg.IsBlocked);
        }

        public virtual void BlockModel(int id)
        {
            var model = GetModelById(id);
            if (model == null)
            {
                return;
            }

            model.IsBlocked = true;
            base.Update(id, model);
        }

        public virtual void UnBlockModel(int id)
        {
            var model = base.GetModelById(id);
            if (model == null)
            {
                return;
            }

            model.IsBlocked = false;
            base.Update(id, model);
        }
    }
}
