using Services.Interfaces.MainMenuServices;

namespace Dependencies.NinjectFactories
{
    public interface IMainMenuServiceFactory
    {
        IMainMenuService CreateMainMenuService();
    }
}
