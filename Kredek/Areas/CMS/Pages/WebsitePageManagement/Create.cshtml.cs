using System.Threading.Tasks;
using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kredek.Areas.CMS.Pages.WebsitePageManagement
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
            return Page();
        }

        [BindProperty]
        public WebsitePage WebsitePage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.WebsitePages.Add(WebsitePage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}