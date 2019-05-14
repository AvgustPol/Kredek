namespace Kredek.Data.DatabaseSeeding
{
    public interface IKredekInitializer
    {
        void CreateDefaultLanguages();

        void CreateDefaultPages();

        void CreateDefaultPagesTranslations();
    }
}