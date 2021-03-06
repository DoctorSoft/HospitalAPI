﻿using System;
using System.Collections.Generic;
using Enums.Enums;
using StorageModels.Interfaces;
using StorageModels.Models.UserModels;

namespace StorageModels.Models.MailboxModels
{
    public class MessageStorageModel : IIdModel, IShowStatusModel
    {
        public int Id { get; set; }
        public TwoSideShowStatus ShowStatus { get; set; }

        //

        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public MessageType MessageType { get; set; }
        public bool IsRead { get; set; }

        //

        public UserStorageModel UserTo { get; set; }
        public UserStorageModel UserFrom { get; set; }
        public ICollection<DischargeStorageModel> Discharges { get; set; } 

        //

        public int UserToId { get; set; }

        public int UserFromId { get; set; }
        
    }
}
