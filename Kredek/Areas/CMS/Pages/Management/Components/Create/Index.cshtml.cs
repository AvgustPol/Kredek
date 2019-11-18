using System.Threading.Tasks;
using Kredek.Data;
using Kredek.Data.Models;
using Kredek.Data.Models.ContentElementTranslationTemplates;
using Kredek.Data.Models.CreatableModels;
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

        public ExampleCreatableModel NewElement { get; set; }

        public ContentElement ContentElement { get; set; }

        #region Models 
        public Example ExampleNewElement { get; set; }

        #endregion


        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id, AvailableTemplates type)
        {
            WebsitePageId = id;

            switch (type)
            {
                case AvailableTemplates.Example:
                    NewElement = new ExampleCreatableModel();
                    break;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var newElement = NewElement.GetContentElementTranslation();

            switch (newElement)
            {
                case Example model:
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
