using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kredek.Data;
using Kredek.Data.Models.ContentElementTranslationTemplates;

namespace Kredek.Areas.CMS.Pages.ContentElementTranslationManagement.ImageAndTextLeftManagement
{
    public class CreateModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public CreateModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ContentElementId"] = new SelectList(_context.ContentElement, "ContentElementId", "ContentElementId");
        ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId");
            return Page();
        }

        [BindProperty]
        public ImageAndTextLeft ImageAndTextLeft { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TemplatesImageAndTextLeft.Add(ImageAndTextLeft);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}