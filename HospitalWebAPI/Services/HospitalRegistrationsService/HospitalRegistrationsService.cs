using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Enums.Enums;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;
using ServiceModels.ServiceCommands.HospitalRegistrationsCommands;
using Services.Interfaces.HospitalRegistrationsService;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.HospitalModels;

namespace Services.HospitalRegistrationsService
{
    public class HospitalRegistrationsService : IHospitalRegistrationsService
    {
        private readonly IEmptyPlaceStatisticRepository _emptyPlaceStatisticRepository;
        private readonly IEmptyPlaceByTypeStatisticRepository _emptyPlaceByTypeStatisticRepository;
        private readonly IHospitalSectionProfileRepository _hospitalSectionProfileRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IHospitalUserRepository _hospitalUserRepository;
        private readonly IReservationRepository _reservationRepository;

        public HospitalRegistrationsService(IEmptyPlaceStatisticRepository emptyPlaceStatisticRepository,
            IHospitalSectionProfileRepository hospitalSectionProfileRepository, ITokenManager tokenManager,
            IHospitalUserRepository hospitalUserRepository, IEmptyPlaceByTypeStatisticRepository emptyPlaceByTypeStatisticRepository, IReservationRepository reservationRepository)
        {
            _emptyPlaceStatisticRepository = emptyPlaceStatisticRepository;
            _hospitalSectionProfileRepository = hospitalSectionProfileRepository;
            _tokenManager = tokenManager;
            _hospitalUserRepository = hospitalUserRepository;
            _emptyPlaceByTypeStatisticRepository = emptyPlaceByTypeStatisticRepository;
            _reservationRepository = reservationRepository;
        }

        public GetChangeHospitalRegistrationsPageInformationCommandAnswer GetChangeHospitalRegistrationsPageInformation(
            GetChangeHospitalRegistrationsPageInformationCommand command)
        {
            const int days = 30;
            var now = DateTime.Now;
            var deadLine = now + new TimeSpan(days, 0, 0, 0);

            var startMonday = GetPreviousMonday(now);
            var endMonday = GetPreviousMonday(deadLine);
            var weeks = (endMonday - startMonday).Days/7 + 1;

            var user = _tokenManager.GetUserByToken(command.Token);
            var hospitalId = GetHospitalIdByUserId(user.Id);
            var completeCount = this.GetHospitalProfileCount(hospitalId);
            var statisticList = this.GetStatisticList(now, deadLine, hospitalId);

            var startSchedule = Enumerable.Range(0, weeks)
                .Select(week => new ScheduleTableItem
                {
                    Cells = Enumerable.Range(0, 7)
                        .ToDictionary(day => (DayOfWeek) day, day => new ScheduleTableCell
                        {
                            IsBlocked =
                                startMonday.AddDays(7*week + day).Date < now.Date ||
                                startMonday.AddDays(7*week + day).Date > deadLine.Date,
                            Day = startMonday.AddDays(7*week + day).Day,
                            IsCompleted =
                                statisticList.Count(model => model.Date.Date == startMonday.AddDays(7*week + day).Date) ==
                                completeCount,
                            IsStarted =
                                statisticList.Any(model => model.Date.Date == startMonday.AddDays(7*week + day).Date),
                            IsThisMonth = startMonday.AddDays(7*week + day).Month == now.Month,
                            IsThisDate = startMonday.AddDays(7*week + day).Date == now.Date,
                            Date = startMonday.AddDays(7*week + day).Date
                        })
                })
                .ToList();

            return new GetChangeHospitalRegistrationsPageInformationCommandAnswer
            {
                Token = (Guid) command.Token,
                Schedule = startSchedule
            };
        }

        protected virtual List<EmptyPlaceStatisticStorageModel> GetStatisticList(DateTime startDate,
            DateTime endDate, int hospitalId)
        {
            var emptyPlaceStatistics = _emptyPlaceStatisticRepository.GetModels();
            var hospitalSectionProfiles = _hospitalSectionProfileRepository.GetModels();

            var results = from emptyPlaceStatistic in emptyPlaceStatistics
                where emptyPlaceStatistic.Date >= startDate && emptyPlaceStatistic.Date <= endDate
                join hospitalSectionProfile in hospitalSectionProfiles on emptyPlaceStatistic.HospitalSectionProfileId
                    equals hospitalSectionProfile.Id
                where hospitalSectionProfile.HospitalId == hospitalId
                select emptyPlaceStatistic;

            return results.ToList();
        }

