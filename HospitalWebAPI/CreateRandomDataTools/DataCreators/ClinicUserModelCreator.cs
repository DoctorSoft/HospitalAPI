using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using HelpingTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.AnotherRepositories.RemoteAPIRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Enums;
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
            var clinics = _clinicRepository.GetModels().ToList();
            var userTypeId = _userTypeRepository.GetModels().FirstOrDefault(model => model.UserType == UserType.ClinicUser).Id;

            var results = clinics.SelectMany(model => GetUsersByClinic(model.Id, userTypeId));

            return results;
        }

        protected virtual IEnumerable<ClinicUserStorageModel> GetUsersByClinic(int clinicId, int userTypeId)
        {
            for (var i = 0; i < UsersInOneClinic; i++)
            {
                var userName = _personDataApiRepository.GetModelById(0);
                var nextUser = new ClinicUserStorageModel
                {
                    ClinicId = clinicId,
                    User = new UserStorageModel
                    {
                        Name = string.Format("{0} {1}", userName.FirstName, userName.LastName),
                        UserTypeId = userTypeId,
                        Account = new AccountStorageModel
                        {
                            HashedPassword = _passwordHashManager.GetPasswordHash(StandardPassword),
                            IsBlocked = false,
                            Login = _accountNameCalculator.GetAccountName(userName.FirstName, userName.LastName),
                        }
                    }
                };

                yield return nextUser;
            }
        }
    }
}
