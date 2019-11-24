using Kredek.Global;
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
                MigratePendingMigrations();

                TryInitializeKredek();
                
                //TryCreatePreview();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void TryInitializeKredek()
        {
            TryCreateDefaultLanguages();

            TryCreateDefaultPages();

            TryCreateDefaultContent();
        }

        private void TryCreateDefaultPages()
        {
            TryCreateDefaultWebsitePages();
            TryCreateDefaultWebsitePagesTranslations();
        }

        private void TryCreateDefaultContent()
        {
            TryCreateDefaultContentElements();
            TryCreateDefaultContentElementsTranslations();
        }

        private void TryCreateDefaultContentElementsTranslations()
        {
            if (!_context.ContentElementTranslation.Any())
            {
                _kredekInitializer.CreateDefaultContentElementsTranslations();
            }
        }

        private void TryCreateDefaultContentElements()
        {
            if (!_context.ContentElements.Any())
            {
                _kredekInitializer.CreateDefaultContentElements();
            }
        }

        private void TryCreateDefaultWebsitePagesTranslations()
        {
            if (!_context.WebsitePageTranslations.Any())
            {
                _kredekInitializer.CreateDefaultPagesTranslations();
            }
        }

        private void TryCreateDefaultWebsitePages()
        {
            if (!_context.WebsitePages.Any())
            {
                _kredekInitializer.CreateDefaultPages();
            }
        }

        private void TryCreateDefaultLanguages()
        {
            if (!_context.Languages.Any())
            {
                _kredekInitializer.CreateDefaultLanguages();
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
            else if (_context.WebsitePages.FirstOrDefault(p => p.Name == DefaultVariables.PreviewPage) is null)
            {
                _previewInitializer.CreatePreview();
            }
        }
    }
}