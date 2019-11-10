using EmailService;
using FacebookPageGetter.Models.FeedPostDto;
using FacebookPageGetter.Services.FacebookService;
using Kredek.Data;
using Kredek.Data.Dto;
using Kredek.Data.Models;
using Kredek.Data.Models.ContentElementTranslationTemplates;
using Kredek.Global;
using Kredek.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kredek.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        #region Default Variables

        private const string CookieLanguageName = DefaultVariables.CookieLanguageName;

        private const int CookieTime = DefaultVariables.CookieLifeTime;

        private const string DefaultLanguage = DefaultVariables.DefaultLanguageIsoCode;
        private const string DefaultPage = DefaultVariables.DefaultPage;

        #endregion Default Variables

        public readonly IFacebookService _facebookService;
        private const int NUMBER_OF_FACEBOOK_POSTS = 5;
        private readonly ApplicationDbContext _context;
        private readonly ICookiesManager _cookiesManager;
        private readonly IEmailService _emailService;

        /// <summary>
        /// ISO code of current language
        /// </summary>
        public string CurrentLanguage { get; set; }

        public WebsitePage CurrentPage { get; set; }

        /// <summary>
        /// Elements on current page in current language
        /// </summary>
        public List<ContentElementTranslation> CurrentPageElements { get; set; }

        /// <summary>
        /// Property that stores data related to current page in current language
        /// </summary>
        public WebsitePageTranslation CurrentPageTranslation { get; set; }

        public EmailModel EmailInfo { get; set; }

        /// <summary>
        /// List of all available languages.
        /// </summary>
        public IList<Language> Languages { get; set; }

        /// <summary>
        /// Dictionary( pageName , Dictionary (language, navigationTabName) )
        /// e.g. (about , (en , about))
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> Navigation { get; set; }

        public IndexModel(ApplicationDbContext context, ICookiesManager cookiesManager, IFacebookService facebookService, IEmailService emailService)
        {
            _context = context;
            _cookiesManager = cookiesManager;
            _facebookService = facebookService;
            _emailService = emailService;

            CreateLanguages();
            CreateNavigation();
        }

        public async Task<FeedPostsDto> GetFacebookPostsAsync()
        {
            return await _facebookService.GetPostsAsync(NUMBER_OF_FACEBOOK_POSTS);
        }

        public async Task<IActionResult> OnGetAsync(string language = DefaultLanguage, string pageName = DefaultPage)
        {
            GetPageLanguageFromCookie();

            return await LoadPage(pageName);
        }

        public async Task<IActionResult> OnPostChangeLanguageAsync(string language, string pageName)
        {
            SetPageLanguage(language);

            CreateLanguages();
            CreateNavigation();

            return await LoadPage(pageName);
        }

        public async Task OnPostFooAsync()
        {
            _emailService.Message()
                            .FromServer()
                            .ToServer()
                            .WithSubject($"[ {EmailInfo.SubjectTag} ] + {EmailInfo.Subject}")
                            .WithBodyHtml(await GenerateHtmlMessage())
                                .Send();
        }

        private async Task<string> GenerateHtmlMessage()
        {
            string htmlLineBreak = "<br/>";
            StringBuilder builder = new StringBuilder();

            #region Title

            builder.Append($"<h2>Tytuł wiadomości</h2>");

            builder.Append($"{MakeBold("Kategorija: ")} {EmailInfo.SubjectTag}")
                .AppendLine().AppendLine();

            builder.Append($"{MakeBold("Tytuł: ")} {EmailInfo.Subject}")
                .AppendLine().AppendLine();

            #endregion Title

            #region Contact data

            builder.AppendLine().Append($"<h2>Dane kontaktowe </h2>");

            builder
                .Append($"{MakeBold("Imię:")} {EmailInfo.FirstName}")
                .AppendLine().AppendLine();
            builder
                .Append($"{MakeBold("Nazwisko:")} {EmailInfo.LastName}")
                .AppendLine().AppendLine();
            builder
                .Append($"{MakeBold("Email:")} {EmailInfo.Email}")
                .AppendLine()
                .AppendLine().AppendLine();

            #endregion Contact data

            #region Main message body

            builder.AppendLine().Append($"<h2>Treść: </h2>");

            builder.Append(EmailInfo.Text);

            string result = builder.ToString();

            result = result.Replace("\r\n", htmlLineBreak);

            #endregion Main message body

            return result;
        }

        private string MakeBold(string text)
        {
            return $"<b>{text}</b>";
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
            CurrentPage = await _context.WebsitePages
                .SingleAsync(page => page.Name == pageName);

            CurrentPageTranslation = await _context.WebsitePageTranslations
                .SingleAsync(t => t.WebsitePage == CurrentPage && t.Language.ISOCode == CurrentLanguage);

            CurrentPageElements = await _context.ContentElements.Where(x => x.WebsitePage == CurrentPage)
                .OrderBy(q => q.Position)
                    .Include(y => y.ContentElementTranslations)
                    .SelectMany(x => x.ContentElementTranslations)
                        .Where(x => x.Language.ISOCode == CurrentLanguage)
                            .ToListAsync();

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