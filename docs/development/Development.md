# Developer Setup & Build Guide

## Prerequisites
- .NET 8.0 SDK (Windows Forms support enabled)
- Visual Studio 2022 or VS Code with C# Dev Kit
- Git

## Building & Running

```powershell
# Restore dependencies and build solution
dotnet build src/AirlineApp/AirlineApp.csproj -c Release

# Run application locally
dotnet run --project src/AirlineApp/AirlineApp.csproj
```

## Packaging & GitHub Release Publishing

To build binaries, create standalone zip archives, and publish release assets to GitHub:

```powershell
powershell -ExecutionPolicy Bypass -File "scripts/publish-releases.ps1"
```
