using System.Linq;
using DataBaseTools.Interfaces;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.HospitalModels;
using StorageModels.Models.UserModels;

namespace Services.ServiceTools
{
    public class HospitalManager : IHospitalManager
    {
        private readonly IDataBaseContext _context;

        public HospitalManager(IDataBaseContext context)
        {
            _context = context;
        }

        public HospitalStorageModel GetHospitalByUser(UserStorageModel model)
        {
            var hospitals = _context.Set<HospitalStorageModel>();

            var result =
                hospitals.FirstOrDefault(
                    storageModel =>
                        storageModel.HospitalUsers.Select(userStorageModel => userStorageModel.Id).Contains(model.Id));
            
            return result;
        }
    }
}
