namespace Kredek.Data.DatabaseSeeding
{
    public interface IKredekInitializer
    {
        void CreateDefaultContentElements();

        void CreateDefaultContentElementsTranslations();

        void CreateDefaultLanguages();

        void CreateDefaultPages();

        void CreateDefaultPagesTranslations();
    }
}