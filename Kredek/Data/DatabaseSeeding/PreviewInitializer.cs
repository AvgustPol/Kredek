using Kredek.Data.Models;
using Kredek.Global;
using System.Linq;

namespace Kredek.Data.DatabaseSeeding
{
    public class PreviewInitializer : IPreviewInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly string PreviewName = "Preview";
        private readonly string PreviewPolishTranslationTitle = "Polska versja demo strony";
        public WebsitePage PreviewPage { get; set; }
        public WebsitePageTranslation PreviewPagePl { get; set; }

        public PreviewInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreatePreview()
        {
            CreatePreviewPage();
            CreatePreviewPolishTranslation();
        }

        private void CreatePreviewElements()
        {
            //var preview = new TextSeparatedByLine()
            //{
            //    Title = "This is Text",
            //    SubTitle = "Separated By Line",
            //};

            //_context.TemplatesTextSeparatedByLine.Add(preview);
            //_context.SaveChanges();
        }

        private void CreatePreviewPage()
        {
            var preview = new WebsitePage()
            {
                Name = PreviewName,
                IsActive = true,
            };

            _context.WebsitePages.Add(preview);
            _context.SaveChanges();

            PreviewPage = _context.WebsitePages.Single(p => p.Name == PreviewName);
        }

        private void CreatePreviewPolishTranslation()
        {
            var polishLanguage =
                _context.Languages.Single(language => language.ISOCode == GlobalVariables.PolishLanguageIsoCode);

            var preview = new WebsitePageTranslation()
            {
                Title = PreviewPolishTranslationTitle,
                LanguageId = polishLanguage.LanguageId,
                Language = polishLanguage,
                WebsitePage = PreviewPage,
                WebsitePageId = PreviewPage.WebsitePageId
            };

            _context.WebsitePageTranslations.Add(preview);
            _context.SaveChanges();

            PreviewPagePl = _context.WebsitePageTranslations.Single(p => p.Title == PreviewPolishTranslationTitle);
        }
    }
}