# Contributing

Thanks for helping QuackBuddy stay a tiny, useful rubber-duck companion.

## Ground rules

- Keep the app **lightweight** (no WebView, no game loop, no heavy media stacks).
- Prefer small, focused pull requests.
- Match existing C# / Avalonia style: clear names, no unnecessary abstractions.
- Do not commit secrets, personal paths, or large unpublished art without license notes.

## Setup

1. Install the .NET 8 SDK.
2. `dotnet restore QuackBuddy.sln`
3. `dotnet run --project src/QuackBuddy/QuackBuddy.csproj`

## Pull requests

- Describe the user-visible change.
- Note platform testing (Windows / Linux / macOS).
- Update docs when behavior or build steps change.

## Assets

Placeholder art lives in `assets/`. If you contribute final artwork, document the license in `assets/README.md`.
