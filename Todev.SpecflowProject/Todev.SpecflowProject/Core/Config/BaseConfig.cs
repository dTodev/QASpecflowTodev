using Microsoft.Extensions.Configuration;

namespace Todev.SpecflowProject.Core.Config
{
    public class BaseConfig
    {
        public HttpClientConfig HttpClientConfig { get; set; }

        public BaseConfig()
        {
            var config = new ConfigurationBuilder().AddJsonFile("specflowConfig.json").Build();

            HttpClientConfig = config.GetSection("HttpClient").Get<HttpClientConfig>();
        }
    }
}
