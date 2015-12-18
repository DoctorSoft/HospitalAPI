using StorageModels.Models.ClinicModels;
using StorageModels.Models.UserModels;

namespace Services.Interfaces.ServiceTools
{
    public interface IClinicManager
    {
        ClinicStorageModel GetClinicByUser(UserStorageModel model);
    }
}
