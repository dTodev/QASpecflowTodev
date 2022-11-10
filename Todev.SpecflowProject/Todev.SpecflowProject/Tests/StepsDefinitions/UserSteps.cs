using FluentAssertions.Execution;
using Todev.SpecflowProject.Core.Config;
using Todev.SpecflowProject.Core.ContextContainers;
using Todev.SpecflowProject.Core.Support;
using Newtonsoft.Json;
using TechTalk.SpecFlow.Assist;

namespace Todev.SpecflowProject.Tests.StepsDefinitions
{
    [Binding]
    public class UserSteps
    {
        private readonly BaseConfig _baseConfig;
        private HttpResponseMessage _response;
        private GoRESTRequestUser _user;
        private GoRESTUser _userWithId;
        private TestContextContainer _context;

        public UserSteps(BaseConfig baseConfig, TestContextContainer context)
        {
            _baseConfig = baseConfig;
            _context = context;
        }

        [Given(@"I want to prepare a request")]
        public void GivenIWantToPrepareARequest()
        {
        }

        [When(@"I get all users from the (.*) endpoint")]
        public void WhenIGetAllUsersFromTheUsersEndpoint(string endpoint)
        {
            _response = _context.HttpClient.GetAsync($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}").Result;
        }

        [Then(@"The response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBeOK(string statusCode)
        {
            _response.StatusCode.ToString().Should().Be(statusCode);
        }

        [Then(@"The response should contain a list of users")]
        public void ThenTheResponseShouldContainAListOfUsers()
        {
            var content = _response.Content.ReadAsStringAsync().Result;
            var expectedResponse = JsonConvert.DeserializeObject<List<GoRESTUser>>(content);
            expectedResponse.Should().NotBeEmpty();
        }

        [Given(@"I have the following user data")]
        public void GivenIHaveTheFollowingUserData(Table table)
        {
            _user = table.CreateInstance<GoRESTRequestUser>();
        }

        [When(@"I send a request to the (.*) endpoint")]
        public void WhenISendARequestToTheUsersEndpoint(string endpoint)
        {
            var requestContent = JsonConvert.SerializeObject(_user);
            var requestBody = new StringContent(requestContent, System.Text.Encoding.UTF8, "application/json");

            var msgBody = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}"),
                Content = requestBody
            };

            _response = _context.HttpClient.SendAsync(msgBody).Result;
        }

        [Then(@"the user should be created successfully")]
        public void ThenTheUserShouldBeCreatedSuccessfully()
        {
            var actualResponse = JsonConvert.DeserializeObject<GoRESTUser>(_response.Content.ReadAsStringAsync().Result);

            using (new AssertionScope())
            {
                actualResponse.Id.Should().NotBe(null);
                actualResponse.Name.Should().Be(_user.Name);
            }
        }

        [Given(@"I have a created user in the (.*) endpoint already")]
        public void GivenIHaveACreatedUserInTheUsersEndpointAlready(string endpoint, Table table)
        {
            _userWithId = table.CreateInstance<GoRESTUser>();

            _response = _context.HttpClient.GetAsync($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}/{_userWithId.Id}").Result;
        }

        [When(@"I send an update request to the (.*) endpoint")]
        public void WhenISendAnUpdateRequestToTheUsersEndpoint(string endpoint)
        {
            var requestContent = JsonConvert.SerializeObject(_userWithId);

            var content = new StringContent(requestContent, System.Text.Encoding.UTF8, "application/json");

            var requestBody2 = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}/{_userWithId.Id}"),
                Content = content
            };

            _response = _context.HttpClient.SendAsync(requestBody2).Result;
        }

        [Then(@"The user should be updated successfully")]
        public void ThenTheUserShouldBeUpdatedSuccessfully()
        {
            var actualResponse = JsonConvert.DeserializeObject<GoRESTUser>(_response.Content.ReadAsStringAsync().Result);

            actualResponse.Id.Should().Be(_userWithId.Id);
            actualResponse.Name.Should().Be(_userWithId.Name);
            actualResponse.Email.Should().Be(_userWithId.Email);
            actualResponse.Gender.Should().Be(_userWithId.Gender);
            actualResponse.Status.Should().Be(_userWithId.Status);
        }

        [When(@"I send a delete request to the (.*) endpoint")]
        public void WhenISendADeleteRequestToTheUsersEndpoint(string endpoint)
        {
            var requestBody2 = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}/{_userWithId.Id}"),
            };

            _response = _context.HttpClient.SendAsync(requestBody2).Result;
        }

        [Then(@"the user should be deleted successfully")]
        public void ThenTheUserShouldBeDeletedSuccessfully()
        {
            var actualResponse = JsonConvert.DeserializeObject<GoRESTUser>(_response.Content.ReadAsStringAsync().Result);

            actualResponse.Should().Be(null);
        }
    }
}