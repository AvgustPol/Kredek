using FacebookPageGetter.Models.FeedPostDto;
using System.Threading.Tasks;

namespace FacebookPageGetter.Services.FacebookService
{
    public interface IFacebookService : IService
    {
        Task<FeedPostsDto> GetPostsAsync(int count);
    }
}