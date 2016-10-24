using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseModelConfigurations.Contexts;
using Enums.Enums;
using HelpingTools.CalculationTools;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.FunctionModels;
using StorageModels.Models.UserModels;

namespace AdminQuickFunctions
{
    class Program
    {
        public static void AddNewClinicUser()
        {
            var context = new TestDataBaseContext();

            var userType = context.Set<UserTypeStorageModel>().FirstOrDefault(model => model.UserType == UserType.ClinicUser);
            var clinic = context.Set<ClinicStorageModel>().ToList()[1]; // First Clinic 4 (or 6)

            var passwordHashManager = new PasswordHashManager();

            var user = new ClinicUserStorageModel
            {
                Id = 0,
                ClinicId = clinic.Id,
                IsDischargeResponsiblePerson = true,
                User = new UserStorageModel
                {
                    Id = 0,
                    Name = "Clinic 6 Tets 3",
                    UserTypeId =  userType.Id,
                    Account = new AccountStorageModel
                    {
                        Id = 0,
                        IsBlocked = false,
                        Login = "Clinic6Test3",
                        HashedPassword = passwordHashManager.GetPasswordHash("Wolf25Matt"),
                    }
                }
            };

            context.Set<ClinicUserStorageModel>().Add(user);

            context.SaveChanges();

            var functions =
                context.Set<FunctionStorageModel>()
                    .Where(model => ((long) model.FunctionIdentityName >= 200 && (long) model.FunctionIdentityName < 300) || model.FunctionIdentityName == FunctionIdentityName.LogOut)
                    .ToList();

            foreach (var function in functions)
            {
                var userFunction = new UserFunctionStorageModel
                {
                    FunctionId = function.Id,
                    IsBlocked = false,
                    UserId = user.Id
                };
                context.Set<UserFunctionStorageModel>().Add(userFunction);
            }

            context.SaveChanges();
        }

        static void Main(string[] args)
        {
            AddNewClinicUser();
        }
    }
}
