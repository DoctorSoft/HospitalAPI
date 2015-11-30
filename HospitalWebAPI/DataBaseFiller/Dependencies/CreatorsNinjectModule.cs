using CreateRandomDataTools.CreationSettings;
using CreateRandomDataTools.DataCreators;
using CreateRandomDataTools.Interfaces.CommonInterfaces;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using DataBaseFiller.Interfaces;
using DataBaseFiller.Tools;
using Ninject.Modules;

namespace DataBaseFiller.Dependencies
{
    public class CreatorsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // Settings

            Bind<ICreationSettingsModule>().To<TestCreationSettingsModule>();

            // Creators

            Bind<IClinicModelCreator>().To<ClinicModelCreator>();
            Bind<IFunctionalGroupModelCreator>().To<FunctionalGroupModelCreator>();
            Bind<IFunctionModelCreator>().To<FunctionModelCreator>();
            Bind<IHospitalModelCreator>().To<HospitalModelCreator>();
            Bind<ISectionModelCreator>().To<SectionModelCreator>();
            Bind<IUserTypeModelCreator>().To<UserTypeModelCreator>();
            Bind<IClinicUserModelCreator>().To<ClinicUserModelCreator>();
            Bind<IHospitalUserModelCreator>().To<HospitalUserModelCreator>();
            Bind<IClinicBotModelCreator>().To<ClinicBotModelCreator>();
            Bind<IHospitalBotModelCreator>().To<HospitalBotModelCreator>();
            Bind<IAdministratorModelCreator>().To<AdministratorModelCreator>();
            Bind<IUserFunctionModelCreator>().To<UserFunctionModelCreator>();

            // Fillers

            Bind<IDataBaseInfoFiller>().To<DataBaseInfoFiller>();
        }
    }
}
