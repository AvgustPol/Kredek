using Kredek.Data.Models;
using Kredek.Global;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Kredek.Data.DatabaseSeeding
{
    public class DbInitializer : IDbInitializer
    {
        private const string BlogPageName = "Blog";
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

                    CreateBonusPages();
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

        private void CreateANewLanguage(string name, string isoCode)
        {
            var newLanguage = new Language()
            {
                Name = name,
                ISOCode = isoCode
            };

            _context.Languages.Add(newLanguage);
            _context.SaveChanges();
        }

        private void CreateANewPage(string name, bool isActive = true)
        {
            var newPage = new WebsitePage()
            {
                Name = name,
                IsActive = isActive,
            };

            _context.WebsitePages.Add(newPage);
            _context.SaveChanges();
        }

        private void CreateBlogPage()
        {
            CreateANewPage(BlogPageName);
        }

        private void CreateBonusPages()
        {
            CreateBlogPage();
        }

        private void CreateDefaultLanguages()
        {
            CreatePolish();
            CreateEnglish();
        }

        private void CreateDefaultPage()
        {
            CreateANewPage(GlobalVariables.HomePageName, GlobalVariables.HomePageIsActive);
        }

        private void CreateEnglish()
        {
            CreateANewLanguage(GlobalVariables.EnglishLanguageName, GlobalVariables.EnglishLanguageIsoCode);
        }

        private void CreatePolish()
        {
            CreateANewLanguage(GlobalVariables.PolishLanguageName, GlobalVariables.PolishLanguageIsoCode);
        }
    }
}