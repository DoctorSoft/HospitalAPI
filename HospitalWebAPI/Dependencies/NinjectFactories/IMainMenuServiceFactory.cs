using Services.Interfaces.MainMenuServices;
using Services.Interfaces.SessionServices;

namespace Dependencies.NinjectFactories
{
    public interface IMainMenuServiceFactory
    {
        IMainMenuService CreateMainMenuService();
    }
}
