using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using DataBaseTools.Interfaces;
using Enums.Enums;
using HelpingTools.Interfaces;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class AdministratorAndReviewerModelsCreator : IAdministratorAndReviewerModelsCreator
    {
        private readonly IDataBaseContext _context;

        private readonly IPasswordHashManager _passwordHashManager;

        private const string AdminLogin = "Admin";
        private const string AdminPassword = "12345";

        private const string ReviewerLogin = "Смотритель";
        private const string ReviewerPassword = "12345";

        public AdministratorAndReviewerModelsCreator(
            IPasswordHashManager passwordHashManager, IDataBaseContext context)
        {
            _passwordHashManager = passwordHashManager;
            _context = context;
        }
        public IEnumerable<UserStorageModel> GetList()
        {
            var adminTypeId = _context.Set<UserTypeStorageModel>().FirstOrDefault(model => model.UserType == UserType.Administrator).Id;
            var reviewerTypeId = _context.Set<UserTypeStorageModel>().FirstOrDefault(model => model.UserType == UserType.Reviewer).Id;

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
