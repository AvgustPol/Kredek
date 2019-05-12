using AutoMapper;
using FacebookPageGetter.Models;
using FacebookPageGetter.Models.FeedPost;
using FacebookPageGetter.Models.FeedPostDto;
using FacebookPageGetter.Services.FacebookClient;
using System.Threading.Tasks;

namespace FacebookPageGetter.Services.FacebookService
{
    public class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;
        private readonly FacebookSettings _facebookSettings;
        private readonly IMapper _mapper;

        public FacebookService(FacebookSettings facebookSettings, IFacebookClient facebookClient, IMapper mapper)
        {
            _facebookSettings = facebookSettings;
            _facebookClient = facebookClient;
            _mapper = mapper;
        }

        public async Task<FeedPostsDto> GetPostsAsync(int count)
        {
            if (count > 100)
            {
                count = 100;
            }

            var posts = await _facebookClient.GetAsync<FeedPosts>(_facebookSettings.AccessToken, "KNKredek/posts",
                $"fields=full_picture,permalink_url,created_time,message&limit={count}").ConfigureAwait(false);

            if (posts == null)
            {
                return null;
            }

            var result = _mapper.Map<FeedPostsDto>(posts);

            return result;
        }
    }
}