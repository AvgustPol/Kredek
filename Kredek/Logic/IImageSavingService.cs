using Microsoft.AspNetCore.Http;

namespace Kredek.Logic
{
    public interface IImageSavingService
    {
        string SaveImage(IFormFile img, int pageId);
    }
}