using Newtonsoft.Json;

namespace Kredek.Pages.TestFeatures.OurTeamJSONData
{
    /// <summary>
    /// Azure AD B2C user.
    /// Domain name (tentant): Kredek.onmicrosoft.com
    /// </summary>
    public class TeamMember
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("extension_3957cf9f3fca4987b3f148e04f4847b7_UserImageUrl")]
        public string UserImageUrl { get; set; }
    }
}