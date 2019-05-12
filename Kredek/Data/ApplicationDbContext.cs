using Kredek.Data.Models;
using Kredek.Data.Models.ContentElementTranslationTemplates;
using Microsoft.EntityFrameworkCore;

namespace Kredek.Data
{
    public class ApplicationDbContext : DbContext
    {
        #region Templates

        public DbSet<ImageAndTextLeft> TemplatesImageAndTextLeft { get; set; }
        public DbSet<ImageAndTextRight> TemplatesImageAndTextRight { get; set; }
        public DbSet<TextSeparatedByLine> TemplatesTextSeparatedByLine { get; set; }

        #endregion Templates

        public DbSet<ContentElement> ContentElement { get; set; }
        public DbSet<ContentElementTranslation> ContentElementTranslation { get; set; }
        public DbSet<Language> Languages { get; set; }

        public DbSet<WebsitePage> WebsitePages { get; set; }

        public DbSet<WebsitePageTranslation> WebsitePageTranslations { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}