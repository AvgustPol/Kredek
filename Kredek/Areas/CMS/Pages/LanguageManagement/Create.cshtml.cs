using System.Threading.Tasks;
using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kredek.Areas.CMS.Pages.LanguageManagement
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
        public Language Language { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Languages.Add(Language);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}