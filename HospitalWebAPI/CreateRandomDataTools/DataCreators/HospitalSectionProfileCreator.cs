using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using DataBaseTools.Interfaces;
using StorageModels.Models.HospitalModels;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class HospitalSectionProfileCreator : IHospitalSectionProfileCreator
    {
        private readonly IDataBaseContext _context;

        public HospitalSectionProfileCreator(IDataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<HospitalSectionProfileStorageModel> GetList()
        {
            var hospitals = _context.Set<HospitalStorageModel>().ToList();
            var sectionProfiles = _context.Set<SectionProfileStorageModel>().ToList();

            var hospitalUsers = _context.Set<HospitalUserStorageModel>().ToList();

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
