using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using StorageModels.Models.HospitalModels;

namespace CreateRandomDataTools.DataCreators
{
    public class HospitalSectionProfileCreator : IHospitalSectionProfileCreator
    {
        private readonly IHospitalRepository _hospitalRepository;

        private readonly ISectionProfileRepository _sectionProfileRepository;
        
        public HospitalSectionProfileCreator(IHospitalRepository hospitalRepository, ISectionProfileRepository sectionProfileRepository)
        {
            this._hospitalRepository = hospitalRepository;
            _sectionProfileRepository = sectionProfileRepository;
        }

        public IEnumerable<HospitalSectionProfileStorageModel> GetList()
        {
            var hospitals = _hospitalRepository.GetModels().ToList();
            var sectionProfiles = _sectionProfileRepository.GetModels().ToList();

            return (from hospital in hospitals
                from sectionProfile in sectionProfiles
                select new HospitalSectionProfileStorageModel
                {
                    HospitalId = hospital.Id, IsBlocked = false, Name = $"{hospital.Name} {sectionProfile.Name}", SectionProfileId = sectionProfile.Id
                }).ToList();
        }
    }
}
