using System.Threading.Tasks;
using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Kredek.Areas.CMS.Pages.WebsitePageManagement
{
    public class DetailsModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public DetailsModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public WebsitePage WebsitePage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WebsitePage = await _context.WebsitePages.FirstOrDefaultAsync(m => m.WebsitePageId == id);

            if (WebsitePage == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
