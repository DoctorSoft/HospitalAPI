using System.Linq;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.ClinicModels;

namespace Services.ServiceTools
{
    public class SettingsManager : ISettingsManager
    {
        private readonly IClinicRegistrationTimeRepository _clinicRegistrationTimeRepository;

        public SettingsManager(IClinicRegistrationTimeRepository clinicRegistrationTimeRepository)
        {
            _clinicRegistrationTimeRepository = clinicRegistrationTimeRepository;
        }

        public ClinicRegistrationTimeStorageModel GetClinicRegistration()
        {

            var maxDate = _clinicRegistrationTimeRepository.GetModels().Max(model => model.DateCreate);
            var clinicRegistrationTime = _clinicRegistrationTimeRepository.GetModels().FirstOrDefault(model => model.DateCreate == maxDate);

            return clinicRegistrationTime;
        }
    }
}
