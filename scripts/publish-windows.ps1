$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$project = Join-Path $root "src\QuackBuddy\QuackBuddy.csproj"
$out = Join-Path $root "artifacts\win-x64"

dotnet publish $project -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true `
  -p:PublishTrimmed=true `
  -p:DebugType=None `
  -p:DebugSymbols=false `
  -p:IncludeNativeLibrariesForSelfExtract=true `
  -o $out

Write-Host "Published to $out"
