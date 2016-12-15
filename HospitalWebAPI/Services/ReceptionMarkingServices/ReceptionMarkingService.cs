using System;
using System.Data.Entity.Migrations;
using System.Linq;
using DataBaseTools.Interfaces;
using ServiceModels.ServiceCommandAnswers.ReceptionMarkingCommandAnswers;
using ServiceModels.ServiceCommandAnswers.ReceptionMarkingCommandAnswers.Entities;
using ServiceModels.ServiceCommands.ReceptionMarkingCommands;
using Services.Interfaces.ReceptionMarkingServices;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.ClinicModels;

namespace Services.ReceptionMarkingServices
{
    public class ReceptionMarkingService : IReceptionMarkingService
    {
        private readonly ITokenManager tokenManager;

        private readonly IHospitalManager hospitalManager;

        private readonly IDataBaseContext _context;

        public ReceptionMarkingService(ITokenManager tokenManager, IHospitalManager hospitalManager, IDataBaseContext context)
        {
            this.tokenManager = tokenManager;
            this.hospitalManager = hospitalManager;
            _context = context;
        }

        public GetReceptionUserMarkClientsPageInformationCommandAnswer GetReceptionUserMarkClientsPageInformation(
            GetReceptionUserMarkClientsPageInformationCommand command)
        {
            var reservations = this._context.Set<ReservationStorageModel>();

            var user = tokenManager.GetUserByToken(command.Token.Value);
            var hospital = hospitalManager.GetHospitalByUser(user);

            var now = DateTime.Now.Date;

            var table = reservations
                .Where(model => model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == hospital.Id)
                .Where(model => model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.Date == now)
                .Where(model => model.CancelTime == null)
                .Select(model => new ReceptionClientTableItem
                {
                    FirstName = model.Patient.FirstName,
                    LastName = model.Patient.LastName,
                    ClientCode = model.Patient.Code,
                    ClinicName = model.Clinic.Name,
                    SectionName = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Name,
                    ReservationId = model.Id
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
            var reservations = this._context.Set<ReservationStorageModel>();

            var user = tokenManager.GetUserByToken(command.Token.Value);
            var hospital = hospitalManager.GetHospitalByUser(user);

            var now = DateTime.Now.Date;

            var table = reservations
                .Where(model => model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.HospitalId == hospital.Id)
                .Where(model => model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.Date == now)
                .Where(model => model.CancelTime != null)
                .Select(model => new ReceptionClientTableItem
                {
                    FirstName = model.Patient.FirstName,
                    LastName = model.Patient.LastName,
                    ClientCode = model.Patient.Code,
                    ClinicName = model.Clinic.Name,
                    SectionName = model.EmptyPlaceByTypeStatistic.EmptyPlaceStatistic.HospitalSectionProfile.Name,
                    ReservationId = model.Id
                })
                .ToList();

            var result = new GetReceptionUserUnmarkClientsPageInformationCommandAnswer
            {
                Token = (Guid)command.Token,
                Date = DateTime.Now,
                TableItems = table
            };

            return result;
        }

        public MarkClientAsArrivedCommandAnswer MarkClientAsArrived(MarkClientAsArrivedCommand command)
        {
            var reservations = _context.Set<ReservationStorageModel>();
            var reservation = reservations.FirstOrDefault(model => model.Id == command.ReservationId);

            reservation.CancelTime = DateTime.Now;
            _context.Set<ReservationStorageModel>().AddOrUpdate(reservation);
            _context.SaveChanges();

            var result = new MarkClientAsArrivedCommandAnswer
            {
                Token = (Guid)command.Token
            };

            return result;
        }

        public MarkClientAsArrivingCommandAnswer MarkClientAsArriving(MarkClientAsArrivingCommand command)
        {
            var reservations = _context.Set<ReservationStorageModel>();
            var reservation = reservations.FirstOrDefault(model => model.Id == command.ReservationId);

            reservation.CancelTime = null;
            _context.Set<ReservationStorageModel>().AddOrUpdate(reservation);
            _context.SaveChanges();

            var result = new MarkClientAsArrivingCommandAnswer
            {
                Token = (Guid)command.Token
            };

            return result;
        }
    }
}
