# Releasing

1. Bump `<Version>` in `src/QuackBuddy/QuackBuddy.csproj`.
2. Update the README changelog / release notes.
3. Replace placeholder assets if shipping branded art (see `assets/README.md`).
4. Run CI locally: `dotnet build QuackBuddy.sln -c Release`.
5. Publish all RIDs via `scripts/`.
6. Tag `vMAJOR.MINOR.PATCH` and attach the `artifacts/` zips to the GitHub Release.

Phase 1 ships **portable** binaries only (no MSI/DMG/AppImage yet).
