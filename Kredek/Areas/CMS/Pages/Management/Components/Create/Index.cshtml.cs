using System.Threading.Tasks;
using Kredek.Data;
using Kredek.Data.Models.ContentElementTranslationTemplates;
using Kredek.Data.ViewModels;
using Kredek.Models.Common.Emuns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kredek.Areas.CMS.Pages.Management.Components.Create
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public AvailableTemplates Type { get; set; }
        public int WebsitePageId { get; set; }
        public ContentElementTranslation NewElement { get; set; }
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult OnGet(int id, AvailableTemplates type)
        {
            WebsitePageId = id;

            switch (type)
            {
                case AvailableTemplates.Foo:
                    NewElement = new FooViewModel();
                    break;

                case AvailableTemplates.Boo:
                    NewElement = new BooViewModel();
                    break;
            }

            ViewData["Languages"] = new SelectList(_context.Languages, "LanguageId", "Name");
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
