using System;
using System.Runtime.InteropServices;
using Avalonia.Controls;

namespace QuackBuddy.Helpers;

/// <summary>
/// Applies platform-specific chrome tweaks for a floating overlay window.
/// Wayland compositors may ignore always-on-top or transparency requests;
/// prefer an X11 session on Linux when the duck must stay above other apps.
/// </summary>
public static class WindowChromeHelper
{
    public static void ApplyOverlayChrome(Window window)
    {
        window.SystemDecorations = SystemDecorations.None;
        window.CanResize = false;
        window.ShowInTaskbar = false;
        window.Topmost = true;
        window.TransparencyLevelHint = new[]
        {
            WindowTransparencyLevel.Transparent,
            WindowTransparencyLevel.None
        };
        window.Background = Avalonia.Media.Brushes.Transparent;

        if (OperatingSystem.IsWindows())
        {
            window.ExtendClientAreaToDecorationsHint = true;
            window.ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.NoChrome;
            window.ExtendClientAreaTitleBarHeightHint = -1;
        }
    }

    public static string PlatformNotes()
    {
        if (OperatingSystem.IsWindows())
            return "Windows: layered transparent topmost overlay is fully supported.";

        if (OperatingSystem.IsMacOS())
            return "macOS: floating transparent window; keep Accessibility/Screen Recording permissions unused (not required for overlay).";

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return "Linux: X11 usually honors Topmost + transparency. Wayland support depends on the compositor and may keep the window below fullscreen apps.";

        return "Unknown platform.";
    }
}
