using B2CGraph;
using Kredek.Pages.TestFeatures.OurTeamJSONData;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Kredek.Pages.TestFeatures
{
    public class OurTeamModel : PageModel
    {
        private readonly IB2CGraphClient _client;

        public List<TeamMember> OurTeam { get; set; }

        public OurTeamModel(IB2CGraphClient client)
        {
            _client = client;
        }

        public void OnGet()
        {
            //GetAllGroups();

            //GetAllMembers();
        }

        public string SetTestRequest(string api, string query = null)
        {
            return _client.SendGraphGetRequest(api, query).Result;
        }

        //private void GetAllGroups()
        //{
        //    string result = _client.GetAllGroups(null).Result;
        //}

        private void GetAllMembers()
        {
            string result = _client.GetAllUsers(null).Result;

            OurTeam = JsonConvert.DeserializeObject<TeamMembers>(result).Members.ToList();
        }
    }
}