using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.CommonInterfaces;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using DataBaseTools.Interfaces;
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
        private readonly IClinicBotModelCreator _clinicBotModelCreator;
        private readonly IHospitalBotModelCreator _hospitalBotModelCreator;
        private readonly IAdministratorModelCreator _administratorModelCreator;
        private readonly IUserFunctionModelCreator _userFunctionModelCreator;

        private readonly IDataBaseContext _dataBaseContext;
        private readonly ICreationSettingsModule _creationSettingsModule;

        public DataBaseInfoFiller(IDataBaseContext dataBaseContext, IClinicModelCreator clinicModelCreator,
            IFunctionalGroupModelCreator functionalGroupModelCreator, IFunctionModelCreator functionModelCreator,
            IHospitalModelCreator hospitalModelCreator, ISectionModelCreator sectionModelCreator, ICreationSettingsModule creationSettingsModule, 
            IUserTypeModelCreator userTypeModelCreator, IClinicUserModelCreator clinicUserModelCreator, IHospitalUserModelCreator hospitalUserModelCreator, IClinicBotModelCreator clinicBotModelCreator,
            IHospitalBotModelCreator hospitalBotModelCreator, IAdministratorModelCreator administratorModelCreator, IUserFunctionModelCreator userFunctionModelCreator)
        {
            _dataBaseContext = dataBaseContext;

            _clinicModelCreator = clinicModelCreator;
            _functionalGroupModelCreator = functionalGroupModelCreator;
            _functionModelCreator = functionModelCreator;
            _hospitalModelCreator = hospitalModelCreator;
            _sectionModelCreator = sectionModelCreator;
            _creationSettingsModule = creationSettingsModule;
            _userTypeModelCreator = userTypeModelCreator;
            _clinicUserModelCreator = clinicUserModelCreator;
            _hospitalUserModelCreator = hospitalUserModelCreator;
            _clinicBotModelCreator = clinicBotModelCreator;
            _hospitalBotModelCreator = hospitalBotModelCreator;
            _administratorModelCreator = administratorModelCreator;
            _userFunctionModelCreator = userFunctionModelCreator;
        }

        public void FillDataBase()
        {
            FillSectionModels();
            FillHospitalModels();
            FillClinicModels();
            FillUserTypeModels();
            FillFunctionModels();
            FillFunctionalGroupModels();
            FillClinicUserModels();
            FillHospitalUserModels();
            FillClinicBotModels();
            FillHospitalBotModels();
            FillAdministratorModels();
            FillUserFunctionModels();
        }


        protected virtual void FillList<T>(IEnumerable<T> models, bool fillApprove = true)
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
        }

        protected virtual void FillSectionModels()
        {
            var models = _sectionModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateSections();

            FillList(models, fillApprove);
        }

        protected virtual void FillHospitalModels()
        {
            var models = _hospitalModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateHospitals();

            FillList(models, fillApprove);
        }

        protected virtual void FillClinicModels()
        {
            var models = _clinicModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateClinics();

            FillList(models, fillApprove);
        }

        protected virtual void FillFunctionModels()
        {
            var models = _functionModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateFunctions();

            FillList(models, fillApprove);
        }

        protected virtual void FillFunctionalGroupModels()
        {
            var models = _functionalGroupModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateFunctionalGroups();

            FillList(models, fillApprove);
        }

        protected virtual void FillUserTypeModels()
        {
            var models = _userTypeModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateUserTypes();

            FillList(models, fillApprove);
        }

        protected virtual void FillClinicUserModels()
        {
            var models = _clinicUserModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateClinicUsers();

            FillList(models, fillApprove);
        }

        protected virtual void FillHospitalUserModels()
        {
            var models = _hospitalUserModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateHospitalUsers();

            FillList(models, fillApprove);
        }

        protected virtual void FillClinicBotModels()
        {
            var models = _clinicBotModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateClinicBots();

            FillList(models, fillApprove);
        }

        protected virtual void FillHospitalBotModels()
        {
            var models = _hospitalBotModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateHospitalBots();

            FillList(models, fillApprove);
        }

        protected virtual void FillAdministratorModels()
        {
            var models = _administratorModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateAdministrators();

            FillList(models, fillApprove);
        }

        protected virtual void FillUserFunctionModels()
        {
            var models = _userFunctionModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateUserFunctions();

            FillList(models, fillApprove);
        }
    }
}
