using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.CommonInterfaces;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using DataBaseFiller.Interfaces;
using DataBaseTools.Interfaces;
using StorageModels.Interfaces;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.FunctionModels;
using StorageModels.Models.HospitalModels;

namespace DataBaseFiller.Tools
{
    public class DataBaseInfoFiller : IDataBaseInfoFiller
    {
        private readonly IClinicModelCreator _clinicModelCreator;
        private readonly IDistributiveGroupModelCreator _distributiveGroupModelCreator;
        private readonly IFunctionModelCreator _functionModelCreator;
        private readonly IHospitalModelCreator _hospitalModelCreator;
        private readonly ISectionModelCreator _sectionModelCreator;

        private readonly IDataBaseContext _dataBaseContext;
        private readonly ICreationSettingsModule _creationSettingsModule;

        public DataBaseInfoFiller(IClinicModelCreator clinicModelCreator,
            IDistributiveGroupModelCreator distributiveGroupModelCreator, IFunctionModelCreator functionModelCreator,
            IHospitalModelCreator hospitalModelCreator, ISectionModelCreator sectionModelCreator, IDataBaseContext dataBaseContext, ICreationSettingsModule creationSettingsModule)
        {
            _clinicModelCreator = clinicModelCreator;
            _distributiveGroupModelCreator = distributiveGroupModelCreator;
            _functionModelCreator = functionModelCreator;
            _hospitalModelCreator = hospitalModelCreator;
            _sectionModelCreator = sectionModelCreator;

            _dataBaseContext = dataBaseContext;
            _creationSettingsModule = creationSettingsModule;
        }

        public void FillDataBase()
        {
            FillSectionModels();
            FillHospitalModels();
            FillClinicModels();
            FillFunctionModels();
            FillDistributiveGroupModels();
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

        protected virtual void FillDistributiveGroupModels()
        {
            var models = _distributiveGroupModelCreator.GetList();
            var fillApprove = _creationSettingsModule.CreateDistributiveGroups();

            FillList(models, fillApprove);
        }
    }
}
