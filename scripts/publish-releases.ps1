# PowerShell script to automatically publish or draft GitHub Releases for all version tags

$ErrorActionPreference = "Continue"

$tags = @(
    @{ Tag = "v1.0.0"; Name = "v1.0.0 — Clearance"; Prerelease = $true },
    @{ Tag = "v2.0.0"; Name = "v2.0.0 — Taxi"; Prerelease = $false },
    @{ Tag = "v3.0.0"; Name = "v3.0.0 — Ascent"; Prerelease = $false },
    @{ Tag = "v4.0.0"; Name = "v4.0.0 — Cruising"; Prerelease = $false },
    @{ Tag = "v5.0.0"; Name = "v5.0.0 — Touchdown"; Prerelease = $false },
    @{ Tag = "v6.0.0"; Name = "v6.0.0 — Mayday"; Prerelease = $false }
)

foreach ($t in $tags) {
    $ver = $t.Tag.TrimStart('v')
    $docFile = "docs/releases/$($t.Tag).md"
    
    Write-Host "Packaging version $($t.Tag)..." -ForegroundColor Cyan
    & ./scripts/package-release.ps1 -Version $ver

    $zipFile = "AirlineReservationSystem-$($t.Tag).zip"
    if (Test-Path $zipFile) {
        Write-Host "Creating GitHub Release for $($t.Tag)..." -ForegroundColor Green
        
        $prereleaseFlag = if ($t.Prerelease) { "--prerelease" } else { "" }
        
        if ($prereleaseFlag -ne "") {
            gh release create $t.Tag $zipFile -F $docFile --title $t.Name --prerelease --clobber
        } else {
            gh release create $t.Tag $zipFile -F $docFile --title $t.Name --clobber
        }
    }
}

Write-Host "All GitHub Releases processed successfully!" -ForegroundColor Green