        protected virtual int GetHospitalProfileCount(int hospitalId)
        {
            return _hospitalSectionProfileRepository.GetModels().Count(model => model.HospitalId == hospitalId);
        }

        protected virtual int GetHospitalIdByUserId(int userId)
        {
            var result = _hospitalUserRepository.GetModels().FirstOrDefault(model => model.Id == userId).HospitalId;

            return result;
        }

        protected virtual DateTime GetPreviousMonday(DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;
            const DayOfWeek monday = DayOfWeek.Monday;

            var dayDifference = (int)dayOfWeek - (int)monday;
            dayDifference = dayDifference < 0 ? 7 + dayDifference : dayDifference;

            var previousMonday = date - new TimeSpan(dayDifference, 0, 0, 0);

            return previousMonday;
        }

        public ShowHospitalRegistrationPlacesByDateCommandAnswer ShowHospitalRegistrationPlacesByDate(
            ShowHospitalRegistrationPlacesByDateCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);
            var hospitalId = GetHospitalIdByUserId(user.Id);

            var hospitalSectionProfiles = _hospitalSectionProfileRepository.GetModels();

            var date = command.Date.Date;
            var statisticList = this.GetStatisticList(date, date, hospitalId);
            var completeCount = this.GetHospitalProfileCount(hospitalId);

            var table = ((IDbSet<HospitalSectionProfileStorageModel>) hospitalSectionProfiles)
                .Where(model => model.HospitalId == hospitalId)
                .Where(model => model.EmptyPlaceStatistics.Any(storageModel => storageModel.Date == command.Date))
                .Select(model => new HospitalRegistrationTableItem
                {
                    HospitalProfileId = model.Id,
                    HospitalProfileName = model.Name,
                    StatisticItems = model.EmptyPlaceStatistics
                        .Where(storageModel => storageModel.Date == command.Date)
                        .SelectMany(storageModel => storageModel.EmptyPlaceByTypeStatistics)
                        .Select(storageModel => new HospitalRegistrationCountStatisticItem
                        {
                            Sex = storageModel.Sex,
                            OpenCount = storageModel.Count
                        })
                        .ToList()
                }).ToList();

            var statistics = statisticList.Count(model => model.Date.Date == date);

            return new ShowHospitalRegistrationPlacesByDateCommandAnswer
            {
                Token = (Guid) command.Token,
                Date = command.Date,
                Table = table,
                IsCompleted = statistics == completeCount,
            };
        }

