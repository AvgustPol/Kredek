using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Kredek.Logic
{
    public class ImageSavingService : IImageSavingService
    {
        private const string ContentFolder = "content";
        private const string ImagesFolder = "img";
        private readonly string _wwwRootFolderServerPath;

        public ImageSavingService(IHostingEnvironment hostingEnvironment)
        {
            _wwwRootFolderServerPath = hostingEnvironment.WebRootPath;
        }

        /// <summary>
        /// Saves image to wwwroot/img/content/{pageId} folder
        /// </summary>
        /// <param name="img"></param>
        /// <param name="pageId"></param>
        /// <returns>Path to static folder ( important: result does not contains path to wwwroot folder!) </returns>
        public string SaveImage(IFormFile img, int pageId)
        {
            if (img != null)
            {
                string folderName = pageId.ToString();
                string destinationFolderPath = Path.Combine(_wwwRootFolderServerPath, ImagesFolder, ContentFolder, folderName);

                if (!Directory.Exists(destinationFolderPath))
                    Directory.CreateDirectory(destinationFolderPath);

                string originalExtension = GetImageType(img);
                string newName = Guid.NewGuid().ToString() + "." + originalExtension;

                var destinationPath = Path.Combine(destinationFolderPath, newName);

                img.CopyTo(new FileStream(destinationPath, FileMode.Create));

                return Path.Combine(ImagesFolder, ContentFolder, folderName, newName);
            }

            return null;
        }

        private string GetImageType(IFormFile img)
        {
            string type;

            //ContentType will return "image/[type]"
            type = img.ContentType.Split('/')[1];

            return type;
        }
    }
}