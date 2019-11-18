using Kredek.Data.Models.ContentElementTranslationTemplates;
using Kredek.Models.Common.Emuns.Inputs;

namespace Kredek.Data.Models.CreatableModels
{
    //this class should be deleted .
    public class ExampleCreatableModel : BaseCreatableModel
    {
        public CreatableModelProperty ExamplePropertyOneLine { get; set; } = new CreatableModelProperty()
        {
            InputName = "First property - OneLine",
            InputType = InputType.OneLine
        };
        public CreatableModelProperty ExamplePropertyTextArea { get; set; } = new CreatableModelProperty()
        {
            InputName = "Second property - TextBox",
            InputType = InputType.TextBox
        };

        public new Example GetContentElementTranslation()
        {
            return new Example()
            {
                ExamplePropertyOneLine = this.ExamplePropertyOneLine.Property,
                ExamplePropertyTextArea = this.ExamplePropertyTextArea.Property,
            };
        }
    }
}
