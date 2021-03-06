﻿using System;
using System.Collections.Generic;
using CreateRandomDataTools.Interfaces;
using CreateRandomDataTools.Interfaces.CommonInterfaces;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using DataBaseTools.Interfaces;
using Resources;
using StorageModels.Interfaces;

namespace CreateRandomDataTools.Fillers
{
    public class DataBaseInfoFiller : IDataBaseInfoFiller
    {
        private readonly IClinicModelCreator _clinicModelCreator;
        private readonly ISettingsItemCreator _settingsItemCreator;
        private readonly IFunctionalGroupModelCreator _functionalGroupModelCreator;
        private readonly IFunctionModelCreator _functionModelCreator;
        private readonly IHospitalModelCreator _hospitalModelCreator;
        private readonly ISectionModelCreator _sectionModelCreator;
        private readonly IUserTypeModelCreator _userTypeModelCreator;
        private readonly IClinicUserModelCreator _clinicUserModelCreator;
        private readonly IHospitalUserModelCreator _hospitalUserModelCreator;
        private readonly IBotModelCreator _botModelCreator;
        private readonly IAdministratorAndReviewerModelsCreator _administratorAndReviewerModelsCreator;
        private readonly IUserFunctionModelCreator _userFunctionModelCreator;
        private readonly IReceptionUserModelCreator _receptionUserModelCreator;
        private readonly IClinicHospitalPrioritiesCreator _clinicHospitalPrioritiesCreator;
        private readonly IHospitalSectionProfileCreator _hospitalSectionProfileCreator;
        private readonly IMessageModelCreator _messageModelCreator;

        private readonly IDataBaseContext _dataBaseContext;
        private readonly ICreationSettingsModule _creationSettingsModule;

        private readonly string _startMessageText = "";


        public DataBaseInfoFiller(IDataBaseContext dataBaseContext, ICreationSettingsModule creationSettingsModule,
            IClinicModelCreator clinicModelCreator,
            IFunctionalGroupModelCreator functionalGroupModelCreator, IFunctionModelCreator functionModelCreator,
            IHospitalModelCreator hospitalModelCreator, ISectionModelCreator sectionModelCreator,
            IUserTypeModelCreator userTypeModelCreator, IClinicUserModelCreator clinicUserModelCreator,
            IHospitalUserModelCreator hospitalUserModelCreator, IBotModelCreator botModelCreator,
            IAdministratorAndReviewerModelsCreator administratorAndReviewerModelsCreator,
            IUserFunctionModelCreator userFunctionModelCreator,
            ISettingsItemCreator settingsItemCreator,
            IReceptionUserModelCreator receptionUserModelCreator, IClinicHospitalPrioritiesCreator clinicHospitalPrioritiesCreator, IHospitalSectionProfileCreator hospitalSectionProfileCreator, IMessageModelCreator messageModelCreator)
        {
            _dataBaseContext = dataBaseContext;
            _creationSettingsModule = creationSettingsModule;

            _startMessageText = MainResources.FillerLoadingMessage;

            _clinicModelCreator = clinicModelCreator;
            _functionalGroupModelCreator = functionalGroupModelCreator;
            _functionModelCreator = functionModelCreator;
            _hospitalModelCreator = hospitalModelCreator;
            _sectionModelCreator = sectionModelCreator;
            _userTypeModelCreator = userTypeModelCreator;
            _clinicUserModelCreator = clinicUserModelCreator;
            _hospitalUserModelCreator = hospitalUserModelCreator;
            _botModelCreator = botModelCreator;
            _administratorAndReviewerModelsCreator = administratorAndReviewerModelsCreator;
            _userFunctionModelCreator = userFunctionModelCreator;
            _settingsItemCreator = settingsItemCreator;
            _receptionUserModelCreator = receptionUserModelCreator;
            _clinicHospitalPrioritiesCreator = clinicHospitalPrioritiesCreator;
            _hospitalSectionProfileCreator = hospitalSectionProfileCreator;
            _messageModelCreator = messageModelCreator;
        }

        public void FillDataBase(Func<string, bool> showStatusFunction = null)
        {
            var percents = 0.0;

            const double percentIncrementation = 100 / 16.0;

            //FillSectionModels(showStatusFunction, percents += percentIncrementation);
            //FillHospitalModels(showStatusFunction, percents += percentIncrementation);
            //FillClinicModels(showStatusFunction, percents += percentIncrementation);
            //FillSettingsItemModels(showStatusFunction, percents += percentIncrementation);
            //FillUserTypeModels(showStatusFunction, percents += percentIncrementation);
            //FillFunctionModels(showStatusFunction, percents += percentIncrementation);
            //FillFunctionalGroupModels(showStatusFunction, percents += percentIncrementation);
            //FillClinicUserModels(showStatusFunction, percents += percentIncrementation);
            //FillHospitalUserModels(showStatusFunction, percents += percentIncrementation);
            //FillReceptionUserModels(showStatusFunction, percents += percentIncrementation);
            //FillBotModels(showStatusFunction, percents += percentIncrementation);
            //FillAdministratorAndReviewerModels(showStatusFunction, percents += percentIncrementation);
            FillClinicHospitalPriorityModels(showStatusFunction, percents += percentIncrementation);
            //FillUserFunctionModels(showStatusFunction, percents += percentIncrementation);
            //FillHospitalSectionProfileModels(showStatusFunction, percents + percentIncrementation);
            //FillMessageModels(showStatusFunction, 100.0);
        }

        protected virtual void FillList<T>(IEnumerable<T> models, Func<string, bool> showStatusFunction = null, double percentCount = 0, bool fillApprove = true)
            where T: class, IIdModel
        {
            if (!fillApprove)
            {
                return;
            }

            var fillList = models;

            if (fillList == null)
            {
                return;
            }

            _dataBaseContext.Set<T>().AddRange(fillList);
            _dataBaseContext.SaveChanges();

            if (showStatusFunction != null)
            {
                showStatusFunction($"{_startMessageText} : {Math.Round(percentCount, 2)}%");
            }
        }

        protected virtual void FillSectionModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _sectionModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateSections();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillHospitalModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _hospitalModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateHospitals();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillClinicModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _clinicModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateClinics();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillSettingsItemModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _settingsItemCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateSettingsItems();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillFunctionModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _functionModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateFunctions();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillFunctionalGroupModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _functionalGroupModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateFunctionalGroups();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillUserTypeModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _userTypeModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateUserTypes();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillClinicUserModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _clinicUserModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateClinicUsers();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillHospitalUserModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _hospitalUserModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateHospitalUsers();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillReceptionUserModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _receptionUserModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateReceptionUsers();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillBotModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _botModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateBots();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillAdministratorAndReviewerModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _administratorAndReviewerModelsCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateAdministratorsAndReviewers();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillUserFunctionModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _userFunctionModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateUserFunctions();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillClinicHospitalPriorityModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _clinicHospitalPrioritiesCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateClinicHospitalPriorities();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillHospitalSectionProfileModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _hospitalSectionProfileCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateHospitalSectionProfiles();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillMessageModels(Func<string, bool> showStatusFunction = null, double percentCount = 0)
        {
            var models = _messageModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateMessages();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }
    }
}
