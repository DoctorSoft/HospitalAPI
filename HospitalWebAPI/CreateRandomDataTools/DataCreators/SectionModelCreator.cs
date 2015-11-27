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