using Kredek.Data.Models;
using Kredek.Data.Models.ContentElementTranslationTemplates;
using Microsoft.EntityFrameworkCore;

namespace Kredek.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TextSeparatedByLine> TextSeparatedByLine { get; set; }
        public DbSet<ImageAndTextLeft> ImageAndTextLeft { get; set; }

        public DbSet<ContentElement> ContentElements { get; set; }
        public DbSet<ContentElementTranslation> ContentElementTranslation { get; set; }
        public DbSet<Language> Languages { get; set; }

        public DbSet<WebsitePage> WebsitePages { get; set; }

        public DbSet<WebsitePageTranslation> WebsitePageTranslations { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}