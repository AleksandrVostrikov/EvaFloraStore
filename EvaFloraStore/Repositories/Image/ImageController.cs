using EvaFloraStore.Models;
using Microsoft.AspNetCore.Hosting;
using System.Security.Policy;

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
            if (imageUrl != null)
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, imageUrl);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
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
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            return Path.Combine("uploads", uniqueFileName);
        }
    }
}
