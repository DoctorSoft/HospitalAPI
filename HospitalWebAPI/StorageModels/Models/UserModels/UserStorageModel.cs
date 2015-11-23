using System.Collections;
using System.Collections.Generic;
using StorageModels.Enums;
using StorageModels.Interfaces;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.FunctionModels;
using StorageModels.Models.MailboxModels;

namespace StorageModels.Models.UserModels
{
    public class UserStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public string Name { get; set; }
        public UserType UserType { get; set; }

        //

        public AccountStorageModel Account { get; set; }
        public IEnumerable<UserFunctionStorageModel> UserFunctions { get; set; }
        public ClinicUserStorageModel ClinicUser { get; set; }
        public HospitalUserStorageModel HospitalUser { get; set; }
        public IEnumerable<MessageStorageModel> MessagesTo { get; set; }
        public IEnumerable<MessageStorageModel> MessageFrom { get; set; }
    }
}
