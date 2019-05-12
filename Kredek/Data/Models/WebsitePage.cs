using System.Collections.Generic;

namespace Kredek.Data.Models
{
    public class WebsitePage
    {
        #region Class properties

        /// <summary>
        /// False - page is in an archive and it is not active (doesn't rendering on site)
        /// </summary>
        public bool IsActive { get; set; }

        public string Name { get; set; }
        public int WebsitePageId { get; set; }

        #endregion Class properties

        #region [WebsitePageTranslation] 1 to N relationship

        public ICollection<WebsitePageTranslation> WebsitePageTranslations { get; set; }

        #endregion [WebsitePageTranslation] 1 to N relationship

        #region [ContentElement] 1 to N relationship

        public ICollection<ContentElement> ContentElements { get; set; }

        #endregion [ContentElement] 1 to N relationship
    }
}