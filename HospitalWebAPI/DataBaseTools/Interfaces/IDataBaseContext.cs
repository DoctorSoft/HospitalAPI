using System.Collections.Generic;
using System.Data.Entity;
using EntityFramework.BulkInsert.Extensions;

namespace DataBaseTools.Interfaces
{
    public interface IDataBaseContext
    {
        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
