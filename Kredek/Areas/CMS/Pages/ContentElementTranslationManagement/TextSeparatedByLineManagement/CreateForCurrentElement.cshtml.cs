using Kredek.Data.Models.ContentElementTranslationTemplates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Kredek.Areas.CMS.Pages.ContentElementTranslationManagement.TextSeparatedByLineManagement
{
    public class CreateForCurrentElementModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public int ContentElementId { get; set; } = 1;

        [BindProperty]
        public TextSeparatedByLine TextSeparatedByLine { get; set; }

        public CreateForCurrentElementModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            ContentElementId = id;

            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            TextSeparatedByLine.ContentElementId = id;

            _context.TemplatesTextSeparatedByLine.Add(TextSeparatedByLine);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}