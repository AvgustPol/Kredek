using System.Threading.Tasks;
using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Kredek.Areas.CMS.Pages.LanguageManagement
{
    public class DetailsModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public DetailsModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Language Language { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Language = await _context.Languages.FirstOrDefaultAsync(m => m.LanguageId == id);

            if (Language == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
