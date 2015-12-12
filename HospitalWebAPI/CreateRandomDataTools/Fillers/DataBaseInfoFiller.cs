using System;
using System.Collections.Generic;
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

        private readonly IDataBaseContext _dataBaseContext;
        private readonly ICreationSettingsModule _creationSettingsModule;

        private readonly string _startMessageText = "";


        public DataBaseInfoFiller(IDataBaseContext dataBaseContext, ICreationSettingsModule creationSettingsModule, IClinicModelCreator clinicModelCreator,
            IFunctionalGroupModelCreator functionalGroupModelCreator, IFunctionModelCreator functionModelCreator,
            IHospitalModelCreator hospitalModelCreator, ISectionModelCreator sectionModelCreator,  
            IUserTypeModelCreator userTypeModelCreator, IClinicUserModelCreator clinicUserModelCreator, IHospitalUserModelCreator hospitalUserModelCreator, IBotModelCreator botModelCreator,
            IAdministratorAndReviewerModelsCreator administratorAndReviewerModelsCreator, IUserFunctionModelCreator userFunctionModelCreator)
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
        }

        public void FillDataBase(Func<string, bool> showStatusFunction = null)
        {
            var percents = 0;

            const int percentIncrementation = 8;

            FillSectionModels(showStatusFunction, percents += percentIncrementation);
            FillHospitalModels(showStatusFunction, percents += percentIncrementation);
            FillClinicModels(showStatusFunction, percents += percentIncrementation);
            FillUserTypeModels(showStatusFunction, percents += percentIncrementation);
            FillFunctionModels(showStatusFunction, percents += percentIncrementation);
            FillFunctionalGroupModels(showStatusFunction, percents += percentIncrementation);
            FillClinicUserModels(showStatusFunction, percents += percentIncrementation);
            FillHospitalUserModels(showStatusFunction, percents += percentIncrementation);
            FillBotModels(showStatusFunction, percents += percentIncrementation);
            FillAdministratorAndReviewerModels(showStatusFunction, percents + percentIncrementation);
            FillUserFunctionModels(showStatusFunction, 100);
        }

        protected virtual void FillList<T>(IEnumerable<T> models, Func<string, bool> showStatusFunction = null, int percentCount = 0, bool fillApprove = true)
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
                showStatusFunction(string.Format("{0} : {1}%", _startMessageText, percentCount));
            }
        }

        protected virtual void FillSectionModels(Func<string, bool> showStatusFunction = null, int percentCount = 0)
        {
            var models = _sectionModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateSections();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillHospitalModels(Func<string, bool> showStatusFunction = null, int percentCount = 0)
        {
            var models = _hospitalModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateHospitals();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillClinicModels(Func<string, bool> showStatusFunction = null, int percentCount = 0)
        {
            var models = _clinicModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateClinics();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillFunctionModels(Func<string, bool> showStatusFunction = null, int percentCount = 0)
        {
            var models = _functionModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateFunctions();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillFunctionalGroupModels(Func<string, bool> showStatusFunction = null, int percentCount = 0)
        {
            var models = _functionalGroupModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateFunctionalGroups();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillUserTypeModels(Func<string, bool> showStatusFunction = null, int percentCount = 0)
        {
            var models = _userTypeModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateUserTypes();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillClinicUserModels(Func<string, bool> showStatusFunction = null, int percentCount = 0)
        {
            var models = _clinicUserModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateClinicUsers();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillHospitalUserModels(Func<string, bool> showStatusFunction = null, int percentCount = 0)
        {
            var models = _hospitalUserModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateHospitalUsers();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillBotModels(Func<string, bool> showStatusFunction = null, int percentCount = 0)
        {
            var models = _botModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateBots();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillAdministratorAndReviewerModels(Func<string, bool> showStatusFunction = null, int percentCount = 0)
        {
            var models = _administratorAndReviewerModelsCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateAdministratorsAndReviewers();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }

        protected virtual void FillUserFunctionModels(Func<string, bool> showStatusFunction = null, int percentCount = 0)
        {
            var models = _userFunctionModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateUserFunctions();

            FillList(models, showStatusFunction, percentCount, fillApprove);
        }
    }
}
