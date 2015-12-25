using System.Linq;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.ClinicModels;

namespace Services.ServiceTools
{
    public class SettingsManager : ISettingsManager
    {
        private readonly ISettingsItemRepository _settingsItemRepository;

        public SettingsManager(ISettingsItemRepository settingsItemRepository)
        {
            _settingsItemRepository = settingsItemRepository;
        }

        public SettingsItemStorageModel GetRegistrationSettings()
        {

            var maxDate = _settingsItemRepository.GetModels().Max(model => model.DateCreate);
            var clinicRegistrationTime = _settingsItemRepository.GetModels().FirstOrDefault(model => model.DateCreate == maxDate);

            return clinicRegistrationTime;
        }
    }
}
