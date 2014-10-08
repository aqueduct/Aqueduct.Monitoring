using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Collections.Specialized;
using Common.Logging;

namespace Aqueduct.ServerDensity
{
    public class ServerDensityApi : IServerDensityApi
    {
        private static readonly ILog Logger = LogManager.GetLogger<ServerDensityApi>();

        private const string UrlTemplate = "https://{0}/{1}/{2}/{3}?account={4}&apiKey={5}{6}";
        private string _apiKey;
        private NetworkCredential _credentials;
        private string _version;
        private string _account;
        private string _apiUrl;

        private IRequestClient RequestClient { get; set; }

        public static IServerDensityApi Initialise()
        {
            return Initialise(ServerDensitySettings.GetFromAppConfig());
        }

        public static IServerDensityApi Initialise(ServerDensitySettings settings)
        {
            return Initialise(settings, null);
        }

        internal static IServerDensityApi Initialise(ServerDensitySettings settings, IRequestClient requestClient)
        {
            if (string.IsNullOrEmpty(settings.ApiKey))
                throw new ArgumentException("Api key missing. Api key is required to initialise API");

            if (settings.Credentials == null || string.IsNullOrEmpty(settings.Credentials.UserName) || string.IsNullOrEmpty(settings.Credentials.Password))
                throw new ArgumentException("Credetials are required for the api calls to function correctly.");

            if (string.IsNullOrEmpty(settings.Account))
                throw new ArgumentException("Account missing. Account is required to initialise API. It will be in the form of [name].serverdensity.com");

            return new ServerDensityApi(settings) { RequestClient = requestClient ?? new RequestClient(settings.Credentials) };
        }

        internal string CallUrl(string module, string method)
        {
            Logger.Debug(String.Format("Calling url for module: {0} and method: {1}", module, method));
            return RequestClient.Get(string.Format(UrlTemplate, _apiUrl, _version, module, method, _account, _apiKey, ""));
        }

        public string PostTo(string module, string method, NameValueCollection extraQueryParams, NameValueCollection postData)
        {
            Logger.Debug(String.Format("Posting to module: {0} and method: {1}, with params: [{2}] and postData: [{3}]", module, method, extraQueryParams.ToQueryString(), postData.ToQueryString()));
            return RequestClient.Post(GetPostUrl(module, method, extraQueryParams.ToQueryString()), postData.ToQueryString());
        }

        private string GetPostUrl(string module, string method, string extraQueryString)
        {
            return string.Format(UrlTemplate, 
                       _apiUrl, 
                       _version, 
                       module, 
                       method, 
                       _account, 
                       _apiKey, 
                       string.IsNullOrWhiteSpace(extraQueryString) ? "" : "&" + extraQueryString);
        }
        public string Version { get { return _version; } }

        private ServerDensityApi(ServerDensitySettings settings)
        {
            _apiKey = settings.ApiKey;
            _credentials = settings.Credentials;
            _account = settings.Account;
            _apiUrl = settings.ApiUrl;
            _version = settings.Version;

            Alerts = new AlertsApi(this);
            Metrics = new MetricsApi(this);
        }

        public IAlertsApi Alerts { get; private set; }
        public IMetricsApi Metrics { get; private set; }
    }
}
