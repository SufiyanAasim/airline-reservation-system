<div align="center">

<img src="assets/logo.png" alt="Airline Reservation System Logo" width="110" />

# Airline Reservation System

**A Windows desktop reservation, yield analytics, and emergency telemetry platform for airline operations**

[![.NET](https://img.shields.io/badge/.NET-8.0--windows-512BD4?style=flat&logo=dotnet&logoColor=white)](docs/Development.md)
[![Version](https://img.shields.io/badge/version-6.0.0%20Mayday-ef4444?style=flat)](docs/releases/v6.0.0.md)
[![Database](https://img.shields.io/badge/Database-SQLite-003B57?style=flat&logo=sqlite&logoColor=white)](docs/Database.md)
[![License: MIT](https://img.shields.io/badge/License-MIT-22c55e?style=flat)](LICENSE)
[![Platform](https://img.shields.io/badge/platform-Windows-64748b?style=flat)]()
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-0ea5e9?style=flat)](CONTRIBUTING.md)

Registers passengers, manages interactive 2-2 aircraft seating, calculates baggage allowances, renders live load factor & revenue analytics charts, exports audit reports, logs transactions to SQLite database (`AirlineSystem.db`), plays PCM synth audio, prints boarding passes, and triggers emergency Mayday protocols — all offline with zero external server dependencies.

[**Download .exe**](docs/releases/v6.0.0.md) · [**Changelog**](CHANGELOG.md) · [**Roadmap**](ROADMAP.md) · [**Report a Bug**](.github/ISSUE_TEMPLATE/bug_report.md)

</div>

---

## ✨ Features

### 🔐 User Portal & Authentication
- Dual-role access (Passenger / Operations Manager) with password eye toggle button (👁 / 🙈)
- Self-service registration with full name, passport, email, and instant credential validation

### 🛫 Flight Clearance Engine
- Flight search across 7 major domestic and international routes (KHI, LHE, ISB, DXB, LHR, KDU, DOH)
- Departure clearance verification and live marquee status ticker animation

### 🛞 Interactive Cabin Seating Map
- 2-2 aircraft seating grid (24 seats) with real-time seat availability status colors
- Cabin class pricing tiers: Economy (1.0x), Business (1.85x), First Class (3.20x)

### 🛬 Baggage & In-Flight Engine
- Baggage allowance calculator enforcing 20kg (Economy), 35kg (Business), and 50kg (First Class) limits
- Excess baggage rate calculation ($12.50 / kg) and optional in-flight amenity add-on toggles

### 📊 Cruising Telemetry & Yield Analytics
- Live telemetry metrics (FL380 altitude micro-flicker, Mach 0.82 airspeed, fuel burn rate, cabin pressure)
- Custom GDI+ double-buffered canvas rendering cabin load factor capacity bar charts

### 📄 Executive Report Generation
- Dedicated audit report builder filtering passenger manifests, financial revenue, and safety logs
- Exporting support to formatted text (.TXT) and spreadsheet (.CSV) files

### 🗄️ SQLite Database Persistence (`AirlineSystem.db`)
- Embedded ACID database transaction logging for all bookings, PNR references, fare structures, and flight phases

### 🔊 Synthesized PCM Audio Feedback
- Custom 44.1kHz 16-bit PCM WAV audio synthesizer supplying tactile button tap clicks and emergency sirens

### 🎫 Printable Boarding Pass & Local History
- Electronic Boarding Pass card with PNR barcode aesthetic and Windows print spooler integration
- Auto-saves every booking to both the SQLite database (`AirlineSystem.db`) and local text audit logs (`Airline Reservation History/`)

### 🚨 Mayday Emergency Protocol & System Credits
- Emergency Squawk 7700 flight abort trigger with live pulsing radar beacon, fuel jettison progress animation, and cockpit telemetry deck
- Project author credits screen (`CreditsForm`)

---

## 🏗️ Architecture

```
┌──────────────────────────────────────────────────────────────────────────┐
│                             WinForms UI                                  │
│ Login ➔ Signup ➔ Clearance ➔ Taxi ➔ Ascent ➔ Cruising ➔ Reports ➔        │
│                    Touchdown ➔ Mayday ➔ Credits                          │
└────────┬───────────────┬──────────────┬──────────────────┬───────────────┘
         │               │              │                  │
         ▼               ▼              ▼                  ▼
  FormNavigator     SoundHelper    FlightService     DatabaseService
  (screen swap)    (PCM audio)     & AuthService      (SQLite engine)
                                        │                  │
                                        ▼                  ▼
                                BookingHistoryService (AirlineSystem.db)
```

Full breakdown in [docs/Architecture.md](docs/Architecture.md).

---

## 🛠️ Technology Stack

### .NET Framework & Packages

| Namespace / Package | Purpose |
|---------------------|---------|
| `Microsoft.Data.Sqlite` | Embedded SQLite database engine (`AirlineSystem.db`) for transaction logging |
| `System.Windows.Forms` | GUI framework — forms, controls, dialogs, DPI awareness, multithreaded timers |
| `System.Drawing` | Custom GDI+ double-buffered chart graphics and icons |
| `System.IO` | Travel history file persistence and CSV report exporter |
| `System.Media` | Synthesized 44.1kHz PCM WAV audio playback |

---

## 🚀 Getting Started

### Requirements
- Windows OS
- Visual Studio 2022 or later (or .NET 8.0 SDK for CLI builds)

### Clone and run

```bash
git clone https://github.com/SufiyanAasim/airline-reservation-system.git
cd airline-reservation-system
```

Open `src/AirlineApp/AirlineApp.csproj` in Visual Studio and build/run (`F5`), or build from the command line:

```bash
dotnet build "src/AirlineApp/AirlineApp.csproj" -c Release
```

Or download a packaged build from [docs/releases/v6.0.0.md](docs/releases/v6.0.0.md).

The app saves database transactions to `AirlineSystem.db` and audit text logs to `<app directory>/Airline Reservation History/`. Full setup details in [docs/Development.md](docs/Development.md).

---

## 🗂️ Project Structure

```
airline-reservation-system/
├── .github/                # CI/CD workflows, issue/PR templates
├── assets/                 # Application branding logo and icons
├── docs/
│   ├── Architecture.md
│   ├── Database.md
│   ├── Development.md
│   ├── Troubleshooting.md
│   └── releases/            # Per-version release notes (v1.0.0 to v6.0.0)
├── scripts/
│   ├── package-release.ps1  # Windows release packaging script
│   └── publish-releases.ps1  # Release publishing script
├── src/
│   └── AirlineApp/
│       ├── AirlineApp.csproj
│       ├── Program.cs
│       ├── Forms/           # 8 Aviation phase forms and custom dialogs
│       ├── Models/          # Flight, Passenger, Booking, User, Analytics models
│       ├── Services/        # FlightService, AuthService, BookingHistoryService, FormNavigator, IconHelper
│       └── Resources/       # Embedded app icon and logo assets
├── tests/                    # Testing guidance
├── CHANGELOG.md
├── CODE_OF_CONDUCT.md
├── CONTRIBUTING.md
├── LICENSE
├── README.md
├── RELEASE.md
├── ROADMAP.md
├── SECURITY.md
└── SUPPORT.md
```

---

## 🚉 Flight Routes

The app models 7 commercial flight routes:

| # | Origin | Destination | Flight No | Airline | Base Fare |
|---|--------|-------------|-----------|---------|-----------|
| 0 | Karachi (KHI) | Islamabad (ISB) | PK-301 | PIA Airlines | $180.00 |
| 1 | Lahore (LHE) | Karachi (KHI) | PK-302 | PIA Airlines | $165.00 |
| 2 | Karachi (KHI) | Dubai (DXB) | PA-200 | Airblue | $340.00 |
| 3 | Islamabad (ISB) | Skardu (KDU) | ER-501 | Serene Air | $140.00 |
| 4 | Karachi (KHI) | London (LHR) | EK-605 | Emirates | $850.00 |
| 5 | Lahore (LHE) | Doha (DOH) | QR-611 | Qatar Airways | $520.00 |

---

## 🧪 Testing

There is no automated test suite yet — the wizard flow is validated manually after each change. See [tests/README.md](tests/README.md) for what's testable and how to add coverage.

---

## 📦 Building the Windows Executable

```powershell
./scripts/package-release.ps1 -Version "6.0.0"
```

Builds in Release mode and stages `AirlineApp.exe` plus its config, runtime DLLs, SQLite dependencies, and docs into `AirlineReservationSystem-v6.0.0.zip`. See [docs/Development.md](docs/Development.md).

---

## 🛡️ Security

This is a fully offline, single-user desktop app — no network calls, no external server database. Data is stored locally in `AirlineSystem.db` (SQLite) and local travel-history text logs (see [docs/Architecture.md](docs/Architecture.md) and [docs/Database.md](docs/Database.md)). See [SECURITY.md](SECURITY.md) to report a vulnerability.

---

## 🤝 Contributor

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/SufiyanAasim">
        <img src="https://github.com/SufiyanAasim.png" width="80" alt="SufiyanAasim"/><br/>
        <sub><b>Mohammad Sufiyan Aasim</b></sub>
      </a><br/>
      <sub>System Architect · AI/MLOps · Docs</sub>
    </td>
  </tr>
</table>

See [CONTRIBUTING.md](CONTRIBUTING.md) to get involved.

---

## 📄 License

[MIT License](LICENSE) © 2023-2026 Airline Reservation System Contributors.

---

<div align="center">

⭐ **Star this repo if it helped you manage flight reservations.**

[Report Bug](.github/ISSUE_TEMPLATE/bug_report.md) · [Request Feature](.github/ISSUE_TEMPLATE/feature_request.md)

</div>
