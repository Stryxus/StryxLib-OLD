using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace StryxLib.NET;

public static class Runtime
{
    public static Assembly? RuntimeAssembly { get; } = Assembly.GetCallingAssembly();
    public static Assembly? LibraryAssembly { get; set; } = Assembly.GetAssembly(typeof(Runtime));

    public static bool IsMono { get; } = RuntimeInformation.FrameworkDescription.Contains("Mono");
    public static bool IsWASM { get; } = RuntimeInformation.OSDescription.Contains("Browser");
#if DEBUG
    public static bool IsDevelopmentMode { get; } = true;
#else
    public static bool IsDevelopmentMode { get; } = false;
#endif

    public static DateTime RuntimeBuildDate = GetBuildDate(RuntimeAssembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>());

    public static DateTime LibraryBuildDate
    {
        get
        {
            LibraryAssembly = Assembly.GetAssembly(typeof(Runtime));
            return GetBuildDate(LibraryAssembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>());
        }
    }

    public static long RuntimeVersion { get; } = long.Parse($"{RuntimeBuildDate.Year}{RuntimeBuildDate.Month}{RuntimeBuildDate.Day}{RuntimeBuildDate.Hour}{RuntimeBuildDate.Minute}{RuntimeBuildDate.Second}");

    public static long LibraryVersion
    {
        get
        {
            LibraryAssembly = Assembly.GetAssembly(typeof(Runtime));
            return long.Parse($"{LibraryBuildDate.Year}{LibraryBuildDate.Month}{LibraryBuildDate.Day}{LibraryBuildDate.Hour}{LibraryBuildDate.Minute}{LibraryBuildDate.Second}");
        }
    }

    public static DateTime GetBuildDate(AssemblyInformationalVersionAttribute? attribute)
    {
        string BuildVersionMetadataPrefix = "+build";
        if (attribute?.InformationalVersion != null)
        {
            string value = attribute.InformationalVersion;
            int index = value.IndexOf(BuildVersionMetadataPrefix);
            if (index > 0 && DateTime.TryParseExact(value[(index + BuildVersionMetadataPrefix.Length)..], "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result)) return result;
        }
        return default;
    }
}
