param (
    [string]$Version = "6.0.0"
)

Write-Host "Publishing release bundle for version $Version..." -ForegroundColor Cyan
& ./scripts/package-release.ps1 -Version $Version
