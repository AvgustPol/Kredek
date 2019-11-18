
using Kredek.Data.Models.ContentElementTranslationTemplates;
using Kredek.Models.Common.Emuns.Inputs;

namespace Kredek.Data.ViewModels
{
    public class BooViewModel : ContentElementTranslation
    {
        public BaseViewModel HugeTextBlock { get; set; } = new BaseViewModel()
        {
            InputName = "A lot of text",
            InputType = InputType.TextBox
        };
    }
}
