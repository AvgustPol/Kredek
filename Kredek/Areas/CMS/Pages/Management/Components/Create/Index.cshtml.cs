using System;
using System.Threading.Tasks;
using Kredek.Data;
using Kredek.Data.Models.ContentElementTranslationTemplates;
using Kredek.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kredek.Areas.CMS.Pages.Management.Components.Create
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ContentElementTranslation NewElement { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            switch (NewElement)
            {
                case FooViewModel model:
                    await CreateNewElement(model);
                    break;
                case BooViewModel model:
                    await CreateNewElement(model);
                    break;
            }


            return RedirectToPage("./Index");
        }

        private async Task CreateNewElement(ContentElementTranslation model)
        {
            _context.ContentElementTranslation.Add(model);
            await _context.SaveChangesAsync();
        }
    }
}
