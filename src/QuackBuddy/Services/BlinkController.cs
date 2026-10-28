using System;
using Avalonia.Threading;

namespace QuackBuddy.Services;

/// <summary>
/// Low-CPU blink loop: idle most of the time, brief closed-eyes pulse.
/// </summary>
public sealed class BlinkController : IDisposable
{
    private readonly DispatcherTimer _idleTimer;
    private readonly DispatcherTimer _blinkTimer;
    private readonly Random _random = new();
    private readonly Action<bool> _setEyesOpen;
    private bool _disposed;

    public BlinkController(Action<bool> setEyesOpen)
    {
        _setEyesOpen = setEyesOpen ?? throw new ArgumentNullException(nameof(setEyesOpen));

        _idleTimer = new DispatcherTimer { Interval = NextIdleInterval() };
        _idleTimer.Tick += OnIdleTick;

        _blinkTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(120) };
        _blinkTimer.Tick += OnBlinkTick;
    }

    public void Start()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        _setEyesOpen(true);
        _idleTimer.Start();
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;
        _idleTimer.Stop();
        _blinkTimer.Stop();
        _idleTimer.Tick -= OnIdleTick;
        _blinkTimer.Tick -= OnBlinkTick;
    }

    private void OnIdleTick(object? sender, EventArgs e)
    {
        _idleTimer.Stop();
        _setEyesOpen(false);
        _blinkTimer.Start();
    }

    private void OnBlinkTick(object? sender, EventArgs e)
    {
        _blinkTimer.Stop();
        _setEyesOpen(true);
        _idleTimer.Interval = NextIdleInterval();
        _idleTimer.Start();
    }

    private TimeSpan NextIdleInterval()
    {
        // 3–6 seconds between blinks with jitter.
        var ms = 3000 + _random.Next(0, 3001);
        return TimeSpan.FromMilliseconds(ms);
    }
}
