using System.Linq;
using DataBaseTools.Interfaces;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.ClinicModels;

namespace Services.ServiceTools
{
    public class SettingsManager : ISettingsManager
    {
        private readonly IDataBaseContext _context;

        public SettingsManager(IDataBaseContext context)
        {
            _context = context;
        }

        public SettingsItemStorageModel GetRegistrationSettings()
        {

            var maxDate = _context.Set<SettingsItemStorageModel>().Max(model => model.DateCreate);
            var clinicRegistrationTime = _context.Set<SettingsItemStorageModel>().FirstOrDefault(model => model.DateCreate == maxDate);

            return clinicRegistrationTime;
        }
    }
}
