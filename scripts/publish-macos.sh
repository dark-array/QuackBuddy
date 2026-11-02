#!/usr/bin/env bash
set -euo pipefail
ROOT="$(cd "$(dirname "$0")/.." && pwd)"

publish_rid() {
  local rid="$1"
  local out="$ROOT/artifacts/$rid"
  dotnet publish "$ROOT/src/QuackBuddy/QuackBuddy.csproj" -c Release -r "$rid" --self-contained true \
    -p:PublishSingleFile=true \
    -p:PublishTrimmed=true \
    -p:IncludeNativeLibrariesForSelfExtract=true \
    -o "$out"
  echo "Published to $out"
}

publish_rid osx-arm64
publish_rid osx-x64
