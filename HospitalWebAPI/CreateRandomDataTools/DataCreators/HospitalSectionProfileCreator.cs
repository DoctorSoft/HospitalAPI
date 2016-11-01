using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.HospitalModels;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class HospitalSectionProfileCreator : IHospitalSectionProfileCreator
    {
        private readonly IHospitalRepository _hospitalRepository;

        private readonly ISectionProfileRepository _sectionProfileRepository;

        private readonly IHospitalUserSectionAccessRepository _hospitalUserSectionAccessRepository;

        private readonly IHospitalUserRepository _hospitalUserRepository;
        
        public HospitalSectionProfileCreator(IHospitalRepository hospitalRepository, ISectionProfileRepository sectionProfileRepository, IHospitalUserSectionAccessRepository hospitalUserSectionAccessRepository, IHospitalUserRepository hospitalUserRepository)
        {
            this._hospitalRepository = hospitalRepository;
            _sectionProfileRepository = sectionProfileRepository;
            _hospitalUserSectionAccessRepository = hospitalUserSectionAccessRepository;
            this._hospitalUserRepository = hospitalUserRepository;
        }

        public IEnumerable<HospitalSectionProfileStorageModel> GetList()
        {
            var hospitals = _hospitalRepository.GetModels().ToList();
            var sectionProfiles = _sectionProfileRepository.GetModels().ToList();

            var hospitalUsers = _hospitalUserRepository.GetModels().ToList();

            return (from hospital in hospitals
                from sectionProfile in sectionProfiles
                select new HospitalSectionProfileStorageModel
                {
                    HospitalId = hospital.Id, 
                    IsBlocked = false, 
                    Name = $"{hospital.Name} {sectionProfile.Name}", 
                    SectionProfileId = sectionProfile.Id,
                    HasGenderFactor = !hospital.IsForChildren,
                    HospitalUserSectionAccesses = hospitalUsers
                        .Where(model => model.HospitalId == hospital.Id)
                        .Select(model => new HospitalUserSectionAccessStorageModel
                        {
                             HospitalUserId = model.Id,
                             IsBlocked = false
                        }).ToList()
                }).ToList();
        }
    }
}
