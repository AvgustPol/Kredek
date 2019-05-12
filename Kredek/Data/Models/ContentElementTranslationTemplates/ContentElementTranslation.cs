namespace Kredek.Data.Models.ContentElementTranslationTemplates
{
    public abstract class ContentElementTranslation
    {
        #region Class properties

        public int ContentElementTranslationId { get; set; }

        #endregion Class properties

        #region [ContentElement] 1 to N relationship

        public ContentElement ContentElement { get; set; }
        public int ContentElementId { get; set; }

        #endregion [ContentElement] 1 to N relationship

        #region [Language] 1 to 1 relationship

        public Language Language { get; set; }
        public int LanguageId { get; set; }

        #endregion [Language] 1 to 1 relationship
    }
}