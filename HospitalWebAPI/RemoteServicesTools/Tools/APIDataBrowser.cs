using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        protected virtual string GetUriPartFromParameters(Dictionary<string, string> uriParameters)
        {
            var parameters = uriParameters.Keys.Select(s => string.Format("{0}={1}", s, uriParameters[s])).ToList();
            var result = string.Join("&", parameters);
            return result;
        }

        public string PostData(string url, Dictionary<string, string> bodyParameters, Dictionary<string, string> uriParameters)
        {
            if (uriParameters == null || !uriParameters.Any())
            {
                return PostData(url, bodyParameters);
            }

            return PostData(string.Format("{0}?{1}", url, GetUriPartFromParameters(uriParameters)), bodyParameters);
        }

        public string PostData(string uri, Dictionary<string, string> bodyParameters)
        {
            var browser = new HttpClient();

            var contentParameters = bodyParameters.Keys.Select(s => new KeyValuePair<string, string>(s, bodyParameters[s])).ToArray();
            var content = new FormUrlEncodedContent(contentParameters);

            var result = browser.PostAsync(uri, content).Result;
            var resultContent = result.Content.ReadAsStringAsync().Result;

            return resultContent;
        }

        public T PostData<T>(string url, Dictionary<string, string> bodyParameters, Dictionary<string, string> uriParameters)
        {
            var jsonData = PostData(url, bodyParameters, uriParameters);
            var serializer = new JavaScriptSerializer();

            var result = serializer.DeserializeObject(jsonData);
            return (T)result;
        }

        public T PostData<T>(string uri, Dictionary<string, string> bodyParameters)
        {
            var jsonData = PostData(uri, bodyParameters);
            var serializer = new JavaScriptSerializer();

            var result = serializer.DeserializeObject(jsonData);
            return (T)result;
        }
    }
}
