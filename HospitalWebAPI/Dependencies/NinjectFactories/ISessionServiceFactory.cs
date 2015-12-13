using Services.Interfaces.SessionServices;

namespace Dependencies.NinjectFactories
{
    public interface ISessionServiceFactory
    {
        ISessionService CreateSessionService();
    }
}
