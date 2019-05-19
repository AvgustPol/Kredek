using Kredek.Data.Models;
using Kredek.Data.Models.ContentElementTranslationTemplates;
using Kredek.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kredek.Areas.CMS.Pages.Test
{
    public class AllElementsModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public string LanguageName { get; private set; } = "Polski";
        public IList<ContentElement> PageContent { get; set; }
        public PageContentViewModel PageContentViewModel { get; set; }

        public AllElementsModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(string pageName)
        {
            PageContent = await _context.ContentElement
                .Include(x => x.ContentElementTranslations)
                .ToListAsync();

            CreateViewModel(pageName, PageContent[0].ContentElementTranslations.ToList());
        }

        private List<PageContentElementViewModel> CreatePageContentElementViewModel(List<ContentElementTranslation> elementsTranslations)
        {
            var elements = new List<PageContentElementViewModel>();

            foreach (var translation in elementsTranslations)
            {
                elements.Add(new PageContentElementViewModel()
                {
                    TypeName = translation.GetType().ShortDisplayName()
                });
            }

            return elements;
        }

        private void CreateViewModel(string pageName, List<ContentElementTranslation> elementsTranslations)
        {
            PageContentViewModel = new PageContentViewModel()
            {
                Elements = CreatePageContentElementViewModel(elementsTranslations),
                PageName = pageName
            };
        }
    }
}