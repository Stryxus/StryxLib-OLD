using System.Reflection;

namespace StryxLib.NET.ASP;

public static class Runtime
{
    public static DateTime LibraryBuildDate
    {
        get
        {
            NET.Runtime.LibraryAssembly = Assembly.GetAssembly(typeof(Runtime));
            return NET.Runtime.GetBuildDate(NET.Runtime.LibraryAssembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>());
        }
    }

    public static long LibraryVersion
    {
        get
        {
            NET.Runtime.LibraryAssembly = Assembly.GetAssembly(typeof(Runtime));
            return long.Parse($"{LibraryBuildDate.Year}{LibraryBuildDate.Month}{LibraryBuildDate.Day}{LibraryBuildDate.Hour}{LibraryBuildDate.Minute}{LibraryBuildDate.Second}");
        }
    }
}
