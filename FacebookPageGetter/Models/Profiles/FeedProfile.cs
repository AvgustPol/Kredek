using AutoMapper;
using FacebookPageGetter.Models.FeedPost;
using FacebookPageGetter.Models.FeedPostDto;

namespace FacebookPageGetter.Models.Profiles
{
    public class FeedProfile : Profile
    {
        public FeedProfile()
        {
            CreateMap<FeedPosts, FeedPostsDto>();
            //CreateMap<FeedPostsDto, FeedPosts>();

            CreateMap<FeedPostDto.FeedPostDto, FeedPost.FeedPost>();
            //CreateMap<FeedPost.FeedPost, FeedPostDto.FeedPostDto>();
        }
    }
}