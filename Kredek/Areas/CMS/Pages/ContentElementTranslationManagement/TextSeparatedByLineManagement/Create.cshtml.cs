using Kredek.Data.Models.ContentElementTranslationTemplates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Kredek.Areas.CMS.Pages.ContentElementTranslationManagement.TextSeparatedByLineManagement
{
    public class CreateModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        [BindProperty]
        public TextSeparatedByLine TextSeparatedByLine { get; set; }

        public CreateModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ContentElementId"] = new SelectList(_context.ContentElement, "ContentElementId", "ContentElementId");
            //ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId");
            ViewData["LanguageName"] = new SelectList(_context.Languages, "LanguageId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TemplatesTextSeparatedByLine.Add(TextSeparatedByLine);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}