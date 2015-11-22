using DataBaseTools.AbstractClasses;
using DataBaseTools.Interfaces;

namespace DataBaseModelConfigurations.Contexts
{
    public class OriginalDataBaseContext : AbstractConfiguredContext
    {
        protected OriginalDataBaseContext(IDataBaseConfigurationFactory configurationFactory) : base("OriginalHospitalDataBase", configurationFactory)
        {
        }
    }
}
