param (
    [string]$Version = "6.0.0"
)

$ErrorActionPreference = "Stop"

Write-Host "Building Airline Reservation System v$Version in Release mode..." -ForegroundColor Cyan

$projectPath = "src/AirlineApp/AirlineApp.csproj"
dotnet build $projectPath -c Release

$stageDir = "temp_release_stage"
if (Test-Path $stageDir) { Remove-Item -Recurse -Force $stageDir }
New-Item -ItemType Directory -Path $stageDir | Out-Null

$binDir = "src/AirlineApp/bin/Release/net8.0-windows"
Copy-Item "$binDir/*" -Destination $stageDir -Recurse

$zipName = "AirlineReservationSystem-v$Version.zip"
if (Test-Path $zipName) { Remove-Item $zipName }

Compress-Archive -Path "$stageDir/*" -DestinationPath $zipName
Remove-Item -Recurse -Force $stageDir

Write-Host "Successfully packaged release to $zipName" -ForegroundColor Green
