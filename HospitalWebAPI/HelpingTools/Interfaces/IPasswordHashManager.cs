namespace HelpingTools.Interfaces
{
    public interface IPasswordHashManager
    {
        string GetPasswordHash(string password);

        bool IsCorrectPassword(string password, string passwordHash);
    }
}
