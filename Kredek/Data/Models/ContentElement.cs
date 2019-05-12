using System.Collections.Generic;
using Kredek.Data.Models.ContentElementTranslationTemplates;

namespace Kredek.Data.Models
{
    public class ContentElement
    {
        #region Class properties

        public int ContentElementId { get; set; }

        /// <summary>
        /// Index of the element on the webpage
        /// 0 - top of the webpage
        /// </summary>
        public int Position { get; set; }

        #endregion Class properties

        #region [ContentElementTranslation] 1 to N relationship

        public ICollection<ContentElementTranslation> ContentElementTranslations { get; set; }

        #endregion [ContentElementTranslation] 1 to N relationship

        #region [CurrentPage] 1 to N relationship

        public WebsitePage WebsitePage { get; set; }
        public int WebsitePageId { get; set; }

        #endregion [CurrentPage] 1 to N relationship
    }
}