using System.Collections.Generic;
using System.Threading.Tasks;
using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Kredek.Areas.CMS.Pages.WebsitePageTranslationManagement
{
    public class IndexModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public IndexModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<WebsitePageTranslation> WebsitePageTranslation { get;set; }

        public async Task OnGetAsync()
        {
            WebsitePageTranslation = await _context.WebsitePageTranslations
                .Include(w => w.Language)
                .Include(w => w.WebsitePage).ToListAsync();
        }
    }
}
