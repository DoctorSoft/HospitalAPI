using CreateRandomDataTools.Interfaces.CommonInterfaces;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using DataBaseFiller.Interfaces;
using DataBaseTools.Interfaces;
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

        protected virtual void FillSectionModels()
        {
            if (_creationSettingsModule.CreateSections())
            {
                var fillList = _sectionModelCreator.GetList();
                _dataBaseContext.Set<SectionStorageModel>().AddRange(fillList);
                _dataBaseContext.SaveChanges();
            }
        }

        protected virtual void FillHospitalModels()
        {
            if (_creationSettingsModule.CreateHospitals())
            {
                var fillList = _hospitalModelCreator.GetList();
                _dataBaseContext.Set<HospitalStorageModel>().AddRange(fillList);
                _dataBaseContext.SaveChanges();
            }
        }

        protected virtual void FillClinicModels()
        {
            if (_creationSettingsModule.CreateClinics())
            {
                var fillList = _clinicModelCreator.GetList();
                _dataBaseContext.Set<ClinicStorageModel>().AddRange(fillList);
                _dataBaseContext.SaveChanges();
            }
        }

        protected virtual void FillFunctionModels()
        {
            if (_creationSettingsModule.CreateFunctions())
            {
                var fillList = _functionModelCreator.GetList();
                _dataBaseContext.Set<FunctionStorageModel>().AddRange(fillList);
                _dataBaseContext.SaveChanges();
            }
        }

        protected virtual void FillDistributiveGroupModels()
        {
            if (_creationSettingsModule.CreateDistributiveGroups())
            {
                var fillList = _distributiveGroupModelCreator.GetList();
                _dataBaseContext.Set<DistributiveGroupStorageModel>().AddRange(fillList);
                _dataBaseContext.SaveChanges();
            }
        }
    }
}
