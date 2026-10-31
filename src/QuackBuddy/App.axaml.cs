using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using QuackBuddy.Views;

namespace QuackBuddy;

public partial class App : Application
{
    private DuckWindow? _duckWindow;

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            _duckWindow = new DuckWindow();
            desktop.MainWindow = _duckWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void OnTrayClicked(object? sender, EventArgs e) => ToggleDuckVisibility();

    private void OnTrayToggleClick(object? sender, EventArgs e) => ToggleDuckVisibility();

    private void OnTrayQuackClick(object? sender, EventArgs e) => _duckWindow?.PlayQuack();

    private void OnTrayQuitClick(object? sender, EventArgs e)
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.Shutdown();
    }

    private void ToggleDuckVisibility()
    {
        if (_duckWindow is null)
            return;

        if (_duckWindow.IsVisible)
            _duckWindow.Hide();
        else
            _duckWindow.Show();
    }
}
