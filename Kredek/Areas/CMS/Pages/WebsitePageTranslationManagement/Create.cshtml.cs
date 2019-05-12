using System.Threading.Tasks;
using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kredek.Areas.CMS.Pages.WebsitePageTranslationManagement
{
    public class CreateModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public CreateModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId");
        ViewData["WebsitePageId"] = new SelectList(_context.WebsitePages, "WebsitePageId", "WebsitePageId");
            return Page();
        }

        [BindProperty]
        public WebsitePageTranslation WebsitePageTranslation { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.WebsitePageTranslations.Add(WebsitePageTranslation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}