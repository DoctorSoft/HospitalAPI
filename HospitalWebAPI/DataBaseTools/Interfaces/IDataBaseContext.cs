using System.Data.Entity;

namespace DataBaseTools.Interfaces
{
    public interface IDataBaseContext
    {
        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
