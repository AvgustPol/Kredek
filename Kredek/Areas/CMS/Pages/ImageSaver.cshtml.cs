using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kredek.Areas.CMS.Pages
{
    public class ImageSaverModel : PageModel
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImageSaverModel(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void OnGet()
        {
        }

        public void OnPost(List<IFormFile> files)
        {
            foreach (var img in files)
            {
                if (img != null)
                {
                    //Firstly we need to find the wwwroot path.
                    //To find that we need a Hosting Environment object (to ask "him" about "what is the path of my wwwroot?")
                    //We are using DI (dependency injection) and passing hostingEnvironment via controller
                    string wwwrootPath = _hostingEnvironment.WebRootPath;
                    string originalName = Path.GetFileName(img.FileName);
                    string folderForSavedImages = "SavedImages";

                    var destinationPath = Path.Combine(wwwrootPath, folderForSavedImages, originalName);

                    img.CopyTo(new FileStream(destinationPath, FileMode.Create));
                }
            }
        }
    }
}