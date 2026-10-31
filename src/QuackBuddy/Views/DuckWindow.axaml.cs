using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using QuackBuddy.Helpers;
using QuackBuddy.Services;

namespace QuackBuddy.Views;

public partial class DuckWindow : Window
{
    private const double DragThreshold = 4.0;

    private readonly ISoundService _soundService = new SoundService();
    private readonly BlinkController _blinkController;

    private Bitmap? _eyesOpen;
    private Bitmap? _eyesClosed;

    private bool _pointerDown;
    private bool _dragging;
    private Point _pressPoint;

    public DuckWindow()
    {
        InitializeComponent();
        WindowChromeHelper.ApplyOverlayChrome(this);

        _blinkController = new BlinkController(SetEyesOpen);
        Opened += OnOpened;
        Closed += OnClosed;
    }

    public void PlayQuack() => _soundService.PlayQuack();

    private void OnOpened(object? sender, EventArgs e)
    {
        LoadSprites();
        PositionNearBottomRight();
        _blinkController.Start();
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        _blinkController.Dispose();
        _soundService.Dispose();
        _eyesOpen?.Dispose();
        _eyesClosed?.Dispose();
    }

    private void LoadSprites()
    {
        _eyesOpen = LoadBitmap("avares://QuackBuddy/Assets/duck_eyes_open.png");
        _eyesClosed = LoadBitmap("avares://QuackBuddy/Assets/duck_eyes_closed.png");
        DuckImage.Source = _eyesOpen;
    }

    private static Bitmap LoadBitmap(string uri) =>
        new(AssetLoader.Open(new Uri(uri)));

    private void SetEyesOpen(bool open)
    {
        if (_eyesOpen is null || _eyesClosed is null)
            return;

        DuckImage.Source = open ? _eyesOpen : _eyesClosed;
    }

    private void PositionNearBottomRight()
    {
        var screen = Screens.Primary ?? Screens.All[0];
        var area = screen.WorkingArea;
        const int margin = 24;
        Position = new PixelPoint(
            area.X + area.Width - (int)Width - margin,
            area.Y + area.Height - (int)Height - margin);
    }

    private void OnDuckPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            return;

        _pointerDown = true;
        _dragging = false;
        _pressPoint = e.GetPosition(this);
        e.Pointer.Capture(DuckImage);
    }

    private void OnDuckPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_pointerDown)
            return;

        var current = e.GetPosition(this);
        var delta = current - _pressPoint;

        if (!_dragging && (Math.Abs(delta.X) + Math.Abs(delta.Y)) >= DragThreshold)
            _dragging = true;

        if (!_dragging)
            return;

        var scaling = DesktopScaling;
        Position = new PixelPoint(
            Position.X + (int)(delta.X * scaling),
            Position.Y + (int)(delta.Y * scaling));
    }

    private void OnDuckPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (!_pointerDown)
            return;

        e.Pointer.Capture(null);
        var wasDragging = _dragging;
        _pointerDown = false;
        _dragging = false;

        if (!wasDragging && e.InitialPressMouseButton == MouseButton.Left)
            PlayQuack();
    }
}
