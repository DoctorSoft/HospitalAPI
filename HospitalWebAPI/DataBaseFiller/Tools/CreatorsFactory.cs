using CreateRandomDataTools.Interfaces.CommonInterfaces;
using Dependencies.NinjectKernels;
using Ninject;

namespace DataBaseFiller.Tools
{
    public class CreatorsFactory 
    {
        public IDataBaseInfoFiller GetFiller()
        {
            var kernel = new DataBaseFillerKernel();

            return kernel.Get<IDataBaseInfoFiller>();
        }
    }
}
