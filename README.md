# QuackBuddy

A lightweight, always-on-top **rubber duck debugging** companion for Windows, Linux, and macOS.

Explain your bug out loud to the duck. Click it to hear a quack. It blinks while it listens.

Built with **.NET 8 + Avalonia** — no Electron, no WebView, no game loop.

## Features (Phase 1)

- Transparent borderless overlay that stays on top of other apps
- Eyes-open / eyes-closed blink (low CPU timers)
- Click the duck → quack sound
- Drag to reposition
- System tray: show/hide, quack, quit

## Quick start

```bash
dotnet restore QuackBuddy.sln
dotnet run --project src/QuackBuddy/QuackBuddy.csproj
```

Published binaries: see [docs/building.md](docs/building.md).

## Rubber duck debugging?

[Rubber duck debugging](https://en.wikipedia.org/wiki/Rubber_duck_debugging) is the practice of explaining code line-by-line to an inanimate duck (or colleague). Articulating the problem often reveals the fix — QuackBuddy is that duck, living on your desktop.

## Assets

Phase 1 ships **placeholder** sprites and a generated quack WAV. Swap in your Avatar-style 3D duck renders using the names in [assets/README.md](assets/README.md).

## Platform notes

- **Windows:** full transparent topmost support.
- **macOS:** floating transparent window.
- **Linux:** X11 is the reliable path for always-on-top; Wayland depends on the compositor.

Details: [docs/architecture.md](docs/architecture.md).

## Roadmap (later)

- Remember window position
- Skin packs / multiple ducks
- Global hotkey toggle
- Optional click-through outside the silhouette
- Native AOT publish experiments
- Installers (MSI / DMG / AppImage)

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) and [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md).

## License

[MIT](LICENSE)
