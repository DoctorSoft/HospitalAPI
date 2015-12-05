using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace RemoteServicesTools.Interfaces
{
    public interface IAPIDataBrowser
    {
        T GetData<T>(string url);

        string GetData(string url);

        string PostData(string url, Dictionary<string, string> bodyParameters, Dictionary<string, string> uriParameters);

        string PostData(string uri, Dictionary<string, string> bodyParameters);

        T PostData<T>(string url, Dictionary<string, string> bodyParameters, Dictionary<string, string> uriParameters);

        T PostData<T>(string uri, Dictionary<string, string> bodyParameters);
    }
}
