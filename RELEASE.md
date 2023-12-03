# Release Process

This document describes how to cut a new version release of Airline Reservation System.

---

## 1. Decide version and codename

Use Semantic Versioning:

| Version | Codename | Flight Phase Theme |
|---|---|---|
| v1.0.0 | Clearance | Initial passenger clearance & auth |
| v2.0.0 | Taxi | Seat selection & cabin class |
| v3.0.0 | Ascent | Baggage calculator & in-flight extras |
| v4.0.0 | Cruising | Telemetry & dynamic analytics dashboard |
| v5.0.0 | Touchdown | Boarding pass receipt & persistence |
| v6.0.0 | Mayday | Emergency fail-safe protocol & credits |

---

## 2. Verify Build

```bash
dotnet build "src/AirlineApp/AirlineApp.csproj" -c Release
```

---

## 3. Package Release

```powershell
./scripts/package-release.ps1 -Version "6.0.0"
```

Generates release archive `AirlineReservationSystem-v6.0.0.zip`.
