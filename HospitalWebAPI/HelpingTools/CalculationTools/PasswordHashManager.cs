using HelpingTools.ExtentionTools;
using HelpingTools.Interfaces;

namespace HelpingTools.CalculationTools
{
    public class PasswordHashManager : IPasswordHashManager
    {
        private readonly Md5Encryptor _md5Encryptor;

        public PasswordHashManager()
        {
            this._md5Encryptor = new Md5Encryptor();
        }

        public string GetPasswordHash(string password)
        {
            return _md5Encryptor.GetMd5Hash(password);
        }

        public bool IsCorrectPassword(string password, string passwordHash)
        {
            return passwordHash == GetPasswordHash(password);
        }
    }
}
