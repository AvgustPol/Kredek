using Kredek.Data;
using Kredek.Data.Models;
using Kredek.Data.Models.ContentElementTranslationTemplates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kredek.Areas.CMS.Pages.ContentManagement.Create
{
    public class TestCreate : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public ContentElement ContentElement { get; set; }

        [BindProperty]
        public ImageAndTextLeft ImageAndTextLeft { get; set; }

        [BindProperty]
        public TextSeparatedByLine TextSeparatedByLineModel { get; set; }

        public AvailableTemplates Type { get; set; }

        [BindProperty]
        public int WebsitePageId { get; set; }

        public TestCreate(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id, AvailableTemplates type)
        {
            Type = type;
            WebsitePageId = id;

            ViewData["Languages"] = new SelectList(_context.Languages, "LanguageId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var page = await _context.WebsitePages.FindAsync(id);

            #region Create a new ContentElement

            ContentElement.WebsitePageId = id;
            ContentElement.WebsitePage = page;
            _context.ContentElement.Add(ContentElement);

            #endregion Create a new ContentElement

            CreateElementTranslation();

            await _context.SaveChangesAsync();

            return RedirectToPage("/ContentManagement/Index", new { pageName = page.Name });
        }

        private void CreateElementTranslation()
        {
            var createTemplate = new Dictionary<AvailableTemplates, Action> {
                { AvailableTemplates.ImageAndTextLeft, () => CreateImageAndTextLeft(ImageAndTextLeft) },
                { AvailableTemplates.TextSeparatedByLine , () => CreateTextSeparatedByLine(TextSeparatedByLineModel) },
            };

            createTemplate[Type]();
        }

        private void CreateImageAndTextLeft(ImageAndTextLeft element)
        {
            _context.TemplatesImageAndTextLeft.Add(element);

            element.ContentElement = ContentElement;
        }

        private void CreateTextSeparatedByLine(TextSeparatedByLine element)
        {
            _context.TemplatesTextSeparatedByLine.Add(element);

            element.ContentElement = ContentElement;
        }
    }
}