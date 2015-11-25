using System.Collections.Generic;
using System.Linq;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces;
using StorageModels.Interfaces;

namespace DataBaseRepositoryTools.AbstractTools
{
    public abstract class AbstractReadOnlyDataBaseRepository<T> : IReadOnlyRepository<T>
        where T: class, IIdModel
    {
        private readonly IDataBaseContext _context;

        protected AbstractReadOnlyDataBaseRepository(IDataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetModels()
        {
            return _context.Set<T>();
        }

        public T GetModelById(int id)
        {
            return _context.Set<T>().SingleOrDefault(arg => arg.Id == id);
        }
    }
}
