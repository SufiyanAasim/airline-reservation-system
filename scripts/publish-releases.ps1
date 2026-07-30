# PowerShell script to automatically create or update GitHub Release assets for all version tags

$ErrorActionPreference = "Continue"

$tags = @(
    @{ Tag = "v1.0.0"; Name = "v1.0.0 - Clearance"; Prerelease = $true },
    @{ Tag = "v2.0.0"; Name = "v2.0.0 - Taxi"; Prerelease = $false },
    @{ Tag = "v3.0.0"; Name = "v3.0.0 - Ascent"; Prerelease = $false },
    @{ Tag = "v4.0.0"; Name = "v4.0.0 - Cruising"; Prerelease = $false },
    @{ Tag = "v5.0.0"; Name = "v5.0.0 - Touchdown"; Prerelease = $false },
    @{ Tag = "v6.0.0"; Name = "v6.0.0 - Mayday"; Prerelease = $false }
)

foreach ($t in $tags) {
    $ver = $t.Tag.TrimStart('v')
    $docFile = "docs/releases/$($t.Tag).md"
    
    Write-Host "Packaging version $($t.Tag)..." -ForegroundColor Cyan
    & ./scripts/package-release.ps1 -Version $ver

    $zipFile = "AirlineReservationSystem-v$ver.zip"
    if (Test-Path $zipFile) {
        Write-Host "Uploading asset $zipFile to GitHub Release $($t.Tag)..." -ForegroundColor Green
        
        gh release upload $t.Tag $zipFile --clobber 2>$null
        if ($LASTEXITCODE -ne 0) {
            if ($t.Prerelease) {
                gh release create $t.Tag $zipFile -F $docFile --title $t.Name --prerelease
            } else {
                gh release create $t.Tag $zipFile -F $docFile --title $t.Name
            }
        }
    }
}

Write-Host "All GitHub Release assets uploaded successfully!" -ForegroundColor Green
