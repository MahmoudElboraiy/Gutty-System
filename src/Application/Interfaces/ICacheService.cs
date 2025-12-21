
namespace Application.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetOrCreateAsync<T>(string baseKey, string versionKey, string parametersKey, Func<Task<T>> factory, TimeSpan? expiration = null);
        void IncrementVersion(string versionKey);
        int GetVersion(string versionKey);
    }
}
