﻿using System.Collections.Generic;
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

        //

        public AccountStorageModel Account { get; set; }
        public ICollection<UserFunctionStorageModel> UserFunctions { get; set; }
        public ClinicUserStorageModel ClinicUser { get; set; }
        public HospitalUserStorageModel HospitalUser { get; set; }
        public ICollection<MessageStorageModel> MessagesTo { get; set; }
        public ICollection<MessageStorageModel> MessagesFrom { get; set; }
        public UserTypeStorageModel UserType { get; set; }
        public ICollection<ReservationStorageModel> Reservations { get; set; }
        public ICollection<ReservationStorageModel> BehalfReservations { get; set; }

        //

        public int UserTypeId { get; set; }
    }
}
