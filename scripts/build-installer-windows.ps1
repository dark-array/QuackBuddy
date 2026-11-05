$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot

Write-Host "==> Publishing QuackBuddy win-x64..."
& (Join-Path $PSScriptRoot "publish-windows.ps1")

$isccCandidates = @(
    "${env:LocalAppData}\Programs\Inno Setup 6\ISCC.exe",
    "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe",
    "${env:ProgramFiles}\Inno Setup 6\ISCC.exe"
)
$iscc = $isccCandidates | Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not $iscc) {
    throw "Inno Setup 6 (ISCC.exe) not found. Install: winget install JRSoftware.InnoSetup"
}

$iss = Join-Path $root "installer\QuackBuddy.iss"
Write-Host "==> Compiling installer with $iscc"
& $iscc $iss
if ($LASTEXITCODE -ne 0) {
    throw "ISCC failed with exit code $LASTEXITCODE"
}

$out = Join-Path $root "artifacts\installer"
Write-Host "Installer ready in $out"
Get-ChildItem $out -Filter "QuackBuddy-Setup-*.exe" | ForEach-Object {
    Write-Host ("  {0} ({1:N1} MB)" -f $_.Name, ($_.Length / 1MB))
}