        public ChangeHospitalRegistrationForSelectedSectionCommandAnswer ChangeHospitalRegistrationForSelectedSection(
            ChangeHospitalRegistrationForSelectedSectionCommand command)
        {
            var hospitalSectionProfilesName = _hospitalSectionProfileRepository.GetModels()
                .FirstOrDefault(model=>model.SectionProfileId == command.HospitalProfileId).Name;

            var date = DateTime.ParseExact(command.Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

            var emptyPlaceStatisticRepository = _emptyPlaceByTypeStatisticRepository.GetModels();
            
            var emptyPlaceId = _emptyPlaceStatisticRepository.GetModels()
                .FirstOrDefault(model => model.HospitalSectionProfileId == command.HospitalProfileId && model.Date == date)
                .Id;
            
            var table = emptyPlaceStatisticRepository
                .Where(model => model.EmptyPlaceStatistic.HospitalSectionProfileId == command.HospitalProfileId)
                .Where(
                    storageModel => storageModel.EmptyPlaceStatisticId.Equals(emptyPlaceId))
                .Select(model => new HospitalRegistrationCountStatisticItem
                {
                    Sex = model.Sex,
                    OpenCount = model.Count
                }).ToList();

            var emptyPlaceStatisticId = _emptyPlaceStatisticRepository.GetModels().FirstOrDefault(
                model => model.Date == date && model.HospitalSectionProfileId == command.HospitalProfileId).Id;

            var emptyPlaceByTypeStatisticsIdMaleId = _emptyPlaceByTypeStatisticRepository.GetModels().FirstOrDefault(
                model => model.EmptyPlaceStatisticId == emptyPlaceStatisticId && model.Sex == Sex.Male).Id;

            var emptyPlaceByTypeStatisticsIdFemaleId = _emptyPlaceByTypeStatisticRepository.GetModels().FirstOrDefault(
                model => model.EmptyPlaceStatisticId == emptyPlaceStatisticId && model.Sex == Sex.Female).Id;

            var countMale = _reservationRepository.GetModels()
                .Where(model => model.EmptyPlaceByTypeStatisticId == emptyPlaceByTypeStatisticsIdMaleId)
                .Where(model => model.Status == ReservationStatus.Opened)
                .Count(model => model.Patient.Sex == Sex.Male);

            var countFemale = _reservationRepository.GetModels()
                .Where(model => model.EmptyPlaceByTypeStatisticId == emptyPlaceByTypeStatisticsIdFemaleId)
                .Where(model => model.Status == ReservationStatus.Opened)
                .Count(model => model.Patient.Sex == Sex.Female);

            return new ChangeHospitalRegistrationForSelectedSectionCommandAnswer
            {
                Token = (Guid)command.Token,
                StatisticItems = table,
                Date = command.Date,
                SectionProfileName = hospitalSectionProfilesName,
                CountRegisteredMale = countMale,
                CountRegisteredFemale = countFemale,
                HospitalProfileId = command.HospitalProfileId,
                EmptyPlaceByTypeStatisticIdMaleId = emptyPlaceByTypeStatisticsIdMaleId,
                EmptyPlaceByTypeStatisticIdFeMaleId = emptyPlaceByTypeStatisticsIdFemaleId
            };
        }

        public GetChangeHospitalRegistrationCommandAnswer ApplyChangesHospitalRegistration(
            GetChangeHospitalRegistrationCommand command)
        {

            DateTime date = DateTime.ParseExact(command.Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

            var emptyPlaceStatisticsId =
            _emptyPlaceStatisticRepository.GetModels()
                .FirstOrDefault(model => model.HospitalSectionProfileId == command.HospitalProfileId && model.Date == date)
                .Id;

            var freeHospitalSectionsForRegistrationList = command.FreeHospitalSectionsForRegistration;

            var freeHospitalSectionsForRegistrationInTable =
                ((IDbSet<EmptyPlaceByTypeStatisticStorageModel>) _emptyPlaceByTypeStatisticRepository.GetModels())
                    .Where(model => model.EmptyPlaceStatisticId == emptyPlaceStatisticsId).ToList();

            var result = freeHospitalSectionsForRegistrationList
                .Select(element => freeHospitalSectionsForRegistrationInTable
                    .Where(model => element != null 
                        && model.Sex.ToString() == element.Sex.ToString())
                        .Select(model => new EmptyPlaceByTypeStatisticStorageModel
                        {
                            Id = model.Id, 
                            Count = element.OpenCount, 
                            Sex = element.Sex, 
                            EmptyPlaceStatisticId = emptyPlaceStatisticsId
                        }).FirstOrDefault()).ToList();

            foreach (var emptyPlace in result)
            {
                _emptyPlaceByTypeStatisticRepository.Update(emptyPlace.Id, emptyPlace);
            }
            _emptyPlaceByTypeStatisticRepository.SaveChanges();
     
            return new GetChangeHospitalRegistrationCommandAnswer
            {
                Token = (Guid)command.Token
            };
        }

        public GetChangeNewHospitalRegistrationCommandAnswer ApplyChangesNewHospitalRegistration(
            GetChangeNewHospitalRegistrationCommand command)
        {
            DateTime date = DateTime.ParseExact(command.Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

            var newHospitalSectionProfileId = new EmptyPlaceStatisticStorageModel
            {
                Date = DateTime.ParseExact(command.Date.Split(' ').First(), "MM/dd/yyyy", CultureInfo.InvariantCulture),
                CreateTime = DateTime.Now,
                HospitalSectionProfileId = command.HospitalProfileId,
                EmptyPlaceByTypeStatistics = command.FreeHospitalSectionsForRegistration
                    .Select(pair => new EmptyPlaceByTypeStatisticStorageModel
                    {
                        Sex = pair.Sex,
                        Count = pair.OpenCount
                    }
                    ).ToList()
            };

            _emptyPlaceStatisticRepository.Add(newHospitalSectionProfileId);
            _emptyPlaceStatisticRepository.SaveChanges();

            return new GetChangeNewHospitalRegistrationCommandAnswer
            {
                Token = (Guid)command.Token
            };
        }

        public ChangeHospitalRegistrationForNewSectionCommandAnswer ChangeHospitalRegistrationForNewSection(
            ChangeHospitalRegistrationForNewSectionCommand command)
        {

            var user = _tokenManager.GetUserByToken(command.Token);
            var hospitalId = GetHospitalIdByUserId(user.Id);

            var hospitalSectionProfiles = _hospitalSectionProfileRepository.GetModels();

            var hospitalSectionProfilesList = _hospitalSectionProfileRepository.GetModels().Where(model => model.HospitalId == hospitalId).ToList();

            var table = ((IDbSet<HospitalSectionProfileStorageModel>)hospitalSectionProfiles)
                .Where(model => model.HospitalId == hospitalId)
                .Where(model => model.EmptyPlaceStatistics.Any(storageModel => storageModel.Date == command.Date))
                .Select(model => new HospitalRegistrationTableItem
                {
                    HospitalProfileId = model.Id,
                    HospitalProfileName = model.Name
                }).ToList();
            

            var result = hospitalSectionProfilesList.Where(model => !table.Any(t => t.HospitalProfileId == model.Id)).ToList();
            
            return new ChangeHospitalRegistrationForNewSectionCommandAnswer
            {
                Token = (Guid)command.Token,
                FreeHospitalSectionsForRegistration = result,
                Date = command.Date
            };
        }

        public ViewDetailedInformationOnRegisteredPatientsCommandAnswer GetDetailedInformationOnRegisteredPatients(
            ViewDetailedInformationOnRegisteredPatientsCommand command)
        {
            var resrvations = this._reservationRepository.GetModels();

            List<ClinicBreakRegistrationTableItem> table = null;

            if (command.FullInformation == null)
            {
                table = resrvations
                    .Where(model => model.Status == ReservationStatus.Opened)
                    .Where(model => model.EmptyPlaceByTypeStatisticId == command.EmptyPlaceByTypeStatisticId)
                    .Select(model => new ClinicBreakRegistrationTableItem
                    {
                        SectionProfile = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Name,
                        ReservationId = model.Id,
                        Haspital =
                            model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Hospital.Name,
                        PatientCode = model.Patient.Code,
                        PatientFirstName = model.Patient.FirstName,
                        PatientLastName = model.Patient.LastName,
                        ReservationDate = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.Date,
                        Diagnosis = model.Diagnosis
                    })
                    .ToList();
            }
            else
            {
                table = resrvations
                .Where(model => model.Status == ReservationStatus.Opened)
                .Where(model => model.EmptyPlaceByTypeStatisticId == command.EmptyPlaceByTypeStatisticId 
                    || model.EmptyPlaceByTypeStatisticId == command.FullInformation)
                .Select(model => new ClinicBreakRegistrationTableItem
                {
                    SectionProfile = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Name,
                    ReservationId = model.Id,
                    Haspital =
                        model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Hospital.Name,
                    PatientCode = model.Patient.Code,
                    PatientFirstName = model.Patient.FirstName,
                    PatientLastName = model.Patient.LastName,
                    ReservationDate = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.Date,
                    Diagnosis = model.Diagnosis
                })
                .ToList();
            }
            return new ViewDetailedInformationOnRegisteredPatientsCommandAnswer
            {
                Token = (Guid)command.Token,
                Table = table,
                HospitalProfileId = command.HospitalProfileId,
                Date = command.Date
            };
        }
    }
}
