using System;
using System.Data.Entity;
using System.Linq;
using Enums.Enums;
using RepositoryTools.Interfaces.PrivateInterfaces.MailboxRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers;
using ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers.Entities;
using ServiceModels.ServiceCommands.NoticesCommands;
using Services.Interfaces.AuthorizationServices;
using Services.Interfaces.NoticesService;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.MailboxModels;
using StorageModels.Models.UserModels;

namespace Services.NoticesService
{
    public class NoticesService : INoticesService
    {
        private readonly IMessageRepository _messageRepository;

        private readonly ITokenManager _tokenManager;

        private readonly IAuthorizationService _authorizationService;

        private readonly IUserRepository _userRepository;

        public NoticesService(IMessageRepository messageRepository, IAuthorizationService authorizationService, ITokenManager tokenManager, IUserRepository userRepository)
        {
            this._messageRepository = messageRepository;
            _authorizationService = authorizationService;
            _tokenManager = tokenManager;
            _userRepository = userRepository;
        }

        public GetClinicNoticesPageInformationCommandAnswer GetClinicNoticesPageInformation(
            GetClinicNoticesPageInformationCommand command)
        {
            return new GetClinicNoticesPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };
        }

        public GetHospitalNoticesPageInformationCommandAnswer GetHospitalNoticesPageInformation(
            GetHospitalNoticesPageInformationCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);

            var results = _messageRepository
                .GetModels()
                .Where(model => model.UserToId == user.Id)
                .Select(model => new MessageTableItem
                {
                    SendDate = model.Date,
                    AuthorName = user.Name,
                    Title = model.Title,
                    IsRead = model.IsRead,
                    MessageId = model.Id
                });

            return new GetHospitalNoticesPageInformationCommandAnswer
            {
                Token = (Guid)command.Token,
                Messages = results.ToList()
            };
        }

        public GetSendDistributiveMessagesPageInformationCommandAnswer GetSendDistributiveMessagesPageInformation(
            GetSendDistributiveMessagesPageInformationCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);

            var messages = ((IDbSet<UserStorageModel>) _userRepository.GetModels())
                .Include(model => model.UserType)
                .Where(model => model.UserType.UserType == UserType.ClinicUser)
                .ToList()
                .Select(model => new MessageStorageModel
                {
                    Date = DateTime.Now,
                    IsRead = false,
                    MessageType = MessageType.UserMessage,
                    ShowStatus = TwoSideShowStatus.Showed,
                    Text = command.Text,
                    Title = command.Title,
                    UserFromId = user.Id,
                    UserToId = model.Id
                });

            foreach (var message in messages)
            {
                _messageRepository.Add(message);
            }
            _messageRepository.SaveChanges();

            return new GetSendDistributiveMessagesPageInformationCommandAnswer
            {
                Token = command.Token.Value
            };
        }
    }
}
