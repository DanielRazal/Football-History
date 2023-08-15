namespace Server.Services.Classes
{
    public class UploadPhotos : IUploadPhotos
    {
        private readonly IWebHostEnvironment _environment;

        public UploadPhotos(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<User> UploadFile(User user, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return user;
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_environment.WebRootPath, "Uploads", fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var photoUrl = $"/Uploads/{fileName}";
            user.PhotoUrl = photoUrl;
            return user;
        }

        public async Task DeleteFile(string fileName)
        {
            var uploadsDirectory = Path.Combine(_environment.WebRootPath, "Uploads");
            var filePath = Path.Combine(uploadsDirectory, fileName);

            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
            else return;
        }
    }
}