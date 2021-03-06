﻿using System.Data.Entity.Migrations;
using DataBaseTools.Interfaces;
using RepositoryTools.Interfaces;
using RepositoryTools.Interfaces.CommonInterfaces;
using StorageModels.Interfaces;

namespace DataBaseRepositoryTools.AbstractTools
{
    public abstract class AbstractUpdateAbleDataBaseRepository<T> : AbstractReadOnlyDataBaseRepository<T>, IUpdateAbleRepository<T>
        where T: class, IIdModel
    {
        private readonly IDataBaseContext _context;

        protected AbstractUpdateAbleDataBaseRepository(IDataBaseContext context) : base(context)
        {
            _context = context;
        }

        public virtual void Update(int id, T model)
        {
            model.Id = id;
            if (id < 1)
            {
                return;
            }

            _context.Set<T>().AddOrUpdate(model);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
