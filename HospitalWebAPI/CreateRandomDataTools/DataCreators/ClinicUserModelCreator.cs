using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using Enums.Enums;
using HelpingTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.AnotherRepositories.RemoteAPIRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class ClinicUserModelCreator : IClinicUserModelCreator
    {
        private readonly IPersonDataAPIRepository _personDataApiRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly IUserTypeRepository _userTypeRepository;

        private readonly IPasswordHashManager _passwordHashManager;
        private readonly IAccountNameCalculator _accountNameCalculator;

        private const int UsersInOneClinic = 2;
        private const string StandardPassword = "12345";

        public ClinicUserModelCreator(IPersonDataAPIRepository personDataApiRepository, 
            IClinicRepository clinicRepository, IUserTypeRepository userTypeRepository,
            IPasswordHashManager passwordHashManager, IAccountNameCalculator accountNameCalculator)
        {
            _personDataApiRepository = personDataApiRepository;
            _clinicRepository = clinicRepository;
            _userTypeRepository = userTypeRepository;

            _passwordHashManager = passwordHashManager;
            _accountNameCalculator = accountNameCalculator;
        }

        public IEnumerable<ClinicUserStorageModel> GetList()
        {
            var clinic = _clinicRepository.GetModels().ToList();
            var userTypeId = _userTypeRepository.GetModels().FirstOrDefault(model => model.UserType == UserType.ClinicUser).Id;

            var results = GetUsersByFirstClinic(clinic[0].Id, userTypeId).Union(GetUsersBySecondClinic(clinic[1].Id, userTypeId));

            return results;
        }

        protected virtual IEnumerable<ClinicUserStorageModel> GetUsersByFirstClinic(int clinicId, int userTypeId)
        {
            return new List<ClinicUserStorageModel>
            {
                new ClinicUserStorageModel
                {
                    Id = 0,
                    ClinicId = clinicId,
                    IsDischargeResponsiblePerson = true,
                    User = new UserStorageModel
                    {
                        Account = new AccountStorageModel
                        {
                            Id = 0,
                            IsBlocked = false,
                            Login = "Поликлиника1Тест1",
                            HashedPassword = _passwordHashManager.GetPasswordHash("Madrid"),
                        },
                        Id = 0,
                        Name = "Поликлиника 1 Тест 1",
                        UserTypeId = userTypeId,
                    }
                },
                new ClinicUserStorageModel
                {
                    Id = 0,
                    ClinicId = clinicId,
                    IsDischargeResponsiblePerson = false,
                    User = new UserStorageModel
                    {
                        Account = new AccountStorageModel
                        {
                            Id = 0,
                            IsBlocked = false,
                            Login = "Поликлиника1Тест2",
                            HashedPassword = _passwordHashManager.GetPasswordHash("Berlin"),
                        },
                        Id = 0,
                        Name = "Поликлиника 1 Тест 2",
                        UserTypeId = userTypeId
                    }
                },
                new ClinicUserStorageModel
                {
                    Id = 0,
                    ClinicId = clinicId,
                    IsDischargeResponsiblePerson = false,
                    User = new UserStorageModel
                    {
                        Account = new AccountStorageModel
                        {
                            Id = 0,
                            IsBlocked = false,
                            Login = "Поликлиника1Тест3",
                            HashedPassword = _passwordHashManager.GetPasswordHash("Paris"),
                        },
                        Id = 0,
                        Name = "Поликлиника 1 Тест 3",
                        UserTypeId = userTypeId
                    }
                },
            };
        }

        protected virtual IEnumerable<ClinicUserStorageModel> GetUsersBySecondClinic(int clinicId, int userTypeId)
        {
            return new List<ClinicUserStorageModel>
            {
                new ClinicUserStorageModel
                {
                    Id = 0,
                    ClinicId = clinicId,
                    IsDischargeResponsiblePerson = true,
                    User = new UserStorageModel
                    {
                        Account = new AccountStorageModel
                        {
                            Id = 0,
                            IsBlocked = false,
                            Login = "Поликлиника2Тест1",
                            HashedPassword = _passwordHashManager.GetPasswordHash("London"),
                        },
                        Id = 0,
                        Name = "Поликлиника 2 Тест 1",
                        UserTypeId = userTypeId
                    }
                },
                new ClinicUserStorageModel
                {
                    Id = 0,
                    ClinicId = clinicId,
                    IsDischargeResponsiblePerson = false,
                    User = new UserStorageModel
                    {
                        Account = new AccountStorageModel
                        {
                            Id = 0,
                            IsBlocked = false,
                            Login = "Поликлиника2Тест2",
                            HashedPassword = _passwordHashManager.GetPasswordHash("Tokyo"),
                        },
                        Id = 0,
                        Name = "Поликлиника 2 Тест 2",
                        UserTypeId = userTypeId
                    }
                },
                new ClinicUserStorageModel
                {
                    Id = 0,
                    ClinicId = clinicId,
                    IsDischargeResponsiblePerson = false,
                    User = new UserStorageModel
                    {
                        Account = new AccountStorageModel
                        {
                            Id = 0,
                            IsBlocked = false,
                            Login = "Поликлиника3Тест3",
                            HashedPassword = _passwordHashManager.GetPasswordHash("Prague"),
                        },
                        Id = 0,
                        Name = "Поликлиника 3 Тест 3",
                        UserTypeId = userTypeId
                    }
                },
            };
        }
    }
}
