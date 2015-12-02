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
    public class AdministratorModelCreator : IAdministratorModelCreator
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly IUserTypeRepository _userTypeRepository;

        private readonly IPasswordHashManager _passwordHashManager;
        private readonly IAccountNameCalculator _accountNameCalculator;

        private const string AdminLogin = "Admin";
        private const string AdminPassword = "12345";

        public AdministratorModelCreator(IClinicRepository clinicRepository, IUserTypeRepository userTypeRepository,
            IPasswordHashManager passwordHashManager, IAccountNameCalculator accountNameCalculator)
        {
            _clinicRepository = clinicRepository;
            _userTypeRepository = userTypeRepository;

            _passwordHashManager = passwordHashManager;
            _accountNameCalculator = accountNameCalculator;
        }
        public IEnumerable<UserStorageModel> GetList()
        {
            var userTypeId = _userTypeRepository.GetModels().FirstOrDefault(model => model.UserType == UserType.Administrator).Id;

            var results = new List<UserStorageModel>
            {
                GetAdmin(userTypeId)
            };

            return results;
        }

        protected virtual UserStorageModel GetAdmin(int userTypeId)
        {
            var adminUser = new UserStorageModel
            {
                Name = AdminLogin,
                UserTypeId = userTypeId,
                Account = new AccountStorageModel
                {
                    HashedPassword = _passwordHashManager.GetPasswordHash(AdminPassword),
                    IsBlocked = false,
                    Login = AdminLogin,
                }
            };

            return adminUser;
        }
    }
}
