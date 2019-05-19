using FacebookPageGetter.Services.FacebookService;
using Kredek.Data;
using Kredek.Data.Models;
using Kredek.Global;
using Kredek.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kredek.Pages
{
    public class IndexModel : PageModel
    {
        #region Default Variables

        private const string CookieLanguageName = DefaultVariables.CookieLanguageName;

        private const int CookieTime = DefaultVariables.CookieLifeTime;

        private const string DefaultLanguage = DefaultVariables.DefaultLanguage;
        private const string DefaultPage = DefaultVariables.DefaultPage;

        #endregion Default Variables

        private readonly ApplicationDbContext _context;
        private readonly ICookiesManager _cookiesManager;

        /// <summary>
        /// ISO code of current language
        /// </summary>
        public string CurrentLanguage { get; set; }

        public WebsitePage CurrentPage { get; set; }
        public WebsitePageTranslation CurrentPageTranslation { get; set; }
        public IFacebookService FacebookService { get; set; }

        /// <summary>
        /// List of all available languages.
        /// </summary>
        public IList<Language> Languages { get; set; }

        /// <summary>
        /// Dictionary( pageName , Dictionary (language, navigationTabName) )
        /// e.g. (about , (en , about))
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> Navigation { get; set; }

        public IndexModel(ApplicationDbContext context, ICookiesManager cookiesManager, IFacebookService facebookService)
        {
            _context = context;
            _cookiesManager = cookiesManager;
            FacebookService = facebookService;

            CreateLanguages();
            CreateNavigation();
        }

        public async Task<IActionResult> OnGetAsync(string language = DefaultLanguage, string pageName = DefaultPage)
        {
            GetPageLanguageFromCookie();

            return await LoadPage(pageName);
        }

        public async Task<IActionResult> OnPostChangeLanguageAsync(string language, string pageName)
        {
            SetPageLanguage(language);

            return await LoadPage(pageName);
        }

        #region Methods

        private void CreateLanguages()
        {
            Languages = _context.Languages.ToList();
        }

        private void CreateNavigation()
        {
            Navigation = new Dictionary<string, Dictionary<string, string>>();

            var allPages = _context.WebsitePages.Include(x => x.WebsitePageTranslations).OrderBy(x => x.NavigationIndex).ToList();

            foreach (var page in allPages)
            {
                if (page.IsActive)
                {
                    Navigation[page.Name] = new Dictionary<string, string>();
                    foreach (var translation in page.WebsitePageTranslations)
                    {
                        Navigation[page.Name][translation.Language.ISOCode] =
                            translation.NameInNavigationBar;
                    }
                }
            }
        }

        private void GetPageLanguageFromCookie()
        {
            var key = CookieLanguageName;

            //read cookie from Request object
            string cookieValue = Request.Cookies[key];

            //if cookie is empty
            if (string.IsNullOrEmpty(cookieValue))
            {
                //set value to default
                _cookiesManager.Set(Response, key, DefaultLanguage, CookieTime);
                CurrentLanguage = DefaultLanguage;
            }
            else
            {
                CurrentLanguage = cookieValue;
            }
        }

        private async Task<IActionResult> LoadPage(string pageName)
        {
            CurrentPage = await _context.WebsitePages.Where(page => page.Name == pageName).SingleAsync();
            CurrentPageTranslation = await _context.WebsitePageTranslations.Where(t => t.WebsitePage == CurrentPage && t.Language.ISOCode == CurrentLanguage)
                .SingleAsync();

            return Page();
        }

        /// <summary>
        /// Updates object and cookies CurrentLanguage value
        /// </summary>
        /// <param name="language"></param>
        private void SetPageLanguage(string language)
        {
            CurrentLanguage = language;
            _cookiesManager.Set(Response, CookieLanguageName, CurrentLanguage, CookieTime);
        }

        #endregion Methods
    }
}