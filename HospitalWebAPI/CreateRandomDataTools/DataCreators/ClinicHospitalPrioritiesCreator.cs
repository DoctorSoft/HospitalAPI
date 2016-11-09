using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.ClinicModels;

namespace CreateRandomDataTools.DataCreators
{
    public class ClinicHospitalPrioritiesCreator : IClinicHospitalPrioritiesCreator
    {
        private readonly IClinicUserRepository _clinicUserRepository;

        private readonly IHospitalSectionProfileRepository _hospitalSectionProfileRepository;

        public ClinicHospitalPrioritiesCreator(IClinicUserRepository clinicUserRepository, IHospitalSectionProfileRepository hospitalSectionProfileRepository)
        {
            _clinicUserRepository = clinicUserRepository;
            _hospitalSectionProfileRepository = hospitalSectionProfileRepository;
        }

        public IEnumerable<ClinicUserHospitalSectionProfileAccessStorageModel> GetList()
        {
            var clinicUsers = _clinicUserRepository.GetModels().ToList();
            var hospitalSectionProfiles = _hospitalSectionProfileRepository.GetModels().ToList();

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
