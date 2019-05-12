using Kredek.Data.Models;
using Kredek.Global;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Kredek.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IPreviewInitializer _previewInitializer;

        public DbInitializer(ApplicationDbContext context, IPreviewInitializer previewInitializer)
        {
            _context = context;
            _previewInitializer = previewInitializer;
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
                    CreateDefaultPage();
                }
                if (!_context.Languages.Any())
                {
                    CreateDefaultLanguages();
                }

                //if it is first time the database was created
                if (_context.WebsitePages.Count() < 2)
                {
                    _previewInitializer.CreatePreview();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void CreateDefaultLanguages()
        {
            CreatePolish();
            CreateEnglish();

            _context.SaveChanges();
        }

        private void CreateDefaultPage()
        {
            var home = new WebsitePage()
            {
                Name = GlobalVariables.HomePageName,
                IsActive = GlobalVariables.HomePageIsActive
            };

            _context.WebsitePages.Add(home);
            _context.SaveChanges();
        }

        private void CreateEnglish()
        {
            var englishLanguage = new Language()
            {
                Name = GlobalVariables.EnglishLanguageName,
                ISOCode = GlobalVariables.EnglishLanguageIsoCode
            };

            _context.Languages.Add(englishLanguage);
        }

        private void CreatePolish()
        {
            var polishLanguage = new Language()
            {
                Name = GlobalVariables.PolishLanguageName,
                ISOCode = GlobalVariables.PolishLanguageIsoCode
            };

            _context.Languages.Add(polishLanguage);
        }
    }
}