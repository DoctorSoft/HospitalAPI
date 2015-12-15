using Enums.Enums;

namespace HandleToolsInterfaces.Converters
{
    public interface IFunctionsNameToMainMenuItemConverter
    {
        MainMenuItem Convert(FunctionIdentityName name);
    }
}
