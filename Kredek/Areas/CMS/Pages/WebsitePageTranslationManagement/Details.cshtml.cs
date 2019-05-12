using System.Threading.Tasks;
using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Kredek.Areas.CMS.Pages.WebsitePageTranslationManagement
{
    public class DetailsModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public DetailsModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public WebsitePageTranslation WebsitePageTranslation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WebsitePageTranslation = await _context.WebsitePageTranslations
                .Include(w => w.Language)
                .Include(w => w.WebsitePage).FirstOrDefaultAsync(m => m.WebsitePageTranslationId == id);

            if (WebsitePageTranslation == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
