namespace Server.Services.Interfaces
{
    public interface IUploadPhotos
    {
        Task<User> UploadFile(User user, IFormFile file);
        Task DeleteFile(string fileName);
    }
}