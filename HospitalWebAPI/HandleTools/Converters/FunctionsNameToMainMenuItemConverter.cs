using Enums.Enums;
using HandleToolsInterfaces.Converters;

namespace HandleTools.Converters
{
    public class FunctionsNameToMainMenuItemConverter : IFunctionsNameToMainMenuItemConverter
    {
        public MainMenuItem Convert(FunctionIdentityName name)
        {
            return (MainMenuItem) (int) name;
        }
    }
}
