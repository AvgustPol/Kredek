using FacebookPageGetter.Models.FeedPostDto;
using FacebookPageGetter.Services.FacebookService;
using System.Threading.Tasks;

namespace Kredek.Logic
{
    public class BloggingManager : IBloggingManager
    {
        private const int NumberOfPostsToDownload = 5;

        private readonly IFacebookService _facebookService;

        public BloggingManager(IFacebookService facebookService)
        {
            _facebookService = facebookService;
        }

        public async Task<FeedPostsDto> GetPostsAsync()
        {
            return await _facebookService.GetPostsAsync(NumberOfPostsToDownload);
        }
    }
}