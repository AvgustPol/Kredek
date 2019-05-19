using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kredek.Data;
using Kredek.Data.Models.ContentElementTranslationTemplates;

namespace Kredek.Areas.CMS.Pages.ContentElementTranslationManagement.TextSeparatedByLineManagement
{
    public class DetailsModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public DetailsModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public TextSeparatedByLine TextSeparatedByLine { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TextSeparatedByLine = await _context.TemplatesTextSeparatedByLine
                .Include(t => t.ContentElement)
                .Include(t => t.Language).FirstOrDefaultAsync(m => m.ContentElementTranslationId == id);

            if (TextSeparatedByLine == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
