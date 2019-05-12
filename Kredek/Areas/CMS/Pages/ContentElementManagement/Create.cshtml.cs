using System.Threading.Tasks;
using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kredek.Areas.CMS.Pages.ContentElementManagement
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
        ViewData["WebsitePageId"] = new SelectList(_context.WebsitePages, "WebsitePageId", "WebsitePageId");
            return Page();
        }

        [BindProperty]
        public ContentElement ContentElement { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ContentElement.Add(ContentElement);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}