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

            // Fillers

            Bind<IDataBaseInfoFiller>().To<DataBaseInfoFiller>();
        }
    }
}
