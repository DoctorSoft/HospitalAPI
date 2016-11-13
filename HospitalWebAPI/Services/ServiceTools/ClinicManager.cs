using System.Data.Entity;
using System.Linq;
using DataBaseTools.Interfaces;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.UserModels;

namespace Services.ServiceTools
{
    public class ClinicManager : IClinicManager
    {
        private readonly IDataBaseContext _context;

        public ClinicManager(IDataBaseContext context)
        {
            _context = context;
        }

        public ClinicStorageModel GetClinicByUser(UserStorageModel model)
        {
            var clinicUser = _context.Set<ClinicUserStorageModel>()
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
