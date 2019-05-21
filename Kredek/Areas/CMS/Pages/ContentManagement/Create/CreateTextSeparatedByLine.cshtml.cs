using Kredek.Data;
using Kredek.Data.Models;
using Kredek.Data.Models.ContentElementTranslationTemplates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Kredek.Areas.CMS.Pages.ContentManagement.Create
{
    public class CreateTextSeparatedByLine : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public ContentElement ContentElement { get; set; }

        [BindProperty]
        public TextSeparatedByLine TextSeparatedByLine { get; set; }

        [BindProperty]
        public int WebsitePageId { get; set; }

        public CreateTextSeparatedByLine(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            WebsitePageId = id;

            ViewData["Languages"] = new SelectList(_context.Languages, "LanguageId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var page = await _context.WebsitePages.FindAsync(id);

            #region Create a new ContentElement

            ContentElement.WebsitePageId = id;
            ContentElement.WebsitePage = page;
            _context.ContentElement.Add(ContentElement);

            #endregion Create a new ContentElement

            TextSeparatedByLine.ContentElement = ContentElement;

            _context.TemplatesTextSeparatedByLine.Add(TextSeparatedByLine);
            await _context.SaveChangesAsync();

            return RedirectToPage("/ContentManagement/Index", new { pageName = page.Name });
        }
    }
}