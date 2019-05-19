using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Kredek.Data.DatabaseSeeding
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IKredekInitializer _kredekInitializer;
        private readonly IPreviewInitializer _previewInitializer;

        public DbInitializer(ApplicationDbContext context, IPreviewInitializer previewInitializer, IKredekInitializer kredekInitializer)
        {
            _context = context;
            _previewInitializer = previewInitializer;
            _kredekInitializer = kredekInitializer;
        }

        public void SeedDatabase()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }

                if (!_context.WebsitePages.Any())
                {
                    CreateDefaultPages();
                }

                #region First time [Kredek] database creating

                if (!_context.Languages.Any())
                {
                    CreateDefaultLanguages();
                }

                if (_context.WebsitePages.Count() < 2)
                {
                    _previewInitializer.CreatePreview();
                }

                if (!_context.WebsitePageTranslations.Any())
                {
                    CreateWebsitePageTranslations();
                }

                if (!_context.ContentElement.Any())
                {
                    CreateWebsiteContentElements();
                }

                if (!_context.ContentElementTranslation.Any())
                {
                    CreateWebsiteContentElementsTranslations();
                }

                #endregion First time [Kredek] database creating
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void CreateDefaultLanguages()
        {
            _kredekInitializer.CreateDefaultLanguages();
        }

        private void CreateDefaultPages()
        {
            _kredekInitializer.CreateDefaultPages();
        }

        private void CreateWebsiteContentElements()
        {
            _kredekInitializer.CreateDefaultContentElements();
        }

        private void CreateWebsiteContentElementsTranslations()
        {
            _kredekInitializer.CreateDefaultContentElementsTranslations();
        }

        private void CreateWebsitePageTranslations()
        {
            _kredekInitializer.CreateDefaultPagesTranslations();
        }
    }
}