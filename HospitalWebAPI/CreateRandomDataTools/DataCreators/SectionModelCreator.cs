using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.HospitalModels;

namespace CreateRandomDataTools.DataCreators
{
    public class SectionModelCreator : ISectionModelCreator
    {
        public IEnumerable<SectionStorageModel> GetList()
        {
            var sectionProfilesList = new List<SectionStorageModel>
            {
                new SectionStorageModel()
                {
                    IsBlocked = false,
                    Id = 0,
                    Name = "Кардиологическое",
                    SectionProfiles = new List<SectionProfileStorageModel>
                    {
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Кардиологическое"
                        }
                    }
                },
                new SectionStorageModel()
                {
                    IsBlocked = false,
                    Id = 0,
                    Name = "Терапевтическое",
                    SectionProfiles = new List<SectionProfileStorageModel>
                    {
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Терапевтическое"
                        }
                    }
                }
            };

            return sectionProfilesList;
        }
    }
}
