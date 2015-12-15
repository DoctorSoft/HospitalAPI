using Enums.Enums;
using HandleToolsInterfaces.Converters;

namespace HandleTools.Converters
{
    public class FunctionaNameToMainMenuItemConverter : IFunctionaNameToMainMenuItemConverter
    {
        public MainMenuItem Convert(FunctionIdentityName name)
        {
            return (MainMenuItem) (int) name;
        }
    }
}
