using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kredek.Areas.CMS.Pages.WebsitePageManagement
{
    public class IndexModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public IList<WebsitePage> WebsitePage { get; set; }

        public IndexModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            WebsitePage = await _context.WebsitePages.ToListAsync();
        }
    }
}