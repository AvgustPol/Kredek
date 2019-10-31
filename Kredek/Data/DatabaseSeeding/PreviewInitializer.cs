using Kredek.Data.Models;
using Kredek.Global;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kredek.Data.DatabaseSeeding
{
    public class PreviewInitializer : IPreviewInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly string PreviewPageName = DefaultVariables.PreviewPage;

        private readonly string PreviewPolishTranslationTitle = "Demo wersja strony";
        private readonly string PreviewPolishTranslationNameInNavigationBar = "Demo strona";
        private readonly string PreviewEnglishTranslationTitle = "Demo version of the page";
        private readonly string PreviewEnglishTranslationNameInNavigationBar = "Demo page";


        public PreviewInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreatePreview()
        {
            WebsitePage createdPreviewPage = CreatePreviewPage();

            WebsitePageTranslation createdPreviewPagePl = CreatePreviewPolishTranslation(createdPreviewPage);
            WebsitePageTranslation createdPreviewPageEng = CreatePreviewEnglishTranslation(createdPreviewPage);
        }

        private WebsitePage CreatePreviewPage()
        {
            var preview = new WebsitePage()
            {
                Name = PreviewPageName,
                IsActive = true,
            };

            _context.WebsitePages.Add(preview);
            _context.SaveChanges();

            return _context.WebsitePages.Single(p => p.Name == PreviewPageName);
        }

        private WebsitePageTranslation CreatePreviewEnglishTranslation(WebsitePage previewPage)
        {
            var englishLanguage =
                _context.Languages.Single(language => language.ISOCode == GlobalVariables.EnglishLanguageIsoCode);

            var preview = new WebsitePageTranslation()
            {
                NameInNavigationBar = PreviewEnglishTranslationNameInNavigationBar,
                Title = PreviewEnglishTranslationTitle,
                LanguageId = englishLanguage.LanguageId,
                Language = englishLanguage,
                WebsitePage = previewPage,
                WebsitePageId = previewPage.WebsitePageId
            };

            _context.WebsitePageTranslations.Add(preview);
            _context.SaveChanges();

            return _context.WebsitePageTranslations.Single(p => p.Title == PreviewEnglishTranslationTitle);
        }

        private WebsitePageTranslation CreatePreviewPolishTranslation(WebsitePage previewPage)
        {
            var polishLanguage =
                _context.Languages.Single(language => language.ISOCode == GlobalVariables.PolishLanguageIsoCode);

            var preview = new WebsitePageTranslation()
            {
                NameInNavigationBar = PreviewPolishTranslationNameInNavigationBar,
                Title = PreviewPolishTranslationTitle,
                LanguageId = polishLanguage.LanguageId,
                Language = polishLanguage,
                WebsitePage = previewPage,
                WebsitePageId = previewPage.WebsitePageId
            };

            _context.WebsitePageTranslations.Add(preview);
            _context.SaveChanges();

            return _context.WebsitePageTranslations.Single(p => p.Title == PreviewPolishTranslationTitle);
        }
    }
}