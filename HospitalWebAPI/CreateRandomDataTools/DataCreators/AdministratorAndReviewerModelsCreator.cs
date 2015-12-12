using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using Enums.Enums;
using HelpingTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class AdministratorAndReviewerModelsCreator : IAdministratorAndReviewerModelsCreator
    {
        private readonly IUserTypeRepository _userTypeRepository;

        private readonly IPasswordHashManager _passwordHashManager;

        private const string AdminLogin = "Admin";
        private const string AdminPassword = "12345";

        private const string ReviewerLogin = "Смотритель";
        private const string ReviewerPassword = "12345";

        public AdministratorAndReviewerModelsCreator(IUserTypeRepository userTypeRepository,
            IPasswordHashManager passwordHashManager)
        {
            _userTypeRepository = userTypeRepository;

            _passwordHashManager = passwordHashManager;
        }
        public IEnumerable<UserStorageModel> GetList()
        {
            var adminTypeId = _userTypeRepository.GetModels().FirstOrDefault(model => model.UserType == UserType.Administrator).Id;
            var reviewerTypeId = _userTypeRepository.GetModels().FirstOrDefault(model => model.UserType == UserType.Reviewer).Id;

            var results = new List<UserStorageModel>
            {
                GetAdmin(adminTypeId),
                GetReviewer(reviewerTypeId)
            };

            return results;
        }

        protected virtual UserStorageModel GetReviewer(int userTypeId)
        {
            var adminUser = new UserStorageModel
            {
                Name = ReviewerLogin,
                UserTypeId = userTypeId,
                Account = new AccountStorageModel
                {
                    HashedPassword = _passwordHashManager.GetPasswordHash(ReviewerPassword),
                    IsBlocked = false,
                    Login = ReviewerLogin,
                }
            };

            return adminUser;
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
