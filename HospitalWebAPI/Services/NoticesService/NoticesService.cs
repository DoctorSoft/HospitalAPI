using System;
using System.Data.Entity;
using System.Linq;
using Enums.Enums;
using HandleToolsInterfaces.RepositoryHandlers;
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

        private readonly ITwoSideShowingHandler<MessageStorageModel> _messageShowingHandler; 

        public NoticesService(IMessageRepository messageRepository, IAuthorizationService authorizationService,
            ITokenManager tokenManager, IUserRepository userRepository, ITwoSideShowingHandler<MessageStorageModel> messageShowingHandler)
        {
            this._messageRepository = messageRepository;
            _authorizationService = authorizationService;
            _tokenManager = tokenManager;
            _userRepository = userRepository;
            this._messageShowingHandler = messageShowingHandler;
        }

        public GetClinicNoticesPageInformationCommandAnswer GetClinicNoticesPageInformation(
            GetClinicNoticesPageInformationCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);

            var results = ((IDbSet<MessageStorageModel>)_messageRepository.GetModels())
                .Include(model => model.UserFrom)
                .Where(model => model.UserToId == user.Id)
                .Where(model => model.ShowStatus == TwoSideShowStatus.ToSideOnly || model.ShowStatus == TwoSideShowStatus.Showed)
                .Select(model => new MessageTableItem
                {
                    SendDate = model.Date,
                    AuthorName = model.UserFrom.Name,
                    Title = model.Title,
                    IsRead = model.IsRead,
                    MessageId = model.Id
                }).ToList();

            return new GetClinicNoticesPageInformationCommandAnswer
            {
                Token = (Guid) command.Token,
                Messages = results.ToList()
            };
        }

        public GetHospitalNoticesPageInformationCommandAnswer GetHospitalNoticesPageInformation(
            GetHospitalNoticesPageInformationCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);

            var results = ((IDbSet<MessageStorageModel>)_messageRepository.GetModels())
                .Include(model => model.UserFrom)
                .Where(model => model.UserToId == user.Id)
                .Where(model => model.ShowStatus == TwoSideShowStatus.ToSideOnly || model.ShowStatus == TwoSideShowStatus.Showed)
                .Select(model => new MessageTableItem
                {
                    SendDate = model.Date,
                    AuthorName = model.UserFrom.Name,
                    Title = model.Title,
                    IsRead = model.IsRead,
                    MessageId = model.Id
                });

            return new GetHospitalNoticesPageInformationCommandAnswer
            {
                Token = (Guid) command.Token,
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

        public GetClinicMessageByIdCommandAnswer GetClinicMessageById(GetClinicMessageByIdCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);

            var message = ((IDbSet<MessageStorageModel>) _messageRepository.GetModels())
                .Include(model => model.UserFrom)
                .Include(model => model.UserTo)
                .FirstOrDefault(model => model.UserToId == user.Id && model.Id == command.MessageId);

            message.IsRead = true;
            _messageRepository.Update(message.Id, message);
            _messageRepository.SaveChanges();

            var result = new GetClinicMessageByIdCommandAnswer
            {
                AuthorId = message.UserFromId,
                AuthorName = message.UserFrom.Name,
                Text = message.Text,
                Title = message.Title,
                Token = command.Token.Value
            };

            return result;
        }

        public RemoveClinicMessageByIdCommandAnswer RemoveClinicMessageById(RemoveClinicMessageByIdCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);

            var message = _messageRepository.GetModels()
                .FirstOrDefault(model => model.UserToId == user.Id && model.Id == command.MessageId);

            _messageShowingHandler.HideModelFromToSide(message);
            _messageRepository.Update(message.Id, message);
            _messageRepository.SaveChanges();

            return new RemoveClinicMessageByIdCommandAnswer
            {
                Token = command.Token.Value
            };
        }
    }
}
