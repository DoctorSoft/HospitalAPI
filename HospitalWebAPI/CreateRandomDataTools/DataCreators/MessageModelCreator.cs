using System;
using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using DataBaseTools.Interfaces;
using Enums.Enums;
using HelpingTools.Interfaces;
using StorageModels.Models.MailboxModels;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class MessageModelCreator : IMessageModelCreator
    {
        private readonly IExtendedRandom _extendedRandom;

        private readonly IDataBaseContext _context;

        public MessageModelCreator(IExtendedRandom extendedRandom, IDataBaseContext context)
        {
            this._extendedRandom = extendedRandom;
            _context = context;
        }

        public IEnumerable<MessageStorageModel> GetList()
        {
            var titles = new List<string>
            {
                "Warning",
                "Attention",
                "Error",
                "Fatal Error",
                "Success",
                "Notice",
                "Message",
                "Hint"
            };

            var result = new List<MessageStorageModel>();
            var users = _context.Set<UserStorageModel>().ToList();

            foreach (var userTo in users)
            {
                var messagesCount = 0; //extendedRandom.Next(5, 15);
                for (var i = 0; i < messagesCount; i++)
                {
                    var nextUserFrom = _extendedRandom.GetRandomValueFromList(users);
                    var message = new MessageStorageModel
                    {
                        Date = DateTime.Now,
                        Text = $"{_extendedRandom.GetRandomValueFromList(titles)} {_extendedRandom.Next(1000)}",
                        IsRead = false,
                        MessageType = _extendedRandom.GetRandomBool() ? MessageType.UserMessage : MessageType.WarningMessage,
                        ShowStatus = TwoSideShowStatus.Showed,
                        Title = $"{_extendedRandom.GetRandomValueFromList(titles)} {_extendedRandom.Next(1000)}",
                        UserFromId = nextUserFrom.Id,
                        UserToId = userTo.Id
                    };
                    result.Add(message);
                }
            }

            return result;
        }
    }
}
