using Newtonsoft.Json;

namespace Todev.SpecflowProject.Core.Support
{
    public class GoRESTUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class GoRESTRequestUser
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
