namespace Kredek.Data.Models.ContentElementTranslationTemplates
{
    /// <summary>
    /// Object that represents "Frequently asked question" element
    /// </summary>
    public class FaqElement : ContentElementTranslation
    {
        public string Answer { get; set; }
        public string Question { get; set; }
    }
}