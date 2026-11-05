# Releasing

1. Bump `<Version>` in `src/QuackBuddy/QuackBuddy.csproj` and `installer/QuackBuddy.iss`.
2. Update the README changelog / release notes.
3. Replace placeholder assets if shipping branded art (see `assets/README.md`).
4. Run CI locally: `dotnet build QuackBuddy.sln -c Release`.
5. Publish all RIDs via `scripts/`.
6. Windows installer: `./scripts/build-installer-windows.ps1` (requires [Inno Setup 6](https://jrsoftware.org/isinfo.php)).
7. Tag `vMAJOR.MINOR.PATCH` and attach portable zips + `QuackBuddy-Setup-*.exe` to the GitHub Release.

| Artifact | Platform |
|----------|----------|
| `artifacts/win-x64/` | Portable Windows |
| `artifacts/installer/QuackBuddy-Setup-*.exe` | Windows installer (Inno Setup) |
| `artifacts/linux-x64/` | Portable Linux |
| `artifacts/osx-arm64/`, `artifacts/osx-x64/` | Portable macOS |

DMG / AppImage installers are still future work.
