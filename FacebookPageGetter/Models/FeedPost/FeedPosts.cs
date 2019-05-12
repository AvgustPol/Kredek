using Newtonsoft.Json;
using System.Collections.Generic;

namespace FacebookPageGetter.Models.FeedPost
{
    public class FeedPosts
    {
        [JsonProperty("data")]
        public IEnumerable<FeedPost> Posts { get; set; }
    }
}