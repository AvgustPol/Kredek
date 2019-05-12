using System.Threading.Tasks;
using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Kredek.Areas.CMS.Pages.ContentElementManagement
{
    public class DeleteModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public DeleteModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ContentElement ContentElement { get; set; }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContentElement = await _context.ContentElement.FindAsync(id);

            if (ContentElement != null)
            {
                _context.ContentElement.Remove(ContentElement);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
