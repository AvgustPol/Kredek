using System.Collections.Generic;

namespace FacebookPageGetter.Models.FeedPostDto
{
    public class FeedPostsDto
    {
        public IEnumerable<FeedPostDto> Posts { get; set; }
    }
}