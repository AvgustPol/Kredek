﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kredek.Data;
using Kredek.Data.Models.ContentElementTranslationTemplates;

namespace Kredek.Areas.CMS.Pages.ContentElementTranslationManagement.ImageAndTextLeftManagement
{
    public class EditModel : PageModel
    {
        private readonly Kredek.Data.ApplicationDbContext _context;

        public EditModel(Kredek.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ImageAndTextLeft ImageAndTextLeft { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ImageAndTextLeft = await _context.TemplatesImageAndTextLeft
                .Include(i => i.ContentElement)
                .Include(i => i.Language).FirstOrDefaultAsync(m => m.ContentElementTranslationId == id);

            if (ImageAndTextLeft == null)
            {
                return NotFound();
            }
           ViewData["ContentElementId"] = new SelectList(_context.ContentElement, "ContentElementId", "ContentElementId");
           ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ImageAndTextLeft).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageAndTextLeftExists(ImageAndTextLeft.ContentElementTranslationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ImageAndTextLeftExists(int id)
        {
            return _context.TemplatesImageAndTextLeft.Any(e => e.ContentElementTranslationId == id);
        }
    }
}