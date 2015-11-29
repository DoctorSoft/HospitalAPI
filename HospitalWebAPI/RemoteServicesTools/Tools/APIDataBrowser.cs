using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Script.Serialization;
using RemoteServicesTools.Interfaces;

namespace RemoteServicesTools.Tools
{
    public class APIDataBrowser : IAPIDataBrowser
    {
        public T GetData<T>(string url)
        {
            var jsonData = GetData(url);
            var serializer = new JavaScriptSerializer();

            var result = serializer.DeserializeObject(jsonData);
            return (T)result;
        }

        public string GetData(string url)
        {
            var browser = new HttpClient();
            var data = browser.GetAsync(url).Result.Content;
            return data.ReadAsStringAsync().Result;
        }
    }
}
