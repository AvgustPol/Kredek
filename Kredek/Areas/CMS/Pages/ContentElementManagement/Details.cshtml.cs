using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kredek.Areas.CMS.Pages.ContentElementManagement
{
    public class DetailsModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public ContentElement ContentElement { get; set; }

        public DetailsModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContentElement = await _context.ContentElement
                .Include(c => c.WebsitePage).FirstOrDefaultAsync(m => m.ContentElementId == id);

            if (ContentElement == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}