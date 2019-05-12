using System.Linq;
using System.Threading.Tasks;
using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kredek.Areas.CMS.Pages.ContentElementManagement
{
    public class EditModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public EditModel(Kredek.Data.ApplicationDbContext context)
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
           ViewData["WebsitePageId"] = new SelectList(_context.WebsitePages, "WebsitePageId", "WebsitePageId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ContentElement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentElementExists(ContentElement.ContentElementId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ContentElementExists(int id)
        {
            return _context.ContentElement.Any(e => e.ContentElementId == id);
        }
    }
}
