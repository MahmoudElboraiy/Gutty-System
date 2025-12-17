

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
 
        var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");

        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(uploadsPath, fileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        return "/uploads/" + fileName;
    }
}
