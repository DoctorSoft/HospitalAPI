using System.Linq;
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
            var Clinic3Test1 = new PasswordHashManager().GetPasswordHash("Wolf25Matt");
            var Clinic3Test2 = new PasswordHashManager().GetPasswordHash("Battle2Field");
            var Clinic5Test1 = new PasswordHashManager().GetPasswordHash("Rocker11Face");
            var Clinic5Test2 = new PasswordHashManager().GetPasswordHash("Port75Land");
            var Clinic5Test3 = new PasswordHashManager().GetPasswordHash("Ameno23Rika");
            var Clinic4Test3 = new PasswordHashManager().GetPasswordHash("Door45Trap");
            var Clinic6Test3 = new PasswordHashManager().GetPasswordHash("Bool07Dog");
            var Hospital3Test3 = new PasswordHashManager().GetPasswordHash("Some25Song");
        }
    }
}
