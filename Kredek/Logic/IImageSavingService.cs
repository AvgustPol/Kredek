using Microsoft.AspNetCore.Http;

namespace Kredek.Logic
{
    public interface IImageSavingService
    {
        /// <summary>
        /// Saves image to static folder on server
        /// </summary>
        /// <param name="img"></param>
        /// <param name="pageId"></param>
        /// <returns>Path to static folder ( important: result does not contains path to wwwroot folder!) </returns>
        string SaveImage(IFormFile img, int pageId);
    }
}