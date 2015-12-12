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
    }
}
