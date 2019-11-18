using Kredek.Data.Models.ContentElementTranslationTemplates;
using Kredek.Models.Common.Emuns.Inputs;

namespace Kredek.Data.ViewModels
{
    public class BaseViewModel
    {
        public InputType InputType { get; set; }
        public ContentElementTranslation Component { get; set; }
    }
}
