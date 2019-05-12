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
        private const string ThisWebsiteRootUrl = DefaultVariables.ThisWebsiteRootUrl;

        #endregion Default Variables

        private readonly ApplicationDbContext _context;
        private readonly ICookiesManager _cookiesManager;

        public string CurrentLanguage { get; set; }
        public WebsitePage CurrentPage { get; set; }
        public IFacebookService FacebookService { get; set; }

        /// <summary>
        /// List of all available languages.
        /// </summary>
        public IList<string> Languages { get; set; }

        /// <summary>
        /// List of all active pages.
        /// </summary>
        public IList<string> Navigation { get; set; }

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
            Languages = new List<string>();

            var allLanguages = _context.Languages.ToList();
            foreach (var language in allLanguages)
            {
                Languages.Add(language.ISOCode);
            }
        }

        private void CreateNavigation()
        {
            Navigation = new List<string>();

            var allPages = _context.WebsitePages.ToList();
            foreach (var page in allPages)
            {
                if (page.IsActive)
                {
                    Navigation.Add(page.Name);
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
            }
            else
            {
                CurrentLanguage = cookieValue;
            }
        }

        private async Task<IActionResult> LoadPage(string pageName)
        {
            if (!Navigation.Contains(pageName))
            {
                // hardcode
                return RedirectPermanent($"{ThisWebsiteRootUrl}/{DefaultLanguage}/{DefaultPage}");
            }

            CurrentPage = await _context.WebsitePages
                .Include(page => page.ContentElements)
                .ThenInclude(elements => elements.ContentElementTranslations)
                .SingleAsync(page => page.Name == pageName);

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