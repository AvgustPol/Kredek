using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kredek.Data;
using Kredek.Data.Models.ContentElementTranslationTemplates;

namespace Kredek.Areas.CMS.Pages.ContentElementTranslationManagement.ImageAndTextLeftManagement
{
    public class DeleteModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public DeleteModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ImageAndTextLeft ImageAndTextLeft { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ImageAndTextLeft = await _context.TemplatesImageAndTextLeft
                .Include(i => i.ContentElement)
                .Include(i => i.Language).FirstOrDefaultAsync(m => m.ContentElementTranslationId == id);

            if (ImageAndTextLeft == null)
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

            ImageAndTextLeft = await _context.TemplatesImageAndTextLeft.FindAsync(id);

            if (ImageAndTextLeft != null)
            {
                _context.TemplatesImageAndTextLeft.Remove(ImageAndTextLeft);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
