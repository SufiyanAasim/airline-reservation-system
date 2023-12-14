# Development Guide

## Build Prerequisites

- Windows 10/11 OS
- .NET 8.0 SDK or Visual Studio 2022+

## Command Line Build

```bash
dotnet build "src/AirlineApp/AirlineApp.csproj" -c Release
```

## Running the Application

```bash
dotnet run --project "src/AirlineApp/AirlineApp.csproj"
```

## Packaging Release Bundles

```powershell
./scripts/package-release.ps1 -Version "6.0.0"
```
