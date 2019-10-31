using Kredek.Data.Models;
using Kredek.Global;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kredek.Areas.CMS.Pages.WebsitePageManagement
{
    public class CreateModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        [BindProperty]
        public WebsitePage WebsitePage { get; set; }

        public CreateModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.WebsitePages.Add(WebsitePage);
            await _context.SaveChangesAsync();

            await CreateTranslationInDefaultLanguage(WebsitePage);

            return RedirectToPage("./Index");
        }

        private async Task CreateTranslationInDefaultLanguage(WebsitePage websitePage)
        {
            Language defaultLanguage = await _context.Languages.SingleAsync(x => x.ISOCode == DefaultVariables.DefaultLanguageIsoCode);

            WebsitePageTranslation defaultTranslation = new WebsitePageTranslation()
            {
                Language = defaultLanguage,
                LanguageId = defaultLanguage.LanguageId,
                NameInNavigationBar = websitePage.Name,
                Title = websitePage.Name,
                WebsitePage = websitePage,
                WebsitePageId = websitePage.WebsitePageId,
            };

            _context.WebsitePageTranslations.Add(defaultTranslation);

            await _context.SaveChangesAsync();
        }
    }
}