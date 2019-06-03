using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kredek.Areas.CMS.Pages.WebsitePageTranslationManagement
{
    public class DeleteModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        [BindProperty]
        public WebsitePageTranslation WebsitePageTranslation { get; set; }

        public DeleteModel(Kredek.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WebsitePageTranslation = await _context.WebsitePageTranslations.FindAsync(id);

            if (WebsitePageTranslation != null)
            {
                _context.WebsitePageTranslations.Remove(WebsitePageTranslation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}