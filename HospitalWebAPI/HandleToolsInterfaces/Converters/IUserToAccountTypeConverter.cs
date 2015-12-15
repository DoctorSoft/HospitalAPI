using Enums.Enums;

namespace HandleToolsInterfaces.Converters
{
    public interface IUserToAccountTypeConverter
    {
        UserAccountType Convert(UserType userType);

        UserType Convert(UserAccountType userType);
    }
}
