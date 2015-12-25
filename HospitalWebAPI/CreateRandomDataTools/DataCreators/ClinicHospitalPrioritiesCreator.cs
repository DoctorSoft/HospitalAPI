using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using StorageModels.Models.ClinicModels;

namespace CreateRandomDataTools.DataCreators
{
    public class ClinicHospitalPrioritiesCreator : IClinicHospitalPrioritiesCreator
    {
        private readonly IClinicRepository _clinicRepository;

        private readonly IHospitalRepository _hospitalRepository;

        public ClinicHospitalPrioritiesCreator(IClinicRepository clinicRepository, IHospitalRepository hospitalRepository)
        {
            _clinicRepository = clinicRepository;
            _hospitalRepository = hospitalRepository;
        }

        public IEnumerable<ClinicHospitalPriorityStorageModel> GetList()
        {
            var clinics = _clinicRepository.GetModels().ToList();
            var hospitals = _hospitalRepository.GetModels().ToList();

            var result = new List<ClinicHospitalPriorityStorageModel>();
            var priority = 0;
            foreach (var hospital in hospitals)
            {
                result.AddRange(clinics.Select(clinic => new ClinicHospitalPriorityStorageModel
                {
                    Priority = ++priority, ClinicId = clinic.Id, HospitalId = hospital.Id, IsBlocked = false
                }));
            }

            return result;
        }
    }
}
