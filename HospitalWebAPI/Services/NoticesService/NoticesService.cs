using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using DataBaseTools.Interfaces;
using Enums.Enums;
using HandleToolsInterfaces.RepositoryHandlers;
using HelpingTools.ExtentionTools;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers;
using ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers.Entities;
using ServiceModels.ServiceCommands.NoticesCommands;
using Services.Interfaces.AuthorizationServices;
using Services.Interfaces.NoticesService;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.MailboxModels;
using StorageModels.Models.UserModels;

namespace Services.NoticesService
{
    public class NoticesService : INoticesService
    {
        private readonly ITokenManager _tokenManager;

        private readonly IAuthorizationService _authorizationService;

        private readonly ITwoSideShowingHandler<MessageStorageModel> _messageShowingHandler;

        private readonly IHospitalManager _hospitalManager;

        private readonly IClinicManager _clinicManager;

        private readonly IDataBaseContext _context;

        public NoticesService(IAuthorizationService authorizationService,
            ITokenManager tokenManager, ITwoSideShowingHandler<MessageStorageModel> messageShowingHandler, IHospitalManager hospitalManager, IClinicManager clinicManager, IDataBaseContext context)
        {
            _authorizationService = authorizationService;
            _tokenManager = tokenManager;
            this._messageShowingHandler = messageShowingHandler;
            _hospitalManager = hospitalManager;
            _clinicManager = clinicManager;
            _context = context;
        }

        public GetClinicNoticesPageInformationCommandAnswer GetClinicNoticesPageInformation(
            GetClinicNoticesPageInformationCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);

            var today = DateTime.Now.Date;

