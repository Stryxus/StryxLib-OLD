namespace StryxLib.NET.Machine.System;

internal class OperatingSystem
{
    public static KernelType Type { get; private set; }

    static OperatingSystem()
    {
        Type = Environment.OSVersion.Platform != PlatformID.Win32NT ?
               Environment.OSVersion.Platform != PlatformID.Unix ?
               Environment.OSVersion.Platform != PlatformID.MacOSX ?
               Runtime.IsWASM ? KernelType.WASM : throw new InvalidDataException("Unable to determine Kernel of current operating system.")
               : KernelType.XNU : KernelType.Linux : KernelType.NT;
    }

    public enum KernelType
    {
        NT,
        Linux,
        XNU,
        WASM
    }
}
