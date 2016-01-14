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

            var pairs = GetStatisticPairs();

            var result = Enumerable.Range(0, dayCount)
                .SelectMany(dayNumber => hospitalSectionProfiles
                    .Select(hospitalSectionProfileId => new EmptyPlaceStatisticStorageModel
                    {
                        Date = startDate.AddDays(dayNumber).Date,
                        CreateTime = DateTime.Now,
                        HospitalSectionProfileId = hospitalSectionProfileId,
                        EmptyPlaceByTypeStatistics = pairs
                            .Select(pair => new EmptyPlaceByTypeStatisticStorageModel
                            {
                                AgeSection = pair.AgeSection,
                                Sex = pair.Sex,
                                Count = _extendedRandom.Next(0, 15)
                            }).ToList()
                    })
                    .Where(model => _extendedRandom.GetRandomBool(100 - 3*dayNumber))
                ).ToList();

            return result;
        }

        private static List<StatisticTypePair> GetStatisticPairs()
        {
            var sexValues = Enum.GetValues(typeof (Sex)).Cast<Sex>().ToList();
            var ageSections = Enum.GetValues(typeof (AgeSection)).Cast<AgeSection>().ToList();

            return (from sexValue in sexValues from ageSection in ageSections select new StatisticTypePair {AgeSection = ageSection, Sex = sexValue}).ToList();
        }

        private class StatisticTypePair
        {
            public Sex Sex { get; set; }

            public AgeSection AgeSection { get; set; }
        }
    }
}
