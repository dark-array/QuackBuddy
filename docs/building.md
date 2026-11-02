# Building

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- On Linux: a working desktop session (X11 recommended for Topmost) and `paplay` or `aplay` for sound

## Debug run

```bash
dotnet restore QuackBuddy.sln
dotnet run --project src/QuackBuddy/QuackBuddy.csproj
```

## Publish (self-contained, trimmed)

From the repo root:

```powershell
# Windows
./scripts/publish-windows.ps1

# Linux
./scripts/publish-linux.sh

# macOS
./scripts/publish-macos.sh
```

Artifacts land in `artifacts/<rid>/`.

### Manual publish examples

```bash
dotnet publish src/QuackBuddy/QuackBuddy.csproj -c Release -r win-x64 --self-contained true \
  -p:PublishSingleFile=true -p:PublishTrimmed=true -o artifacts/win-x64

dotnet publish src/QuackBuddy/QuackBuddy.csproj -c Release -r linux-x64 --self-contained true \
  -p:PublishSingleFile=true -p:PublishTrimmed=true -o artifacts/linux-x64

dotnet publish src/QuackBuddy/QuackBuddy.csproj -c Release -r osx-arm64 --self-contained true \
  -p:PublishSingleFile=true -p:PublishTrimmed=true -o artifacts/osx-arm64
```

## Native AOT (future)

Native AOT can shrink Windows/Linux binaries further. It is not enabled in Phase 1 because Avalonia trimming/AOT needs extra roots; track it as a follow-up.
