using System;
using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces;
using Enums.Enums;
using HelpingTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.MailboxModels;

namespace CreateRandomDataTools.DataCreators
{
    public class MessageCreator : IMessageCreator
    {
        private readonly IExtendedRandom _extendedRandom;

        private readonly IUserRepository _userRepository;

        public MessageCreator(IExtendedRandom extendedRandom, IUserRepository userRepository)
        {
            this._extendedRandom = extendedRandom;
            _userRepository = userRepository;
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
            var users = _userRepository.GetModels().ToList();

            foreach (var userTo in users)
            {
                var messagesCount = _extendedRandom.Next(5, 15);
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
