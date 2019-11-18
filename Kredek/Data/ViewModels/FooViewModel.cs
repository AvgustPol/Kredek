using Kredek.Data.Models.ContentElementTranslationTemplates;
using Kredek.Models.Common.Emuns.Inputs;

namespace Kredek.Data.ViewModels
{
    public class FooViewModel : ContentElementTranslation
    {
        public BaseViewModel DisplayName { get; set; } = new BaseViewModel()
        {
            InputType = InputType.OneLine,
            InputName = "Display name"
        };

        public BaseViewModel Position { get; set; } = new BaseViewModel()
        {
            InputType = InputType.OneLine,
            InputName = "Position in organization"
        };
    }
}
