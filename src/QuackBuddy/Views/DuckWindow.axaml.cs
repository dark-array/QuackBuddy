using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using QuackBuddy.Helpers;

namespace QuackBuddy.Views;

public partial class DuckWindow : Window
{
    private Bitmap? _eyesOpen;
    private Bitmap? _eyesClosed;

    public DuckWindow()
    {
        InitializeComponent();
        WindowChromeHelper.ApplyOverlayChrome(this);
        Opened += OnOpened;
        Closed += OnClosed;
    }

    private void OnOpened(object? sender, EventArgs e)
    {
        LoadSprites();
        PositionNearBottomRight();
    }

    private void OnClosed(object? sender, EventArgs e)
    {
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

    private void PositionNearBottomRight()
    {
        var screen = Screens.Primary ?? Screens.All[0];
        var area = screen.WorkingArea;
        const int margin = 24;
        Position = new PixelPoint(
            area.X + area.Width - (int)Width - margin,
            area.Y + area.Height - (int)Height - margin);
    }
}
