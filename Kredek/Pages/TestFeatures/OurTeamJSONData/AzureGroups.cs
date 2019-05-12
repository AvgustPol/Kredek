using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kredek.Pages.TestFeatures.OurTeamJSONData
{
    /// <summary>
    /// Azure Active Directory Groups.
    /// Domain name (tentant): Kredek.onmicrosoft.com
    /// </summary>
    public class AzureGroups
    {
        [JsonProperty("value")]
        public IEnumerable<AzureGroup> Groups { get; set; }
    }
}