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
                    Name = "Неврологическое",
                    SectionProfiles = new List<SectionProfileStorageModel>
                    {
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Неврологическое"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Неврологическое (дети до 1 года)"
                        },
                    }
                },
                new SectionStorageModel()
                {
                    IsBlocked = false,
                    Id = 0,
                    Name = "Педиатрическое",
                    SectionProfiles = new List<SectionProfileStorageModel>
                    {
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "5-ое Педиатрическое (Гематологическое)"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "5-ое Педиатрическое (Кардиоревматологическое)"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "5-ое Педиатрическое (Нефрологическое)"
                        },
                        
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "6-ое Педиатрическое (Гастроэнтерологическое)"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "6-ое Педиатрическое (Аллергологическое)"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "6-ое Педиатрическое (Педиатрическое)"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "6-ое Педиатрическое (Эндокринологическое)"
                        },
                    }
                },
                new SectionStorageModel()
                {
                    IsBlocked = false,
                    Id = 0,
                    Name = "Хирургичское",
                    SectionProfiles = new List<SectionProfileStorageModel>
                    {
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "7-ое Хирургичское"
                        },
                    }
                },
                new SectionStorageModel()
                {
                    IsBlocked = false,
                    Id = 0,
                    Name = "Травмотолого-ортопедическое",
                    SectionProfiles = new List<SectionProfileStorageModel>
                    {
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Травмотолого-ортопедическое"
                        },
                    }
                },
            };

            return sectionProfilesList;
        }
    }
}
