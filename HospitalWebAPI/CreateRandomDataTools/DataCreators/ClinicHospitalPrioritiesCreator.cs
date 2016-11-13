using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using DataBaseTools.Interfaces;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.HospitalModels;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class ClinicHospitalPrioritiesCreator : IClinicHospitalPrioritiesCreator
    {
        private readonly IDataBaseContext _context;

        public ClinicHospitalPrioritiesCreator(IDataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<ClinicUserHospitalSectionProfileAccessStorageModel> GetList()
        {
            var clinicUsers = _context.Set<ClinicUserStorageModel>().ToList();
            var hospitalSectionProfiles = _context.Set<HospitalSectionProfileStorageModel>().ToList();

            var result = new List<ClinicUserHospitalSectionProfileAccessStorageModel>();

            foreach (var clinicUser in clinicUsers)
            {
                result.AddRange(hospitalSectionProfiles.Select(hospitalSectionProfile => new ClinicUserHospitalSectionProfileAccessStorageModel
                {
                    ClinicUserId = clinicUser.Id, HospitalSectionProfileId = hospitalSectionProfile.Id, IsBlocked = false
                }));
            }

            return result;
        }
    }
}
