using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using ServiceModels.ServiceCommandAnswers.ReceptionMarkingCommandAnswers;
using ServiceModels.ServiceCommandAnswers.ReceptionMarkingCommandAnswers.Entities;
using ServiceModels.ServiceCommands.ReceptionMarkingCommands;
using Services.Interfaces.ReceptionMarkingServices;
using Services.Interfaces.ServiceTools;

namespace Services.ReceptionMarkingServices
{
    public class ReceptionMarkingService : IReceptionMarkingService
    {
        private readonly IReservationRepository reservationRepository;

        private readonly ITokenManager tokenManager;

        private readonly IHospitalManager hospitalManager;

        public ReceptionMarkingService(IReservationRepository reservationRepository, ITokenManager tokenManager, IHospitalManager hospitalManager)
        {
            this.reservationRepository = reservationRepository;
            this.tokenManager = tokenManager;
            this.hospitalManager = hospitalManager;
        }

        public GetReceptionUserMarkClientsPageInformationCommandAnswer GetReceptionUserMarkClientsPageInformation(
            GetReceptionUserMarkClientsPageInformationCommand command)
        {
            var reservations = this.reservationRepository.GetModels();

            var user = tokenManager.GetUserByToken(command.Token.Value);
            var hospital = hospitalManager.GetClinicByUser(user);

            var table = reservations
                .Where(model => model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == hospital.Id)
                .Where(model => model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.Date == DateTime.Now)
                .Select(model => new ReceptionClientTableItem
                {
                    FirstName = model.Patient.FirstName,
                    LastName = model.Patient.LastName,
                    ClientCode = model.Patient.Code,
                    ClinicName = model.Clinic.Name,
                    SectionName = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Name
                })
                .ToList();

            var result = new GetReceptionUserMarkClientsPageInformationCommandAnswer
            {
                Token = (Guid)command.Token,
                Date = DateTime.Now,
                TableItems = table
            };

            return result;
        }

        public GetReceptionUserUnmarkClientsPageInformationCommandAnswer GetReceptionUserUnmarkClientsPageInformation(
            GetReceptionUserUnmarkClientsPageInformationCommand command)
        {

            var result = new GetReceptionUserUnmarkClientsPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };

            return result;
        }
    }
}
