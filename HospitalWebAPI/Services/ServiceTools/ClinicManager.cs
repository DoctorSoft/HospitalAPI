using System.Data.Entity;
using System.Linq;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.UserModels;

namespace Services.ServiceTools
{
    public class ClinicManager : IClinicManager
    {
        private readonly IClinicUserRepository _clinicUserRepository;

        public ClinicManager(IClinicUserRepository clinicUserRepository)
        {
            _clinicUserRepository = clinicUserRepository;
        }

        public ClinicStorageModel GetClinicByUser(UserStorageModel model)
        {
            var clinicUser = ((IDbSet<ClinicUserStorageModel>) _clinicUserRepository.GetModels())
                .Include(storageModel => storageModel.Clinic)
                .FirstOrDefault(storageModel => storageModel.Id == model.Id);

            if (clinicUser == null)
            {
                return null;
            }

            return clinicUser.Clinic;
        }
    }
}
