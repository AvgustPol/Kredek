using Kredek.Data;
using Kredek.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kredek.Logic.Factories
{
    public class ViewModelsFactory : IViewModelsFactory
    {
        private readonly ApplicationDbContext _context;
        private readonly IPageElementViewModelsFactory _pageElementViewModelsFactory;

        public ViewModelsFactory(ApplicationDbContext context, IPageElementViewModelsFactory pageElementViewModelsFactory)
        {
            _context = context;
            _pageElementViewModelsFactory = pageElementViewModelsFactory;
        }

        public async Task<List<PageElementViewModel>> CreateViewModel(string language, string pageName)
        {
            var currentPage = await _context.WebsitePages
                .SingleAsync(page => page.Name == pageName);

            var currentPageElements = await _context.ContentElements.Where(x => x.WebsitePage == currentPage)
                .OrderBy(q => q.Position)
                    .Include(y => y.ContentElementTranslations)
                    .SelectMany(x => x.ContentElementTranslations)
                        .Where(x => x.Language.ISOCode == language)
                            .ToListAsync();

            List<PageElementViewModel> currentPageViewModels = new List<PageElementViewModel>();

            foreach (var pageElement in currentPageElements)
            {
                var tmp = await _pageElementViewModelsFactory.CreatePageElementViewModel();
                currentPageViewModels.Add(tmp);
            }

            return currentPageViewModels;
        }
    }
}