using System;

namespace FacebookPageGetter.Models.FeedPostDto
{
    public class FeedPostDto
    {
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Id { get; set; }
        public string Picture { get; set; }
        public string SourceUrl { get; set; }
    }
}