using Kredek.Data.Models;
using Kredek.Data.Models.ContentElementTranslationTemplates;
using Kredek.Global;
using Microsoft.EntityFrameworkCore;
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

        private const string HomeNavigationTabNameEn = "Home";
        private const string HomeNavigationTabNamePl = "Główna";
        private const int HomePageNavigationIndex = 1;
        private const string HomePageTitleEn = "Home page";
        private const string HomePageTitlePl = "Strona główna";
        private readonly string HomePageName = GlobalVariables.HomePageName;

        #endregion Home page

        #region Team page

        private const string TeamNavigationTabNameEn = "Team";
        private const string TeamNavigationTabNamePl = "Zespół";
        private const string TeamPageName = "Team";
        private const int TeamPageNavigationIndex = 2;
        private const string TeamPageTitleEn = "Team";
        private const string TeamPageTitlePl = "Zespół";

        #endregion Team page

        #region Course page

        private const string CourseNavigationTabNameEn = "Course";
        private const string CourseNavigationTabNamePl = "Kurs";
        private const string CoursePageName = "Course";
        private const int CoursePageNavigationIndex = 3;
        private const string CoursePageTitleEn = "Course";
        private const string CoursePageTitlePl = "Kurs";

        #endregion Course page

        #region Blog page

        private const string BlogNavigationTabNameEn = "Blog";
        private const string BlogNavigationTabNamePl = "Blog";
        private const string BlogPageName = "Blog";
        private const int BlogPageNavigationIndex = 4;
        private const string BlogPageTitleEn = "Blog";
        private const string BlogPageTitlePl = "Blog";

        #endregion Blog page

        #region About page

        private const string AboutNavigationTabNameEn = "About";
        private const string AboutNavigationTabNamePl = "O Nas";
        private const string AboutPageName = "About";
        private const int AboutPageNavigationIndex = 5;
        private const string AboutPageTitleEn = "About";
        private const string AboutPageTitlePl = "O Nas";

        #endregion About page

        #region Contact page

        private const string ContactNavigationTabNameEn = "Contact";
        private const string ContactNavigationTabNamePl = "Kontakt";
        private const string ContactPageName = "Contact";
        private const int ContactPageNavigationIndex = 6;
        private const string ContactPageTitleEn = "Contact";
        private const string ContactPageTitlePl = "Kontakt";

        #endregion Contact page

        private readonly ApplicationDbContext _context;

        public KredekInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateDefaultContentElements()
        {
            #region Home page

            //banner
            CreateANewContentElement(GlobalVariables.HomePageName, 1);
            //text left
            CreateANewContentElement(GlobalVariables.HomePageName, 2);
            //text right
            CreateANewContentElement(GlobalVariables.HomePageName, 3);
            //banner with buttons
            CreateANewContentElement(GlobalVariables.HomePageName, 4);

            #endregion Home page

            //CreateANewContentElement(BlogPageName, 1);
            //CreateANewContentElement(AboutPageName, 1);
            //CreateANewContentElement(TeamPageName, 1);
            //CreateANewContentElement(ContactPageName, 1);
            //CreateANewContentElement(CoursePageName, 1);
        }

        public void CreateDefaultContentElementsTranslations()
        {
            var homePage = _context.WebsitePages.Where(x => x.Name == HomePageName).Include(y => y.ContentElements)
                .FirstOrDefault();
            var plLanguage = _context.Languages.Single(x => x.ISOCode == GlobalVariables.PolishLanguageIsoCode);
            var enLanguage = _context.Languages.Single(x => x.ISOCode == GlobalVariables.EnglishLanguageIsoCode);

            var contentElement1 = homePage.ContentElements.ToList()[0];

            //banner
            Hardcode_Creating_TextSeparatedByLine(contentElement1, plLanguage);

            var contentElement2 = homePage.ContentElements.ToList()[1];
            //image and text left
            Hardcode_Creating_Pl_ImageAndTextLeft(contentElement2, plLanguage);

            Hardcode_Creating_En_ImageAndTextLeft(contentElement2, enLanguage);
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
            CreateANewPageTranslationPlAndEn(HomePageName, languages, HomePageTitlePl, HomePageTitleEn,
                HomeNavigationTabNamePl, HomeNavigationTabNameEn);

            //Blog page translations
            CreateANewPageTranslationPlAndEn(BlogPageName, languages, BlogPageTitlePl, BlogPageTitleEn,
                BlogNavigationTabNamePl, BlogNavigationTabNameEn);
            //About page translations
            CreateANewPageTranslationPlAndEn(AboutPageName, languages, AboutPageTitlePl, AboutPageTitleEn,
                AboutNavigationTabNamePl, AboutNavigationTabNameEn);

            //TeamPageName page translations
            CreateANewPageTranslationPlAndEn(TeamPageName, languages, TeamPageTitlePl, TeamPageTitleEn,
                TeamNavigationTabNamePl, TeamNavigationTabNameEn);

            //Contact page translations
            CreateANewPageTranslationPlAndEn(ContactPageName, languages, ContactPageTitlePl, ContactPageTitleEn,
                ContactNavigationTabNamePl, ContactNavigationTabNameEn);

            //Course page translations
            CreateANewPageTranslationPlAndEn(CoursePageName, languages, CoursePageTitlePl, CoursePageTitleEn,
                CourseNavigationTabNamePl, CourseNavigationTabNameEn);
        }

        private void CreateANewContentElement(string pageName, int position)
        {
            var page = _context.WebsitePages.Single(x => x.Name == pageName);

            var newContentElement = new ContentElement()
            {
                Position = position,
                WebsitePage = page
            };

            _context.ContentElements.Add(newContentElement);
            _context.SaveChanges();
        }

        private void CreateANewImageAndTextLeft(ContentElement contentElement, Language language, string title,
            string text, string imageUrl)
        {
            var imageAndTextLeft = new ImageAndTextLeft()
            {
                Language = language,
                ContentElement = contentElement,
                Title = title,
                Text = text,
                ImageUrl = imageUrl
            };

            _context.ContentElementTranslation.Add(imageAndTextLeft);
            _context.SaveChanges();
        }

        private void CreateANewLanguage(string name, string isoCode)
        {
            var newLanguage = new Language()
            {
                Name = name,
                ISOCode = isoCode
            };

            _context.Languages.Add(newLanguage);
            _context.SaveChanges();
        }

        private void CreateANewPage(string name, int navigationIndex, bool isActive = true)
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

        private void CreateANewPageTranslation(WebsitePage page, Language language, string title,
            string navigationTabName)
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

        /// <summary>
        /// Hardcode method to create Polish and English page translations
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="languages"></param>
        /// <param name="pageTitlePl"></param>
        /// <param name="pageTitleEn"></param>
        /// <param name="navigationTabNamePl"></param>
        /// <param name="navigationTabNameEn"></param>
        private void CreateANewPageTranslationPlAndEn(string pageName, IList<Language> languages, string pageTitlePl, string pageTitleEn, string navigationTabNamePl, string navigationTabNameEn)
        {
            var page = _context.WebsitePages.Single(x => x.Name == pageName);

            //Polish version
            CreateANewPageTranslation(page,
                languages.Single(x => x.ISOCode == GlobalVariables.PolishLanguageIsoCode), pageTitlePl,
                navigationTabNamePl);

            //English version
            CreateANewPageTranslation(page,
                languages.Single(x => x.ISOCode == GlobalVariables.EnglishLanguageIsoCode), pageTitleEn,
                navigationTabNameEn);
        }

        private void CreateANewTextSeparatedByLine(ContentElement contentElement, Language language, string title, string subTitle, string imageUrl)
        {
            var newTranslation = new TextSeparatedByLine()
            {
                Language = language,
                ContentElement = contentElement,
                Title = title,
                SubTitle = subTitle,
                ImageUrl = imageUrl
            };

            _context.ContentElementTranslation.Add(newTranslation);
            _context.SaveChanges();
        }

        private void Hardcode_Creating_En_ImageAndTextLeft(ContentElement contentElement, Language language)
        {
            var textPl = "Who we are";
            var titlePl = "[Google translate power! c:] The scientific circle \"Kredek\" was founded on March 1, 2007. Every semester we launch the next course edition, during which we make ambitious students and IT specialists. The aim of the Scientific Society is to learn about programming technologies and skills in the future career and learning from each other. We implement our assumptions through meetings, lectures, laboratories and joint projects.";
            var imageUrl = "https://picsum.photos/600/400";

            CreateANewImageAndTextLeft(contentElement, language, textPl, titlePl, imageUrl);
        }

        private void Hardcode_Creating_Pl_ImageAndTextLeft(ContentElement contentElement, Language language)
        {
            var textPl = "Nasze koło";
            var titlePl = "Koło naukowe \"Kredek\" zostało założone dnia 1 Marca 2007. Co semestr uruchamiamy kolejną edycję, podczas której z ambitnych studentów robimy profesjonalnych informatyków. Celem Koła Naukowego jest poznawanie nowych technologii programistycznych, umiejętności przydatnych w przyszłej karierze zawodowej oraz uczenie się od siebie nawzajem. Nasze założenia realizujemy poprzez spotkania, wykłady, laboratoria oraz wspólne projekty.";
            var imageUrl = "https://picsum.photos/600/400";

            CreateANewImageAndTextLeft(contentElement, language, textPl, titlePl, imageUrl);
        }

        private void Hardcode_Creating_TextSeparatedByLine(ContentElement contentElement, Language language)
        {
            var titlePl = "KREDEK";
            var subTitlePl = "Creation and Development Group";
            var imgUrl = "https://picsum.photos/1600/1200";

            //banner
            CreateANewTextSeparatedByLine(contentElement, language, titlePl, subTitlePl, imgUrl);
        }
    }
}