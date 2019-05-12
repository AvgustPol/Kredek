using FacebookPageGetter.Models.FeedPostDto;
using System.Threading.Tasks;

namespace Kredek.Logic
{
    public interface IBloggingManager
    {
        Task<FeedPostsDto> GetPostsAsync();
    }
}