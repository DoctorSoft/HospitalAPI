using StorageModels.Models.HospitalModels;
using StorageModels.Models.UserModels;

namespace Services.Interfaces.ServiceTools
{
    public interface IHospitalManager
    {
        HospitalStorageModel GetHospitalByUser(UserStorageModel model);
    }
}
