using CreateRandomDataTools.CreationSettings;
using CreateRandomDataTools.DataCreators;
using CreateRandomDataTools.Fillers;
using CreateRandomDataTools.Interfaces.CommonInterfaces;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Dependencies.NinjectModules
{
    public class CreatorsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // Settings

            Bind<ICreationSettingsModule>().To<TestCreationSettingsModule>().InRequestScope();

            // Creators

            Bind<IClinicModelCreator>().To<ClinicModelCreator>().InRequestScope();
            Bind<IFunctionalGroupModelCreator>().To<FunctionalGroupModelCreator>().InRequestScope();
            Bind<IFunctionModelCreator>().To<FunctionModelCreator>().InRequestScope();
            Bind<IHospitalModelCreator>().To<HospitalModelCreator>().InRequestScope();
            Bind<ISectionModelCreator>().To<SectionModelCreator>().InRequestScope();
            Bind<IUserTypeModelCreator>().To<UserTypeModelCreator>().InRequestScope();
            Bind<IClinicUserModelCreator>().To<ClinicUserModelCreator>().InRequestScope();
            Bind<IHospitalUserModelCreator>().To<HospitalUserModelCreator>().InRequestScope();
            Bind<IBotModelCreator>().To<BotModelCreator>().InRequestScope();
            Bind<IAdministratorModelCreator>().To<AdministratorModelCreator>().InRequestScope();
            Bind<IUserFunctionModelCreator>().To<UserFunctionModelCreator>().InRequestScope();

            // Fillers

            Bind<IDataBaseInfoFiller>().To<DataBaseInfoFiller>().InRequestScope();
        }
    }
}
