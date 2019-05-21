using Kredek.Data;
using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kredek.Areas.CMS.Pages.ContentManagement
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IList<ContentElement> PageContent { get; set; }
        public string PageName { get; set; }

        [BindProperty]
        public int WebsitePageId { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(string pageName)
        {
            PageName = pageName;

            await GetContent();
        }

        public async Task OnPost()
        {
            PageName = (await _context.WebsitePages.SingleAsync(x => x.WebsitePageId == WebsitePageId)).Name;

            await GetContent();
        }

        private async Task GetContent()
        {
            ViewData["WebsitePageId"] = new SelectList(_context.WebsitePages, "WebsitePageId", "Name");

            PageContent = await _context.ContentElement
                .Include(x => x.ContentElementTranslations)
                    .ThenInclude(z => z.Language)
                .Where(x => x.WebsitePage.Name == PageName)
                .ToListAsync();
        }
    }
}