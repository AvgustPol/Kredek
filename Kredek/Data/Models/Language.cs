namespace Kredek.Data.Models
{
    public class Language
    {
        #region Class properties

        /// <summary>
        /// ISO 639-1 Code
        /// e.g. polish -> pl
        /// https://www.loc.gov/standards/iso639-2/php/code_list.php
        /// </summary>
        public string ISOCode { get; set; }

        public int LanguageId { get; set; }

        /// <summary>
        /// Custom name
        /// </summary>
        public string Name { get; set; }
        
        public string ImagePath { get; set; }

        #endregion Class properties
    }
}