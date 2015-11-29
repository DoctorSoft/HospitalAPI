using System.Runtime.InteropServices.ComTypes;

namespace RemoteServicesTools.Interfaces
{
    public interface IAPIDataBrowser
    {
        T GetData<T>(string url);

        string GetData(string url);
    }
}
