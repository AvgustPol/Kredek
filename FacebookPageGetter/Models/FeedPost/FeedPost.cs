using Newtonsoft.Json;
using System;

namespace FacebookPageGetter.Models.FeedPost
{
    /// <summary>
    /// FacebookService . GetPostsAsync in args param should has all these JSONProperties
    /// </summary>
    public class FeedPost
    {
        [JsonProperty("message")]
        public string Content { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("full_picture")]
        public string Picture { get; set; }

        [JsonProperty("permalink_url")]
        public string SourceUrl { get; set; }

        //[JsonProperty("updated_time")]
        //public DateTime UpdatedTime { get; set; }
    }
}