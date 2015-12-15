using Enums.Enums;
using HandleToolsInterfaces.Converters;

namespace HandleTools.Converters
{
    public class UserToAccountTypeConverter : IUserToAccountTypeConverter
    {
        public UserAccountType Convert(UserType userType)
        {
            return (UserAccountType) (int) userType;
        }

        public UserType Convert(UserAccountType userType)
        {
            return (UserType)(int)userType;
        }
    }
}
