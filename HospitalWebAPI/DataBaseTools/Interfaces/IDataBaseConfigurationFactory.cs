using System.Collections.Generic;
using System.Data.Entity;

namespace DataBaseTools.Interfaces
{
    public interface IDataBaseConfigurationFactory
    {
        DbModelBuilder GetConfigurations(DbModelBuilder builder);
    }
}
