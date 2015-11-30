using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.HospitalModels;

namespace CreateRandomDataTools.DataCreators
{
    public class ClinicModelCreator : IClinicModelCreator
    {
        private readonly IHospitalRepository _hospitalRepository;

        public ClinicModelCreator(IHospitalRepository hospitalRepository)
        {
            _hospitalRepository = hospitalRepository;
        }

        protected ICollection<ClinicHospitalAccessStorageModel> GetAccesses(IEnumerable<HospitalStorageModel> hospitals)
        {
            return hospitals.Select(model => new ClinicHospitalAccessStorageModel
            {
                IsBlocked = false,
                HospitalId = model.Id
            }).ToList();
        }

        public IEnumerable<ClinicStorageModel> GetList()
        {
            var hospitals = _hospitalRepository.GetModels().ToList();

            var clinicsList = new List<ClinicStorageModel>
            {
                new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 0,
                    Name = "Гродненская центральная городская поликлиника",
                    Address = "г. Гродно, ул. Транспортная, 3",
                    ClinicHospitalAccesses = GetAccesses(hospitals)
                },
                new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 1,
                    Name = "Городская поликлиника №1",
                    Address = "г. Гродно, ул. Лермонтова, 13",
                    ClinicHospitalAccesses = GetAccesses(hospitals)
                },
                new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 2,
                    Name = "Городская поликлиника №3",
                    Address = "г. Гродно, ул. Пестрака, 4",
                    ClinicHospitalAccesses = GetAccesses(hospitals)
                },
                new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 3,
                    Name = "Детская поликлиника № 2",
                    Address = "г. Гродно, ул. Гагарина, 18",
                    ClinicHospitalAccesses = GetAccesses(hospitals)
                },
                new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 4,
                    Name = "Городская поликлиника №4",
                    Address = "г. Гродно, ул. Врублевского, 46/1",
                    ClinicHospitalAccesses = GetAccesses(hospitals)
                },
                new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 5,
                    Name = "Городская поликлиника №6",
                    Address = "г. Гродно, ул. Горького, 77",
                    ClinicHospitalAccesses = GetAccesses(hospitals)
                }
            };

            return clinicsList;
        }
    }
}
