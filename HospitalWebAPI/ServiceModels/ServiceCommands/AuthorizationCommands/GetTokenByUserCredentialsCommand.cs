namespace ServiceModels.ServiceCommands.AuthorizationCommands
{
    public class GetTokenByUserCredentialsCommand
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
