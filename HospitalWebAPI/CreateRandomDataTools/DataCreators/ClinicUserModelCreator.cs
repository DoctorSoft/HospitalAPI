using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using DataBaseTools.Interfaces;
using Enums.Enums;
using HelpingTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.AnotherRepositories.RemoteAPIRepositories;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class ClinicUserModelCreator : IClinicUserModelCreator
    {
        private readonly IPersonDataAPIRepository _personDataApiRepository;
        private readonly IDataBaseContext _context;

        private readonly IPasswordHashManager _passwordHashManager;
        private readonly IAccountNameCalculator _accountNameCalculator;

        private const int UsersInOneClinic = 2;
        private const string StandardPassword = "12345";

        public ClinicUserModelCreator(IPersonDataAPIRepository personDataApiRepository, 
            IPasswordHashManager passwordHashManager, IAccountNameCalculator accountNameCalculator, IDataBaseContext context)
        {
            _personDataApiRepository = personDataApiRepository;

            _passwordHashManager = passwordHashManager;
            _accountNameCalculator = accountNameCalculator;
            _context = context;
        }

        public IEnumerable<ClinicUserStorageModel> GetList()
        {
            var clinic = _context.Set<ClinicStorageModel>().ToList();
            var userTypeId = _context.Set<UserTypeStorageModel>().FirstOrDefault(model => model.UserType == UserType.ClinicUser).Id;

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
                            Login = "Clinic4Test1",
                            HashedPassword = _passwordHashManager.GetPasswordHash("Foot69Wand"),
                        },
                        Id = 0,
                        Name = "Clinic 4 User 1",
                        UserTypeId = userTypeId,
                    }
                },
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
                            Login = "Clinic4Test2",
                            HashedPassword = _passwordHashManager.GetPasswordHash("KeTTle77Pot"),
                        },
                        Id = 0,
                        Name = "Clinic 4 User 1",
                        UserTypeId = userTypeId,
                    }
                }
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
                            Login = "Clinic6Test1",
                            HashedPassword = _passwordHashManager.GetPasswordHash("Mess48Age18"),
                        },
                        Id = 0,
                        Name = "Clinic 6 User 1",
                        UserTypeId = userTypeId,
                    }
                },
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
                            Login = "Clinic6Test2",
                            HashedPassword = _passwordHashManager.GetPasswordHash("Cat8KeyBoardF1"),
                        },
                        Id = 0,
                        Name = "Clinic 6 User 2",
                        UserTypeId = userTypeId,
                    }
                }
            };

        }
    }
}
