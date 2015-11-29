using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.HospitalModels;

namespace CreateRandomDataTools.DataCreators
{
    public class SectionModelCreator : ISectionModelCreator
    {
        public IEnumerable<SectionStorageModel> GetList()
        {
            // TODO: Create list of sections with profiles

            var sectionProfilesList = new List<SectionStorageModel>
            {
                new SectionStorageModel()
                {
                    IsBlocked = false,
                    Id = 0,
                    Name = "Терапевтические и неврологические",
                    SectionProfiles = new List<SectionProfileStorageModel>
                    {
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Кардиологическое"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Пульмонологическое"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Неврологическое"
                        }
                    }
                },
                new SectionStorageModel()
                {
                    IsBlocked = false,
                    Id = 1,
                    Name = "Хирургические",
                    SectionProfiles = new List<SectionProfileStorageModel>
                    {
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Хирургическое"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Гнойной хирургии"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Нейрохирургическое"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Ожоговое"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Травматологическое"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Урологическое"
                        }
                    }
                },
                new SectionStorageModel()
                {
                    IsBlocked = false,
                    Id = 2,
                    Name = "Реанимационно-анестезиологические",
                    SectionProfiles = new List<SectionProfileStorageModel>
                    {
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Реанимации и интенсивной терапии"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Анестезиологии и реанимации"
                        }
                    }
                },
                new SectionStorageModel()
                {
                    IsBlocked = false,
                    Id = 3,
                    Name = "Параклинические",
                    SectionProfiles = new List<SectionProfileStorageModel>
                    {
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Операционное"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Анестезиологии"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Переливания крови"
                        },
                        new SectionProfileStorageModel
                        {
                            IsBlocked = false,
                            Name = "Гипербарической оксигенации"
                        }
                    }
                }
            };

            return sectionProfilesList;
        }
    }
}

/*

Гастроэнтерологическое отделение
Гематологическое отделение
Детское инфекционное отделение
Инфекционное отделение
Кардиологическое отделение
Кардиохирургическое отделение
Неврологическое отделение
Нейрохирургическое отделение
Ожоговое отделение
Ортопедо-травматологическое отделение сочетанной травмы
Отделение гнойной хирургии
Отделение сосудистой хирургии
Отделение трансплантации
Отделение хирургии торакальной
Оториноларингологическое отделение
Отделение микрохирургии глаза
Пульмонологическое отделение
Отделение анестезиологии, реанимации и интенсивной терапии
Отделение анестезиологии и реанимации №1
Отделение анестезиологии и реанимации №2 (кардиореанимация)
Отделение анестезиологии и реанимации, нефрологии и гемокоррекции
Ревматологическое отделение
Отделение челюстно-лицевой хирургии
Урологическое отделение №1
Урологическое отделение №2
Хирургическое отделение

*/