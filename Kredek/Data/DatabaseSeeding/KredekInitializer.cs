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
        #region Home page

        private const string HomePageTitleEn = "Home page";
        private const string HomePageTitlePl = "Strona główna";
        private readonly string HomeNavigationTabNameEn = "Home";
        private readonly string HomeNavigationTabNamePl = "Główna";
        private readonly string HomePageName = GlobalVariables.HomePageName;
        private readonly int HomePageNavigationIndex = 1;

        #endregion Home page

        #region Team page

        private const string TeamNavigationTabNameEn = "Team";
        private const string TeamNavigationTabNamePl = "Zespół";
        private const string TeamPageTitleEn = "Team";
        private const string TeamPageTitlePl = "Zespół";
        private readonly string TeamPageName = "Team";
        private readonly int TeamPageNavigationIndex = 2;

        #endregion Team page

        #region Course page

        private const string CourseNavigationTabNameEn = "Course";
        private const string CourseNavigationTabNamePl = "Kurs";
        private const string CoursePageTitleEn = "Course";
        private const string CoursePageTitlePl = "Kurs";
        private readonly string CoursePageName = "Course";
        private readonly int CoursePageNavigationIndex = 3;

        #endregion Course page

        #region Blog page

        private const string BlogNavigationTabNameEn = "Blog";
        private const string BlogNavigationTabNamePl = "Blog";
        private const string BlogPageTitleEn = "Blog";
        private const string BlogPageTitlePl = "Blog";
        private readonly string BlogPageName = "Blog";
        private readonly int BlogPageNavigationIndex = 4;

        #endregion Blog page

        #region About page

        private const string AboutNavigationTabNameEn = "About";
        private const string AboutNavigationTabNamePl = "O Nas";
        private const string AboutPageTitleEn = "About";
        private const string AboutPageTitlePl = "O Nas";
        private readonly string AboutPageName = "About";
        private readonly int AboutPageNavigationIndex = 5;

        #endregion About page

        #region Contact page

        private const string ContactNavigationTabNameEn = "Contact";
        private const string ContactNavigationTabNamePl = "Kontakt";
        private const string ContactPageTitleEn = "Contact";
        private const string ContactPageTitlePl = "Kontakt";
        private readonly string ContactPageName = "Contact";
        private readonly int ContactPageNavigationIndex = 6;

        #endregion Contact page

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

        public void CreateANewPage(string name, int navigationIndex, bool isActive = true)
        {
            var newPage = new WebsitePage()
            {
                Name = name,
                IsActive = isActive,
                NavigationIndex = navigationIndex
            };

            _context.WebsitePages.Add(newPage);
            _context.SaveChanges();
        }

        public void CreateANewPageTranslation(WebsitePage page, Language language, string title, string navigationTabName)
        {
            var newPageTranslation = new WebsitePageTranslation()
            {
                Language = language,
                WebsitePage = page,
                Title = title,
                NameInNavigationBar = navigationTabName
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
            CreateANewPage(GlobalVariables.HomePageName, HomePageNavigationIndex, GlobalVariables.HomePageIsActive);
            CreateANewPage(BlogPageName, BlogPageNavigationIndex);
            CreateANewPage(AboutPageName, AboutPageNavigationIndex);
            CreateANewPage(TeamPageName, TeamPageNavigationIndex);
            CreateANewPage(ContactPageName, ContactPageNavigationIndex);
            CreateANewPage(CoursePageName, CoursePageNavigationIndex);
        }

        public void CreateDefaultPagesTranslations()
        {
            var languages = _context.Languages.ToList();

            //Home page translations
            CreatePagePlAndEnTranslations(HomePageName, languages, HomePageTitlePl, HomePageTitleEn, HomeNavigationTabNamePl, HomeNavigationTabNameEn);

            //Blog page translations
            CreatePagePlAndEnTranslations(BlogPageName, languages, BlogPageTitlePl, BlogPageTitleEn, BlogNavigationTabNamePl, BlogNavigationTabNameEn);
            //About page translations
            CreatePagePlAndEnTranslations(AboutPageName, languages, AboutPageTitlePl, AboutPageTitleEn, AboutNavigationTabNamePl, AboutNavigationTabNameEn);

            //TeamPageName page translations
            CreatePagePlAndEnTranslations(TeamPageName, languages, TeamPageTitlePl, TeamPageTitleEn, TeamNavigationTabNamePl, TeamNavigationTabNameEn);

            //Contact page translations
            CreatePagePlAndEnTranslations(ContactPageName, languages, ContactPageTitlePl, ContactPageTitleEn, ContactNavigationTabNamePl, ContactNavigationTabNameEn);

            //Course page translations
            CreatePagePlAndEnTranslations(CoursePageName, languages, CoursePageTitlePl, CoursePageTitleEn, CourseNavigationTabNamePl, CourseNavigationTabNameEn);
        }

        /// <summary>
        /// Hardcode method to create Polish and English page translations
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="languages"></param>
        /// <param name="pageTitlePl"></param>
        /// <param name="pageTitleEn"></param>
        /// <param name="navigationTabNamePl"></param>
        /// <param name="navigationTabNameEn"></param>
        private void CreatePagePlAndEnTranslations(string pageName, IList<Language> languages, string pageTitlePl, string pageTitleEn, string navigationTabNamePl, string navigationTabNameEn)
        {
            var page = _context.WebsitePages.Single(x => x.Name == pageName);

            //Polish version
            CreateANewPageTranslation(page,
                languages.Single(x => x.ISOCode == GlobalVariables.PolishLanguageIsoCode), pageTitlePl, navigationTabNamePl);

            //English version
            CreateANewPageTranslation(page,
                languages.Single(x => x.ISOCode == GlobalVariables.EnglishLanguageIsoCode), pageTitleEn, navigationTabNameEn);
        }
    }
}