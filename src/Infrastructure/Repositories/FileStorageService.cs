

using Application.Interfaces.UnitOfWorkInterfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Repositories;

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;

    public FileStorageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> SaveImageAsync(IFormFile file)
    {
        try
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            
            if(extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".gif" && extension != ".webp")
            {
                throw new FormatException("Unsupported file format");
            }
            var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return "/uploads/" + fileName;
        }
        catch (Exception ex)
        {
            // Unsupported file format
            if(ex is FormatException)
            {
                throw new Exception("Unsupported file format. Please upload an image file.");
            }
            throw new Exception(
           $"UPLOAD ERROR | Root: {_env.WebRootPath} | ContentRoot: {_env.ContentRootPath} | MSG: {ex.Message}");
        }
        }
}
