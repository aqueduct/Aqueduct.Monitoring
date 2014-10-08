using System.Text;
using System.Net;
using Common.Logging;

namespace Aqueduct.ServerDensity
{
    public class RequestClient : IRequestClient
    {
        private readonly NetworkCredential _creadentials;
        readonly static ILog Logger = LogManager.GetLogger<ServerDensityApi>();

        public RequestClient(NetworkCredential creadentials)
        {
            _creadentials = creadentials;            
        }

        private WebClient Create()
        {
            WebClient client = new WebClient() { Credentials = _creadentials };
            client.Headers["User-Agent"] = "Aqueduct stat uploader";
            return client;
        }

        public string Get(string url)
        {
            Logger.Debug("Sending Request to url: " + url);

            using (WebClient client = Create())
            {
                return client.DownloadString(url);
            }
        }

        public string Post(string url, string postBody)
        {
            Logger.Debug("Sending Request to url: " + url);
            using (WebClient client = Create())
            {
                client.Headers["Content-type"] = "application/x-www-form-urlencoded";

                var postValue = Encoding.UTF8.GetBytes(postBody);
                byte[] result = new byte[0];
                result = client.UploadData(url, "POST", postValue);

                return Encoding.UTF8.GetString(result);
            }
        }
    }
}
