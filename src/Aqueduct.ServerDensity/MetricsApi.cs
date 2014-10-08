using Common.Logging;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace Aqueduct.ServerDensity
{
    public sealed class MetricsApi : IMetricsApi
    {
        readonly static ILog Logger = LogManager.GetLogger<ServerDensityApi>();

        private const string ModuleName = "metrics";
        private readonly ServerDensityApi _ApiBase;
        
        public MetricsApi(ServerDensityApi apiBase)
        {
            _ApiBase = apiBase;
        }

        public string UploadPluginData(string deviceId, MetricsPayload payload)
        {
            Logger.Debug("Uploading plugin data for device " + deviceId);

            var postData = new NameValueCollection();
            postData["payload"] = JsonConvert.SerializeObject(payload);

            var extraParams = new NameValueCollection();
            extraParams["deviceId"] = deviceId;

            return _ApiBase.PostTo(ModuleName, "postback", extraParams, postData);
        }
    }
}
