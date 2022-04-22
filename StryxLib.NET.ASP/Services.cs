using Microsoft.Extensions.Configuration;

public static class Services
{
    public static IConfiguration? Configuration { get; private set; }
    public static IServiceProvider? Provider { get; private set; }
    public static void SetConfiguration(IConfiguration configuration) => Configuration ??= configuration;
    public static void SetServiceProvider(IServiceProvider provider) => Provider ??= provider;
    public static void Get<T>(out T? service)
    {
        object? v = Provider?.GetService(typeof(T));
        service = v is not null ? (T)v : throw new NullReferenceException($"The requested service of type {typeof(T).Name} does not exist!");
    }
    public static T? Get<T>()
    {
        object? v = Provider?.GetService(typeof(T));
        return v is not null ? (T)v : throw new NullReferenceException($"The requested service of type {typeof(T).Name} does not exist!");
    }
}
