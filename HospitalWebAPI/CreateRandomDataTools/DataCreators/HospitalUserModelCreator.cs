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
            var hospital = _hospitalRepository.GetModels().FirstOrDefault();
            var userTypeId = _userTypeRepository.GetModels().FirstOrDefault(model => model.UserType == UserType.HospitalUser).Id;

            var results = GetUsersByHospital(hospital.Id, userTypeId);

            return results;
        }

        protected virtual IEnumerable<HospitalUserStorageModel> GetUsersByHospital(int hospitalId, int userTypeId)
        {
            return new List<HospitalUserStorageModel>
            {
                new HospitalUserStorageModel
                {
                    Id = 0,
                    HospitalId = hospitalId,
                    User = new UserStorageModel
                    {
                        Account = new AccountStorageModel
                        {
                            Id = 0,
                            HashedPassword = _passwordHashManager.GetPasswordHash("Vienna"),
                            IsBlocked = false,
                            Login = "БольницаТест"
                        },
                        Id = 0,
                        Name = "Больница Тест",
                        UserTypeId = userTypeId
                    }
                }
            };
        }
    }
}
