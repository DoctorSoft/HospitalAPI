using System;
using System.Linq;
using RepositoryTools.Interfaces.PrivateInterfaces.MailboxRepositories;
using ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers;
using ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers.Entities;
using ServiceModels.ServiceCommands.NoticesCommands;
using Services.Interfaces.AuthorizationServices;
using Services.Interfaces.NoticesService;
using Services.Interfaces.ServiceTools;

namespace Services.NoticesService
{
    public class NoticesService : INoticesService
    {
        private readonly IMessageRepository _messageRepository;

        private readonly ITokenManager _tokenManager;

        private readonly IAuthorizationService _authorizationService;

        public NoticesService(IMessageRepository messageRepository, IAuthorizationService authorizationService, ITokenManager tokenManager)
        {
            this._messageRepository = messageRepository;
            _authorizationService = authorizationService;
            _tokenManager = tokenManager;
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
            return new GetSendDistributiveMessagesPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };
        }
    }
}
