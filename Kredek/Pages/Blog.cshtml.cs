using FacebookPageGetter.Models.FeedPostDto;
using FacebookPageGetter.Services.FacebookService;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Kredek.Pages
{
    public class BlogModel : PageModel
    {
        private const int NumberOfPostsToDownload = 5;

        private readonly IFacebookService _facebookService;

        public FeedPostsDto FeedPostsDto { get; set; }

        public BlogModel(IFacebookService facebookService)
        {
            _facebookService = facebookService;
        }

        public async Task OnGetAsync()
        {
            FeedPostsDto = await _facebookService.GetPostsAsync(NumberOfPostsToDownload);
        }
    }
}