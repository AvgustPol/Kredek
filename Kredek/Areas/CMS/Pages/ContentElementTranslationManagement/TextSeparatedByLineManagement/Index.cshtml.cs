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
    public class IndexModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public IndexModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TextSeparatedByLine> TextSeparatedByLine { get;set; }

        public async Task OnGetAsync()
        {
            TextSeparatedByLine = await _context.TemplatesTextSeparatedByLine
                .Include(t => t.ContentElement)
                .Include(t => t.Language).ToListAsync();
        }
    }
}
