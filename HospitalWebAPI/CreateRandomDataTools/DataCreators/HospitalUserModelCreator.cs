using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using Enums.Enums;
using HelpingTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.AnotherRepositories.RemoteAPIRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class HospitalUserModelCreator : IHospitalUserModelCreator
    {
        private readonly IPersonDataAPIRepository _personDataApiRepository;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IUserTypeRepository _userTypeRepository;

        private readonly IPasswordHashManager _passwordHashManager;
        private readonly IAccountNameCalculator _accountNameCalculator;

        private const int UsersInOneHospital = 2;
        private const string StandardPassword = "12345";

        public HospitalUserModelCreator(IPersonDataAPIRepository personDataApiRepository, 
            IHospitalRepository hospitalRepository, IUserTypeRepository userTypeRepository,
            IPasswordHashManager passwordHashManager, IAccountNameCalculator accountNameCalculator)
        {
            _personDataApiRepository = personDataApiRepository;
            _hospitalRepository = hospitalRepository;
            _userTypeRepository = userTypeRepository;

            _passwordHashManager = passwordHashManager;
            _accountNameCalculator = accountNameCalculator;
        }
   
        public IEnumerable<HospitalUserStorageModel> GetList()
        {
            var hospitals = _hospitalRepository.GetModels().ToList();
            var userTypeId = _userTypeRepository.GetModels().FirstOrDefault(model => model.UserType == UserType.HospitalUser).Id;

            var results = hospitals.SelectMany(model => GetUsersByHospital(model.Id, userTypeId));

            return results;
        }

        protected virtual IEnumerable<HospitalUserStorageModel> GetUsersByHospital(int hospitalId, int userTypeId)
        {
            for (var i = 0; i < UsersInOneHospital; i++)
            {
                var userName = _personDataApiRepository.GetModelById(0);
                var nextUser = new HospitalUserStorageModel
                {
                    HospitalId = hospitalId,
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
