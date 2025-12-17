

using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.UnitOfWorkInterfaces;

public interface IFileStorageService
{
    Task<string> SaveImageAsync(IFormFile file);
}
