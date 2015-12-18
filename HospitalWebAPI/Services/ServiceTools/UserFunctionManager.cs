using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Enums.Enums;
using HandleToolsInterfaces.RepositoryHandlers;
using RepositoryTools.Interfaces.PrivateInterfaces.FunctionRepositories;
using ServiceModels.ModelTools.Entities;
using Services.Interfaces.ServiceTools;
using StorageModels.Models.FunctionModels;
using StorageModels.Models.UserModels;

namespace Services.ServiceTools
{
    public class UserFunctionManager : IUserFunctionManager
    {
        private readonly IUserFunctionRepository _userFunctionRepository;
        private readonly IFunctionRepository _functionRepository;

        private readonly IBlockAbleHandler _blockAbleHandler;
        private readonly ITokenManager _tokenManager;
        private readonly ISettingsManager _settingsManager;
        private readonly IClinicManager _clinicManager;
        private readonly IClinicReservationsManager _clinicReservationsManager;

        private readonly Dictionary<FunctionIdentityName, Func<Guid, bool>> _functions; 

        public UserFunctionManager(IUserFunctionRepository userFunctionRepository, IFunctionRepository functionRepository, IBlockAbleHandler blockAbleHandler, ITokenManager tokenManager, ISettingsManager settingsManager, IClinicManager clinicManager, IClinicReservationsManager clinicReservationsManager)
        {
            _userFunctionRepository = userFunctionRepository;
            _functionRepository = functionRepository;
            _blockAbleHandler = blockAbleHandler;
            _tokenManager = tokenManager;
            _settingsManager = settingsManager;
            _clinicManager = clinicManager;
            _clinicReservationsManager = clinicReservationsManager;

            _functions = new Dictionary<FunctionIdentityName, Func<Guid, bool>>
            {
                {FunctionIdentityName.HospitalUserChangeEmptyPlaces, this.IsHospitalUserChangeEmptyPlacesEnabled},
                {FunctionIdentityName.HospitalUserFillEmptyPlaces, this.IsHospitalUserFillEmptyPlacesFunctionEnables},
                {FunctionIdentityName.ClinicUserMakeRegistrations, this.IsClinicUserMakeRegistrationsEnabled},
                {FunctionIdentityName.ClinicUserBreakRegistrations, this.IsClinicUserBreakRegistrationsEnabled}
            };
        }

        protected virtual IEnumerable<UserFunctionStorageModel> GetUserFunctionsByUser(UserStorageModel user)
        {
            var result = _blockAbleHandler.GetAccessAbleModels(_userFunctionRepository.GetModels())
                .Where(model => model.UserId == user.Id)
                .ToList();

            return result;
        }

        protected virtual IEnumerable<FunctionStorageModel> GetFunctionsByUserFunctions(
            IEnumerable<UserFunctionStorageModel> models)
        {
            var functionIds = models.Select(model => model.FunctionId).ToList();
            var result =
                _blockAbleHandler.GetAccessAbleModels(_functionRepository.GetModels())
                    .Where(model => functionIds.Contains(model.Id))
                    .ToList();

            return result;
        }

        protected virtual bool IsHospitalUserFillEmptyPlacesFunctionEnables(Guid token)
        {
            return true;
        }

        public virtual bool IsClinicUserBreakRegistrationsEnabled(Guid token)
        {
            var firstCondition = IsClinicUserMakeRegistrationsEnabled(token);

            var user = _tokenManager.GetUserByToken(token);
            var clinic = _clinicManager.GetClinicByUser(user);

            if (clinic == null)
            {
                return false;
            }

            var reservations = _clinicReservationsManager.GetTodaysReservations(clinic);
            var secondCondition = reservations.Any();

            return firstCondition && secondCondition;
        }

        public virtual bool IsClinicUserMakeRegistrationsEnabled(Guid token)
        {
            var settings = _settingsManager.GetRegistrationSettings();
            var now = DateTime.Now.TimeOfDay;
            return settings.StartTime <= now && now <= settings.EndTime;
        }

        public virtual bool IsHospitalUserChangeEmptyPlacesEnabled(Guid token)
        {
            return true;
        }

        public virtual bool IsFunctionsEnabled(FunctionIdentityName function, Guid token)
        {
            return !_functions.ContainsKey(function) || _functions[function](token);
        }

        public IEnumerable<FunctionAccess> GetFunctionsByToken(Guid? token)
        {
            var user = _tokenManager.GetUserByToken(token);

            if (user == null)
            {
                return new List<FunctionAccess>();
            }

            var userFunctions = GetUserFunctionsByUser(user);
            var resultFunctions = GetFunctionsByUserFunctions(userFunctions);

            var functionAccesses = resultFunctions.Select(model => new FunctionAccess
            {
                AccessType = IsFunctionsEnabled(model.FunctionIdentityName, (Guid)token) ? AccessType.Accepted : AccessType.Disabled,
                FunctionStorageModel = model

            });

            return functionAccesses.ToList();
        }
    }
}
