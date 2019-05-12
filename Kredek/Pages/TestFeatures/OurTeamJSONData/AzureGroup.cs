using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kredek.Pages.TestFeatures.OurTeamJSONData
{
    /// <summary>
    /// Azure Active Directory Group.
    /// Domain name (tentant): Kredek.onmicrosoft.com
    /// </summary>
    public class AzureGroup
    {
        //active
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("members")]
        public IEnumerable<TeamMember> GroupMembers { get; set; }
    }
}