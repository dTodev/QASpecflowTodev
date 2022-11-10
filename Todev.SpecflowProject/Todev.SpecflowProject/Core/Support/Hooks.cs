using Todev.SpecflowProject.Core.Config;
using Todev.SpecflowProject.Core.ContextContainers;
using TechTalk.SpecFlow.Infrastructure;

namespace Todev.SpecflowProject.Core.Support
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        private TestContextContainer _testContext;
        private BaseConfig _baseConfig;

        public Hooks(ISpecFlowOutputHelper specFlowOutputHelper, TestContextContainer testContext, BaseConfig baseConfig)
        {
            _specFlowOutputHelper = specFlowOutputHelper;
            _testContext = testContext;
            _baseConfig = baseConfig;

        }

        [BeforeScenario]
        public void TearUp()
        {
            _testContext.HttpClient = new HttpClient();
        }

        [BeforeScenario("Authenticate")]
        public void Authenticate()
        {
            _testContext.HttpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.Token);
        }

        [BeforeScenario("Authenticate2")]
        public void AuthenticateWithForeignToken()
        {
            _testContext.HttpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.ForeignToken);
        }
    }
}
