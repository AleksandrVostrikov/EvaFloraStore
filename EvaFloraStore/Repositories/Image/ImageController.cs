using EvaFloraStore.Models;
using Microsoft.AspNetCore.Hosting;

namespace EvaFloraStore.Repositories.Image
{
    public class ImageController : IImageController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public void DeleteImage(string imageUrl)
        {
            if (File.Exists(imageUrl))
            {
                File.Delete(imageUrl);
            }
        }

        public string UpdateImage(string imageUrl, IFormFile image)
        {
            DeleteImage(imageUrl);
            var newImageUrl = UploadImage(image);
            return newImageUrl;
        }

        public string UploadImage(IFormFile image)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
            var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploads, uniqueFileName);
            image.CopyTo(new FileStream(filePath, FileMode.Create));
            return filePath;
        }
    }
}
