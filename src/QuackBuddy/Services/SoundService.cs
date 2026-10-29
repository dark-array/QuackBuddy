using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace QuackBuddy.Services;

/// <summary>
/// Plays the embedded quack WAV with zero heavy audio engines.
/// Windows uses winmm PlaySound; macOS/Linux shell out to afplay / paplay / aplay.
/// </summary>
public sealed class SoundService : ISoundService
{
    private const string ResourceName = "QuackBuddy.Assets.quack.wav";
    private readonly string _wavPath;
    private readonly object _gate = new();
    private bool _disposed;

    public SoundService()
    {
        _wavPath = ExtractEmbeddedWav();
    }

    public void PlayQuack()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        lock (_gate)
        {
            if (OperatingSystem.IsWindows())
            {
                PlaySound(_wavPath, IntPtr.Zero, SndFilename | SndAsync | SndNoDefault);
                return;
            }

            if (OperatingSystem.IsMacOS())
            {
                StartDetached("afplay", Quote(_wavPath));
                return;
            }

            if (StartDetached("paplay", Quote(_wavPath)) ||
                StartDetached("aplay", Quote(_wavPath)) ||
                StartDetached("ffplay", $"-nodisp -autoexit -loglevel quiet {Quote(_wavPath)}"))
            {
                return;
            }

            Debug.WriteLine("QuackBuddy: no Linux audio player found (paplay/aplay/ffplay).");
        }
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;
        try
        {
            if (File.Exists(_wavPath))
                File.Delete(_wavPath);
        }
        catch (IOException)
        {
            // Best-effort cleanup of temp WAV.
        }
    }

    private static string ExtractEmbeddedWav()
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(ResourceName)
            ?? throw new InvalidOperationException($"Missing embedded resource '{ResourceName}'.");

        var path = Path.Combine(Path.GetTempPath(), $"quackbuddy-{Guid.NewGuid():N}.wav");
        using var file = File.Create(path);
        stream.CopyTo(file);
        return path;
    }

    private static string Quote(string path) => $"\"{path}\"";

    private static bool StartDetached(string fileName, string arguments)
    {
        try
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            });
            return process is not null;
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or FileNotFoundException or InvalidOperationException)
        {
            return false;
        }
    }

    private const uint SndAsync = 0x0001;
    private const uint SndNoDefault = 0x0002;
    private const uint SndFilename = 0x00020000;

    [DllImport("winmm.dll", EntryPoint = "PlaySoundW", CharSet = CharSet.Unicode)]
    private static extern bool PlaySound(string pszSound, IntPtr hmod, uint fdwSound);
}
