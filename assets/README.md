# Assets

Replace these placeholders with your final Avatar-style 3D duck renders before a public release.

| File | Purpose | Recommended size |
|------|---------|------------------|
| `duck_eyes_open.png` | Default sprite | 512×512 PNG, transparent background |
| `duck_eyes_closed.png` | Blink frame (same pose, closed eyes) | Same canvas as open |
| `quack.wav` | Click sound | Short mono/stereo WAV (≤1 s) |
| `tray_icon.ico` | System tray / window icon | Multi-resolution ICO (16–256) |

## Conventions

- Keep both duck frames **pixel-aligned** (identical body pose) so blinking looks natural.
- Prefer premultiplied-friendly straight alpha PNGs.
- Do not rename files unless you also update `QuackBuddy.csproj` resource links.
