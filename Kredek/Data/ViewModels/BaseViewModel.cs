using Kredek.Data.Models.ContentElementTranslationTemplates;
using Kredek.Models.Common.Emuns.Foo;

namespace Kredek.Data.ViewModels
{
    public class BaseViewModel
    {
        public InputType InputType { get; set; }
        public ContentElementTranslation Component { get; set; }
    }
}
