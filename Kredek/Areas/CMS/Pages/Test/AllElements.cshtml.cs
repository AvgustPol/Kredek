using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [BindProperty]
        public int WebsitePageId { get; set; }

        public AllElementsModel(Kredek.Data.ApplicationDbContext context)
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
                .ToListAsync();
        }
    }
}