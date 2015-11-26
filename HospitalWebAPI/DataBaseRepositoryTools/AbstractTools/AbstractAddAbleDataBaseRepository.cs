using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces;
using StorageModels.Interfaces;

namespace DataBaseRepositoryTools.AbstractTools
{
    public abstract class AbstractAddAbleDataBaseRepository<T> : AbstractUpdateAbleDataBaseRepository<T>, IAddAbleRepository<T>
        where T: class, IIdModel
    {
        private readonly IDataBaseContext _context;

        protected AbstractAddAbleDataBaseRepository(IDataBaseContext context) : base(context)
        {
            _context = context;
        }

        public virtual void Add(T model)
        {
            _context.Set<T>().Add(model);
        }
    }
}
