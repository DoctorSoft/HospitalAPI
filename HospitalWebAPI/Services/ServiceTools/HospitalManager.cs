using System.Linq;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.HospitalModels;
using StorageModels.Models.UserModels;

namespace Services.ServiceTools
{
    public class HospitalManager : IHospitalManager
    {
        private readonly IHospitalRepository hospitalRepository;

        public HospitalManager(IHospitalRepository hospitalRepository)
        {
            this.hospitalRepository = hospitalRepository;
        }

        public HospitalStorageModel GetHospitalByUser(UserStorageModel model)
        {
            var hospitals = hospitalRepository.GetModels();

            var result =
                hospitals.FirstOrDefault(
                    storageModel =>
                        storageModel.HospitalUsers.Select(userStorageModel => userStorageModel.Id).Contains(model.Id));
            
            return result;
        }
    }
}
