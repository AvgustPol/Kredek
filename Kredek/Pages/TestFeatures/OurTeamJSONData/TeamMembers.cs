using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kredek.Pages.TestFeatures.OurTeamJSONData
{
    /// <summary>
    /// Azure AD B2C users.
    /// Domain name (tentant): Kredek.onmicrosoft.com
    /// </summary>
    public class TeamMembers
    {
        [JsonProperty("value")]
        public IEnumerable<TeamMember> Members { get; set; }
    }
}