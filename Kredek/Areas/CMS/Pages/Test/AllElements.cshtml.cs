using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kredek.Areas.CMS.Pages.Test
{
    public class AllElementsModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public string LanguageName { get; private set; } = "Polski";
        public IList<ContentElement> PageContent { get; set; }
        public string PageName { get; set; }

        public AllElementsModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(string pageName)
        {
            PageName = pageName;

            PageContent = await _context.ContentElement
                .Include(x => x.ContentElementTranslations)
                .ToListAsync();
        }
    }
}