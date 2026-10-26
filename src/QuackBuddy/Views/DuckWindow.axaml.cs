using System;
using Avalonia;
using Avalonia.Controls;
using QuackBuddy.Helpers;

namespace QuackBuddy.Views;

public partial class DuckWindow : Window
{
    public DuckWindow()
    {
        InitializeComponent();
        WindowChromeHelper.ApplyOverlayChrome(this);
        Opened += OnOpened;
    }

    private void OnOpened(object? sender, EventArgs e) => PositionNearBottomRight();

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
