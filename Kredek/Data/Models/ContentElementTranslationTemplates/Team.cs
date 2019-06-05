using System.Collections.Generic;

namespace Kredek.Data.Models.ContentElementTranslationTemplates
{
    public class Team
    {
        public string Name { get; set; }

        //public string Description { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
    }
}