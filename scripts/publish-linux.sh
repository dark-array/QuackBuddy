#!/usr/bin/env bash
set -euo pipefail
ROOT="$(cd "$(dirname "$0")/.." && pwd)"
OUT="$ROOT/artifacts/linux-x64"

dotnet publish "$ROOT/src/QuackBuddy/QuackBuddy.csproj" -c Release -r linux-x64 --self-contained true \
  -p:PublishSingleFile=true \
  -p:PublishTrimmed=true \
  -p:IncludeNativeLibrariesForSelfExtract=true \
  -o "$OUT"

echo "Published to $OUT"
