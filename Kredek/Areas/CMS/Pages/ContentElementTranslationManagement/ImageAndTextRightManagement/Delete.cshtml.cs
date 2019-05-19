using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kredek.Data;
using Kredek.Data.Models.ContentElementTranslationTemplates;

namespace Kredek.Areas.CMS.Pages.ContentElementTranslationManagement.ImageAndTextRightManagement
{
    public class DeleteModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public DeleteModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ImageAndTextRight ImageAndTextRight { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ImageAndTextRight = await _context.TemplatesImageAndTextRight
                .Include(i => i.ContentElement)
                .Include(i => i.Language).FirstOrDefaultAsync(m => m.ContentElementTranslationId == id);

            if (ImageAndTextRight == null)
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

            ImageAndTextRight = await _context.TemplatesImageAndTextRight.FindAsync(id);

            if (ImageAndTextRight != null)
            {
                _context.TemplatesImageAndTextRight.Remove(ImageAndTextRight);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
