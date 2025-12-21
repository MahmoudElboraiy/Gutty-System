

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

            var uploadsFolder = "/var/www/Gutty-System/src/Presentation/wwwroot/uploads";

           // var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
            Console.WriteLine("WebRootPath: " + _env.WebRootPath);


            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return "/uploads/" + fileName;
        }
        catch (Exception ex)
        {       
            throw new Exception(
           $"UPLOAD ERROR | Root: {_env.WebRootPath} | ContentRoot: {_env.ContentRootPath} | MSG: {ex.Message}");
        }
        }
}
