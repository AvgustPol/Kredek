using Kredek.Global;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Kredek.Data.DatabaseSeeding
{
    public class DbInitializer : IDbInitializer
    {
        private const int MINIMAL_NUMBER_OF_PAGES = 1;

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
                MigratePendingMigrations();

                

                if (!_context.Languages.Any())
                {
                    CreateDefaultLanguages();
                }

                TryCreatePreview();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void MigratePendingMigrations()
        {
            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }
        }

        private void TryCreatePreview()
        {
            if (!_context.WebsitePages.Any())
            {
                _previewInitializer.CreatePreview();
            }
            else if (_context.WebsitePages.Single(p => p.Name == DefaultVariables.PreviewPage) is null)
            {
                _previewInitializer.CreatePreview();
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