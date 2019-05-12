namespace Kredek.Data.Models
{
    public class WebsitePageTranslation
    {
        #region Class properties

        /// <summary>
        /// Website title, that shows on the top of the browser tab.
        /// </summary>
        public string Title { get; set; }

        public int WebsitePageTranslationId { get; set; }

        #endregion Class properties

        #region [CurrentPage] 1 to N relationship

        public WebsitePage WebsitePage { get; set; }
        public int WebsitePageId { get; set; }

        #endregion [CurrentPage] 1 to N relationship

        #region [Language] 1 to 1 relationship

        public Language Language { get; set; }
        public int LanguageId { get; set; }

        #endregion [Language] 1 to 1 relationship
    }
}