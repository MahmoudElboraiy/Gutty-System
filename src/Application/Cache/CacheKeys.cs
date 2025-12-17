
namespace Application.Cache;

public static class CacheKeys
{
    public const string Plans = "plans:all";
    public static string PlanById(string planId) => $"plan:{planId}";
}
