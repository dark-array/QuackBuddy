# Architecture

QuackBuddy is a tiny Avalonia desktop overlay: one transparent always-on-top window, two sprites, one sound.

```
App
 ├─ TrayIcon (show/hide, quack, quit)
 └─ DuckWindow
     ├─ WindowChromeHelper  → Topmost + transparency hints
     ├─ BlinkController     → sparse DispatcherTimer blink
     └─ SoundService        → embedded WAV playback per OS
```

## Design goals

- **Low CPU:** no render loop; blink timers fire a few times per minute.
- **Low memory:** two bitmaps + one short WAV; no WebView, no media engine.
- **Cross-platform:** single .NET 8 codebase; publish per RID.

## Platform notes

| OS | Overlay | Audio |
|----|---------|-------|
| Windows | Transparent + Topmost (supported) | `winmm` `PlaySound` |
| macOS | Transparent floating window | `afplay` |
| Linux X11 | Usually OK | `paplay` / `aplay` / `ffplay` |
| Linux Wayland | Compositor-dependent; may ignore Topmost | same as X11 |

## Extension points (later phases)

- Persist window position
- Multiple skins / asset packs
- Global hotkey to toggle visibility
- Optional click-through outside the duck silhouette
