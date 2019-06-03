using Kredek.Data;
using Kredek.Data.Models;
using Kredek.Data.Models.ContentElementTranslationTemplates;
using Kredek.Logic;
using Microsoft.AspNetCore.Http;
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
        private readonly IImageSavingService _imageSavingService;

        [BindProperty]
        public ContentElement ContentElement { get; set; }

        [BindProperty]
        public IFormFile DownloadedImage { get; set; }

        [BindProperty]
        public FaqElement FaqElement { get; set; }

        [BindProperty]
        public ImageAndTextLeft ImageAndTextLeft { get; set; }

        [BindProperty]
        public TextSeparatedByLine TextSeparatedByLineModel { get; set; }

        [BindProperty]
        public AvailableTemplates Type { get; set; }

        [BindProperty]
        public int WebsitePageId { get; set; }

        public TestCreate(ApplicationDbContext context, IImageSavingService imageSavingService)
        {
            _context = context;
            _imageSavingService = imageSavingService;
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
            WebsitePageId = id;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var page = await _context.WebsitePages.FindAsync(WebsitePageId);

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
                { AvailableTemplates.FaqElement , () => CreateFaqElement(FaqElement) },
            };

            createTemplate[Type]();
        }

        private void CreateFaqElement(FaqElement element)
        {
            element.ContentElement = ContentElement;

            _context.FaqElements.Add(element);
        }

        private void CreateImageAndTextLeft(ImageAndTextLeft element)
        {
            element.ContentElement = ContentElement;

            element.ImageUrl = _imageSavingService.SaveImage(DownloadedImage, WebsitePageId);

            _context.TemplatesImageAndTextLeft.Add(element);
        }

        private void CreateTextSeparatedByLine(TextSeparatedByLine element)
        {
            element.ContentElement = ContentElement;

            element.ImageUrl = _imageSavingService.SaveImage(DownloadedImage, WebsitePageId);

            _context.TemplatesTextSeparatedByLine.Add(element);
        }
    }
}