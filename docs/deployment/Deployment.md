# Deployment & Release Guide

This document outlines deployment procedures, release packaging, and artifact asset publishing for the **Airline Reservation System**.

---

## 📦 Building Standalone Release Artifacts

The project uses PowerShell build automation to compile .NET 8 WinForms binaries in `Release` configuration and stage them into versioned zip archives:

```powershell
./scripts/package-release.ps1 -Version "6.0.0"
```

### Artifact Structure (`AirlineReservationSystem-v6.0.0.zip`)
- `AirlineApp.exe` (Executable application)
- `AirlineApp.dll` (Compiled core assembly)
- `Microsoft.Data.Sqlite.dll` (Embedded SQLite database provider)
- `SQLitePCLRaw.*.dll` (Native SQLite interop binaries)
- `AirlineApp.deps.json` & `AirlineApp.runtimeconfig.json`
- `README.md` & `LICENSE`

---

## 🚀 GitHub Release Deployment

To publish compiled release archives directly to GitHub Releases with asset clobber support:

```powershell
powershell -ExecutionPolicy Bypass -File "scripts/publish-releases.ps1"
```
