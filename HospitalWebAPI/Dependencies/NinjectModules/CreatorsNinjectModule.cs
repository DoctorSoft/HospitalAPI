using CreateRandomDataTools.CreationSettings;
using CreateRandomDataTools.DataCreators;
using CreateRandomDataTools.Fillers;
using CreateRandomDataTools.Interfaces.CommonInterfaces;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using Ninject.Modules;

namespace Dependencies.NinjectModules
{
    public class CreatorsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // Settings

            Bind<ICreationSettingsModule>().To<TestCreationSettingsModule>().InThreadScope();

            // Creators

            Bind<IClinicModelCreator>().To<ClinicModelCreator>().InThreadScope();
            Bind<ISettingsItemCreator>().To<SettingsItemCreator>().InThreadScope();
            Bind<IFunctionalGroupModelCreator>().To<FunctionalGroupModelCreator>().InThreadScope();
            Bind<IFunctionModelCreator>().To<FunctionModelCreator>().InThreadScope();
            Bind<IHospitalModelCreator>().To<HospitalModelCreator>().InThreadScope();
            Bind<ISectionModelCreator>().To<SectionModelCreator>().InThreadScope();
            Bind<IUserTypeModelCreator>().To<UserTypeModelCreator>().InThreadScope();
            Bind<IClinicUserModelCreator>().To<ClinicUserModelCreator>().InThreadScope();
            Bind<IHospitalUserModelCreator>().To<HospitalUserModelCreator>().InThreadScope();
            Bind<IReceptionUserModelCreator>().To<ReceptionUserModelCreator>().InThreadScope();
            Bind<IBotModelCreator>().To<BotModelCreator>().InThreadScope();
            Bind<IAdministratorAndReviewerModelsCreator>().To<AdministratorAndReviewerModelsCreator>().InThreadScope();
            Bind<IUserFunctionModelCreator>().To<UserFunctionModelCreator>().InThreadScope();

            // Fillers

            Bind<IDataBaseInfoFiller>().To<DataBaseInfoFiller>().InThreadScope();
        }
    }
}
