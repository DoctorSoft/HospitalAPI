using Enums.Enums;

namespace HandleToolsInterfaces.Converters
{
    public interface IFunctionaNameToMainMenuItemConverter
    {
        MainMenuItem Convert(FunctionIdentityName name);
    }
}
