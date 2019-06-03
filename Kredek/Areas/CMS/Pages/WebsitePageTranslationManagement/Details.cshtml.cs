using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kredek.Areas.CMS.Pages.WebsitePageTranslationManagement
{
    public class DetailsModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public WebsitePageTranslation WebsitePageTranslation { get; set; }

        public DetailsModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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