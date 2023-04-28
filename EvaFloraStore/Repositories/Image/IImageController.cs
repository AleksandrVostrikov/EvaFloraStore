namespace EvaFloraStore.Repositories.Image
{
    public interface IImageController
    {
        string UploadImage(IFormFile image);
        void DeleteImage(string imageUrl);
        string UpdateImage(string imageUrl, IFormFile image);
    }
}
