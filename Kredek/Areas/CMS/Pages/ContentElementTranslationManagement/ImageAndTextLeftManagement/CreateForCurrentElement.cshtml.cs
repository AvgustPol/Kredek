using Kredek.Data.Models.ContentElementTranslationTemplates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Kredek.Areas.CMS.Pages.ContentElementTranslationManagement.ImageAndTextLeftManagement
{
    public class CreateModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        [BindProperty]
        public int ContentElementId { get; set; }

        [BindProperty]
        public ImageAndTextLeft ImageAndTextLeft { get; set; }

        public CreateModel(Kredek.Data.ApplicationDbContext context)
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

            ImageAndTextLeft.ContentElementId = id;

            _context.TemplatesImageAndTextLeft.Add(ImageAndTextLeft);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}