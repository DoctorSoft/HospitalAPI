using System;
using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using Enums.Enums;
using HelpingTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using StorageModels.Models.HospitalModels;

namespace CreateRandomDataTools.DataCreators
{
    public class EmptyPlaceStatisticModelCreator : IEmptyPlaceStatisticModelCreator
    {
        private readonly IExtendedRandom _extendedRandom;

        private readonly IHospitalSectionProfileRepository _hospitalSectionProfileRepository;

        public EmptyPlaceStatisticModelCreator(IExtendedRandom extendedRandom, IHospitalSectionProfileRepository hospitalSectionProfileRepository)
        {
            _extendedRandom = extendedRandom;
            _hospitalSectionProfileRepository = hospitalSectionProfileRepository;
        }

        public IEnumerable<EmptyPlaceStatisticStorageModel> GetList()
        {
            var startDate = DateTime.Now.AddDays(1).Date;
            const int dayCount = 5;
            var hospitalSectionProfiles = _hospitalSectionProfileRepository.GetModels()
                .Select(model => model.Id)
                .ToList();

            var sexes = GetSexes();

            var result = Enumerable.Range(0, dayCount)
                .SelectMany(dayNumber => hospitalSectionProfiles
                    .Select(hospitalSectionProfileId => new EmptyPlaceStatisticStorageModel
                    {
                        Date = startDate.AddDays(dayNumber).Date,
                        CreateTime = DateTime.Now,
                        HospitalSectionProfileId = hospitalSectionProfileId,
                        EmptyPlaceByTypeStatistics = sexes
                            .Select(pair => new EmptyPlaceByTypeStatisticStorageModel
                            {
                                Sex = pair,
                                Count = _extendedRandom.Next(0, 15)
                            }).ToList()
                    })
                    .Where(model => _extendedRandom.GetRandomBool(100 - 3*dayNumber))
                ).ToList();

            return result;
        }

        private static List<Sex> GetSexes()
        {
            var sexValues = Enum.GetValues(typeof (Sex)).Cast<Sex>().ToList();

            return sexValues;
        }
    }
}
