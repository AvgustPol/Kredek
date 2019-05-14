using Kredek.Data.Models;
using Kredek.Global;
using System.Collections.Generic;
using System.Linq;

namespace Kredek.Data.DatabaseSeeding
{
    /// <summary>
    /// Initializing kredek pages
    /// </summary>
    public class KredekInitializer : IKredekInitializer
    {
        #region Blog

        private const string HomePageTitleEn = "Home page";
        private const string HomePageTitlePl = "Strona główna";
        private readonly string HomeNavigationTabNameEn = "Home";
        private readonly string HomeNavigationTabNamePl = "Główna";
        private readonly string HomePageName = GlobalVariables.HomePageName;

        #endregion Blog

        #region Blog

        private const string BlogNavigationTabNameEn = "Blog";
        private const string BlogNavigationTabNamePl = "Blog";
        private const string BlogPageTitleEn = "Blog";
        private const string BlogPageTitlePl = "Blog";
        private readonly string BlogPageName = "Blog";

        #endregion Blog

        private readonly ApplicationDbContext _context;

        public KredekInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateANewLanguage(string name, string isoCode)
        {
            var newLanguage = new Language()
            {
                Name = name,
                ISOCode = isoCode
            };

            _context.Languages.Add(newLanguage);
            _context.SaveChanges();
        }

        public void CreateANewPage(string name, bool isActive = true)
        {
            var newPage = new WebsitePage()
            {
                Name = name,
                IsActive = isActive,
            };

            _context.WebsitePages.Add(newPage);
            _context.SaveChanges();
        }

        public void CreateANewPagePageTranslation(WebsitePage page, Language language, string title, string navigationTabName)
        {
            var newPageTranslation = new WebsitePageTranslation()
            {
                Language = language,
                WebsitePage = page,
                Title = title,
                NavigationTabName = navigationTabName
            };

            _context.WebsitePageTranslations.Add(newPageTranslation);
            _context.SaveChanges();
        }

        public void CreateDefaultLanguages()
        {
            CreateANewLanguage(GlobalVariables.EnglishLanguageName, GlobalVariables.EnglishLanguageIsoCode);
            CreateANewLanguage(GlobalVariables.PolishLanguageName, GlobalVariables.PolishLanguageIsoCode);
        }

        public void CreateDefaultPages()
        {
            CreateANewPage(GlobalVariables.HomePageName, GlobalVariables.HomePageIsActive);
            CreateANewPage(BlogPageName);
        }

        public void CreateDefaultPagesTranslations()
        {
            var languages = _context.Languages.ToList();

            //Home page translations
            CreatePagePlAndEnTranslations(HomePageName, languages, HomePageTitlePl, HomePageTitleEn, HomeNavigationTabNamePl, HomeNavigationTabNameEn);

            //Blog page translations
            CreatePagePlAndEnTranslations(BlogPageName, languages, BlogPageTitlePl, BlogPageTitleEn, BlogNavigationTabNamePl, BlogNavigationTabNameEn);
        }

        /// <summary>
        /// Hardcode method to create Polish and English page translations
        /// </summary>
        /// <param name="languages"></param>
        /// <param name="pageTitlePl"></param>
        /// <param name="pageTitleEn"></param>
        /// <param name="pageName"></param>
        private void CreatePagePlAndEnTranslations(string pageName, IList<Language> languages, string pageTitlePl, string pageTitleEn, string navigationTabNamePl, string navigationTabNameEn)
        {
            var page = _context.WebsitePages.Single(x => x.Name == pageName);

            //Polish version
            CreateANewPagePageTranslation(page,
                languages.Single(x => x.ISOCode == GlobalVariables.PolishLanguageIsoCode), pageTitlePl, navigationTabNamePl);

            //English version
            CreateANewPagePageTranslation(page,
                languages.Single(x => x.ISOCode == GlobalVariables.EnglishLanguageIsoCode), pageTitleEn, navigationTabNameEn);
        }
    }
}