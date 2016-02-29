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
            var clinic = _clinicRepository.GetModels().FirstOrDefault();
            var userTypeId = _userTypeRepository.GetModels().FirstOrDefault(model => model.UserType == UserType.ClinicUser).Id;

            var results = GetUsersByClinic(clinic.Id, userTypeId);

            return results;
        }

        protected virtual IEnumerable<ClinicUserStorageModel> GetUsersByClinic(int clinicId, int userTypeId)
        {
            return new List<ClinicUserStorageModel>
            {
                new ClinicUserStorageModel
                {
                    Id = 0,
                    ClinicId = clinicId,
                    User = new UserStorageModel
                    {
                        Account = new AccountStorageModel
                        {
                            Id = 0,
                            IsBlocked = false,
                            Login = "ДжонСмит",
                            HashedPassword = _passwordHashManager.GetPasswordHash(StandardPassword),
                        },
                        Id = 0,
                        Name = "Джон Смит",
                        UserTypeId = userTypeId
                    }
                }
            };
        }
    }
}
