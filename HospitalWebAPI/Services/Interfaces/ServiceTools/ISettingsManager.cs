using StorageModels.Models.ClinicModels;

namespace Services.Interfaces.ServiceTools
{
    public interface ISettingsManager
    {
        SettingsItemStorageModel GetRegistrationSettings();
    }
}
 