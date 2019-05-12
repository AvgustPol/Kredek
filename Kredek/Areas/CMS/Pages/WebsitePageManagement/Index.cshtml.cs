using System.Collections.Generic;
using System.Threading.Tasks;
using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Kredek.Areas.CMS.Pages.WebsitePageManagement
{
    public class IndexModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public IndexModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<WebsitePage> WebsitePage { get;set; }

        public async Task OnGetAsync()
        {
            WebsitePage = await _context.WebsitePages.ToListAsync();
        }
    }
}
