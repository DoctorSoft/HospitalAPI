using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.HospitalModels;

namespace CreateRandomDataTools.DataCreators
{
    public class SectionModelCreator : ISectionModelCreator
    {
        public IEnumerable<SectionStorageModel> GetList()
        {
            var firstSectionProfile = new SectionStorageModel
            {
                IsBlocked = false,
                Id = 0,
                Name = "Some Name",
                SectionProfiles = new List<SectionProfileStorageModel>
                {
                    new SectionProfileStorageModel
                    {
                        IsBlocked = false,
                        Name = "Some Name Profile"
                    },
                    new SectionProfileStorageModel
                    {
                        IsBlocked = false,
                        Name = "Some Name Profile 2"
                    },
                }
            };

            return new List<SectionStorageModel>
            {
                firstSectionProfile
            };
        }
    }
}
