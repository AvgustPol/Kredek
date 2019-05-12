using System.Collections.Generic;
using System.Threading.Tasks;
using Kredek.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Kredek.Areas.CMS.Pages.ContentElementManagement
{
    public class IndexModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public IndexModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ContentElement> ContentElement { get;set; }

        public async Task OnGetAsync()
        {
            ContentElement = await _context.ContentElement
                .Include(c => c.WebsitePage).ToListAsync();
        }
    }
}
