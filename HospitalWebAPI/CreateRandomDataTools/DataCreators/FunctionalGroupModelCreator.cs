using System;
using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using Enums.Enums;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.FunctionModels;

namespace CreateRandomDataTools.DataCreators
{
    public class FunctionalGroupModelCreator : IFunctionalGroupModelCreator
    {
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IFunctionRepository _functionRepository;
        private const string GroupNameMark = "Group";
        private readonly Dictionary<UserType, Func<IEnumerable<FunctionStorageModel>, IEnumerable<GroupFunctionStorageModel>>> dictionary;

        public FunctionalGroupModelCreator(IUserTypeRepository userTypeRepository, IFunctionRepository functionRepository)
        {
            _userTypeRepository = userTypeRepository;
            _functionRepository = functionRepository;

            dictionary = new Dictionary<UserType, Func<IEnumerable<FunctionStorageModel>, IEnumerable<GroupFunctionStorageModel>>>
            {
                {UserType.ClinicUser, GetFunctionsForClinicUser},
                {UserType.HospitalUser, GetFunctionsForHospitalUser},
                {UserType.Administrator, GetFunctionsForAdministratorUser},
                {UserType.Bot, GetFunctionsForBotUser},
                {UserType.Reviewer, GetFunctionsForReviewerUser}
            };
        }

        public IEnumerable<FunctionalGroupStorageModel> GetList()
        {
            var userTypes = _userTypeRepository.GetModels().ToList();
            var functions = _functionRepository.GetModels().ToList();

            var results = userTypes.Select(model => new FunctionalGroupStorageModel
            {
                Name = string.Format("{0}{1}", model.Name, GroupNameMark),
                UserTypeId = model.Id,
                GroupFunctions = dictionary[model.UserType](functions).ToList()
                
            }).ToList();

            return results;
        }

        protected virtual GroupFunctionStorageModel GenerateGroupFunctionStorageModelWrap(
            IEnumerable<FunctionStorageModel> functions, FunctionIdentityName identityName)
        {
            return new GroupFunctionStorageModel
            {
                FunctionId = functions.FirstOrDefault(model => model.FunctionIdentityName == identityName).Id
            };
        }

        protected virtual IEnumerable<GroupFunctionStorageModel> GetFunctionsByDiapason(int minValue, int maxValue, IEnumerable<FunctionStorageModel> functions, bool addLogOutFunction = true)
        {
            var results =
                Enum.GetValues(typeof(FunctionIdentityName))
                    .Cast<FunctionIdentityName>()
                    .Where(name => ((int)name) >= minValue && ((int)name) < maxValue)
                    .Select(name => GenerateGroupFunctionStorageModelWrap(functions, name))
                    .ToList();

            if (addLogOutFunction)
            {
                results.Add(GenerateGroupFunctionStorageModelWrap(functions, FunctionIdentityName.LogOut));
            }

            return results;
        }

        protected virtual IEnumerable<GroupFunctionStorageModel> GetFunctionsForHospitalUser(IEnumerable<FunctionStorageModel> functions)
        {
            return GetFunctionsByDiapason(100, 200, functions);
        }

        protected virtual IEnumerable<GroupFunctionStorageModel> GetFunctionsForClinicUser(IEnumerable<FunctionStorageModel> functions)
        {
            return GetFunctionsByDiapason(200, 300, functions);
        }

        protected virtual IEnumerable<GroupFunctionStorageModel> GetFunctionsForBotUser(IEnumerable<FunctionStorageModel> functions)
        {
            return GetFunctionsByDiapason(300, 400, functions, false);
        }

        protected virtual IEnumerable<GroupFunctionStorageModel> GetFunctionsForAdministratorUser(IEnumerable<FunctionStorageModel> functions)
        {
            return GetFunctionsByDiapason(400, 500, functions);
        }

        protected virtual IEnumerable<GroupFunctionStorageModel> GetFunctionsForReviewerUser(IEnumerable<FunctionStorageModel> functions)
        {
            return GetFunctionsByDiapason(500, 600, functions);
        }
    }
}