            var results = ((IDbSet<MessageStorageModel>)_context.Set<MessageStorageModel>())
                .Include(model => model.UserFrom)
                .Where(model => model.UserToId == user.Id)
                .Where(model => model.ShowStatus == TwoSideShowStatus.ToSideOnly || model.ShowStatus == TwoSideShowStatus.Showed)
                .Where(model => command.OlnyUnRead != true || !model.IsRead)
                .Where(model => command.OnlyToday != true || model.Date == today)
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
                Messages = results.ToList(),
                OlnyUnRead = command.OlnyUnRead,
                OnlyToday = command.OnlyToday
            };
        }

        public GetHospitalNoticesPageInformationCommandAnswer GetHospitalNoticesPageInformation(
            GetHospitalNoticesPageInformationCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);

            var today = DateTime.Now.Date;

            var results = ((IDbSet<MessageStorageModel>)_context.Set<MessageStorageModel>())
                .Include(model => model.UserFrom)
                .Where(model => model.UserToId == user.Id)
                .Where(model => model.ShowStatus == TwoSideShowStatus.ToSideOnly || model.ShowStatus == TwoSideShowStatus.Showed)
                .Where(model => command.OlnyUnRead != true || !model.IsRead)
                .Where(model => command.OnlyToday != true || model.Date == today)
                .Take(100)
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
                Messages = results.ToList(),
                OlnyUnRead = command.OlnyUnRead,
                OnlyToday = command.OnlyToday
            };
        }

        private List<CommandAnswerError> ValidateGetSendDistributiveMessagesPageInformationCommand(
            GetSendDistributiveMessagesPageInformationCommand command)
        {
            var errors = new List<CommandAnswerError>(); 

            if (string.IsNullOrWhiteSpace(command.Text))
            {
                errors.Add(new CommandAnswerError
                {
                    FieldName = "Текст",
                    Title = "Текст не может пустым"
                });
            }

            if (string.IsNullOrWhiteSpace(command.Title))
            {
                errors.Add(new CommandAnswerError
                {
                    FieldName = "Заголовок",
                    Title = "Заголовок не может быть пустым"
                });
            }

            return errors;
        }

        public GetSendDistributiveMessagesPageInformationCommandAnswer GetSendDistributiveMessagesPageInformation(
            GetSendDistributiveMessagesPageInformationCommand command)
        {
            var errors = ValidateGetSendDistributiveMessagesPageInformationCommand(command);

            if (errors.Any())
            {
                return new GetSendDistributiveMessagesPageInformationCommandAnswer
                {
                    Token = command.Token.Value,
                    Title = command.Title,
                    Text = command.Text,
                    Errors = errors
                };
            }

            var user = _tokenManager.GetUserByToken(command.Token);

            var messages = ((IDbSet<UserStorageModel>) _context.Set<UserStorageModel>())
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
                _context.Set<MessageStorageModel>().Add(message);
            }
            _context.SaveChanges();

            return new GetSendDistributiveMessagesPageInformationCommandAnswer
            {
                Token = command.Token.Value,
                Title = command.Title,
                Text = command.Text,
                Errors = errors
            };
        }

        public GetClinicMessageByIdCommandAnswer GetClinicMessageById(GetClinicMessageByIdCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);

            var message = ((IDbSet<MessageStorageModel>) _context.Set<MessageStorageModel>())
                .Include(model => model.UserFrom)
                .Include(model => model.UserTo)
                .FirstOrDefault(model => model.UserToId == user.Id && model.Id == command.MessageId);

            message.IsRead = true;
            _context.Set<MessageStorageModel>().AddOrUpdate(message);
            _context.SaveChanges();

            var result = new GetClinicMessageByIdCommandAnswer
            {
                MessageId = message.Id,
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

            var message = _context.Set<MessageStorageModel>()
                .FirstOrDefault(model => model.UserToId == user.Id && model.Id == command.MessageId);

            _messageShowingHandler.HideModelFromToSide(message);
            _context.Set<MessageStorageModel>().AddOrUpdate(message);
            _context.SaveChanges();

            return new RemoveClinicMessageByIdCommandAnswer
            {
                Token = command.Token.Value
            };
        }
        public GetHospitalMessageByIdCommandAnswer GetHospitalMessageById(GetHospitalMessageByIdCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);

            var message = ((IDbSet<MessageStorageModel>)_context.Set<MessageStorageModel>())
                .Include(model => model.UserFrom)
                .Include(model => model.UserTo)
                .FirstOrDefault(model => model.UserToId == user.Id && model.Id == command.MessageId);

            message.IsRead = true;
            _context.Set<MessageStorageModel>().AddOrUpdate(message);
            _context.SaveChanges();

            var result = new GetHospitalMessageByIdCommandAnswer
            {
                MessageId = message.Id,
                AuthorId = message.UserFromId,
                AuthorName = message.UserFrom.Name,
                Text = message.Text,
                Title = message.Title,
                Token = command.Token.Value
            };

            return result;
        }

        public RemoveHospitalMessageByIdCommandAnswer RemoveHospitalMessageById(RemoveHospitalMessageByIdCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);

            var message = _context.Set<MessageStorageModel>()
                .FirstOrDefault(model => model.UserToId == user.Id && model.Id == command.MessageId);

            _messageShowingHandler.HideModelFromToSide(message);
            _context.Set<MessageStorageModel>().AddOrUpdate(message);
            _context.SaveChanges();

            return new RemoveHospitalMessageByIdCommandAnswer
            {
                Token = command.Token.Value
            };
        }

        public ShowDischargesListCommandAnswer ShowDischargesList(ShowDischargesListCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);

            var clinic = _clinicManager.GetClinicByUser(user);
            
            var results = _context.Set<DischargeStorageModel>()
                .Where(model => model.Message.UserTo.UserType.UserType == UserType.ClinicUser)
                .Where(model => model.Message.UserTo.ClinicUser.ClinicId == clinic.Id)
                .Select(model => new DischargeFileItem
                {
                    SentDate = model.Message.Date,
                    DischargeId = model.Id,
                    Doctor = model.Message.UserFrom.Name,
                    Hospital = model.Message.UserFrom.HospitalUser.Hospital.Name
                }).ToList();

            results.ForEach(item =>
            {
                item.Date = item.SentDate.ToCorrectDateString();
                item.Time = item.SentDate.ToString("t");
            });

            return new ShowDischargesListCommandAnswer
            {
                Token = command.Token.Value,
                Files = results
            };
        }

        public ShowPageToSendDischangeCommandAnswer ShowPageToSendDischange(ShowPageToSendDischangeCommand command)
        {
            var clinics = this._context.Set<ClinicStorageModel>().ToList();
            if (command.ClinicId == null)
            {
                command.ClinicId = clinics.FirstOrDefault().Id;
            }
            var clinicResults = clinics.Select(model => new KeyValuePair<int, string>(model.Id, model.Name)).ToList();

            return new ShowPageToSendDischangeCommandAnswer
            {
                Token = command.Token.Value,
                ClinicId = command.ClinicId.Value,
                Clinics = clinicResults,

            };
        }

        public static byte[] ReadFully(Stream input)
        {
            input.Position = 0;
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public SaveDischargeCommandAnswer SaveDischarge(SaveDischargeCommand command)
        {
            var user = _tokenManager.GetUserByToken(command.Token);

            var hospital = _hospitalManager.GetHospitalByUser(user);
            
            var responsiblePersonId = _context.Set<ClinicUserStorageModel>()
                .FirstOrDefault(model => model.ClinicId == command.ClinicId && model.IsDischargeResponsiblePerson).Id;

            var anotherPersonsFromClinic = _context.Set<ClinicUserStorageModel>()
                .Where(model => model.ClinicId == command.ClinicId && !model.IsDischargeResponsiblePerson).Select(model => model.Id).ToList();

            var titleMessage = $"Новая выписка была отправлена из больницы '{hospital.Name}'";
            var textMessage =
                $"Новая выписка была отправлена из больницы '{hospital.Name}'. Отправитель '{user.Name}'. Дата: '{DateTime.Now.ToCorrectDateString()} {DateTime.Now.ToString("t")}' ";

            var body = ReadFully(command.File);

            var discharge = new DischargeStorageModel
            {
                Id = 0,
                Name = command.FileName,
                Message = new MessageStorageModel
                {
                    Id = 0,
                    Title = titleMessage,
                    Date = DateTime.Now,
                    Text = textMessage,
                    ShowStatus = TwoSideShowStatus.Showed,
                    UserFromId = user.Id,
                    UserToId = responsiblePersonId,
                    IsRead = false,
                    MessageType = MessageType.WarningMessage,
                },
                Body = body,
                MimeType = command.ContentType
            };
            this._context.Set<DischargeStorageModel>().Add(discharge);

            foreach (var personId in anotherPersonsFromClinic)
            {
                var message = new MessageStorageModel
                {
                    Id = 0,
                    Title = titleMessage,
                    Date = DateTime.Now,
                    Text = textMessage,
                    ShowStatus = TwoSideShowStatus.Showed,
                    UserFromId = user.Id,
                    UserToId = personId,
                    IsRead = false,
                    MessageType = MessageType.WarningMessage,
                };
                _context.Set<MessageStorageModel>().Add(message);
            }
            _context.SaveChanges();

            return new SaveDischargeCommandAnswer
            {
                Token = command.Token.Value
            };
        }

        public DownloadDischargeCommandAnswer DownloadDischarge(DownloadDischargeCommand command)
        {
            var discharge =
                this._context.Set<DischargeStorageModel>().FirstOrDefault(model => model.Id == command.DischargeId);

            return new DownloadDischargeCommandAnswer
            {
                Token = command.Token.Value,
                Body = discharge.Body,
                FileName = discharge.Name,
                MimeType = discharge.MimeType
            };
        }
    }
}
