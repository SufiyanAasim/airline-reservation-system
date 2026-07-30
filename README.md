<div align="center">

<img src="assets/logo.png" alt="Airline Reservation System Logo" width="110" />

# Airline Reservation System

**A Windows desktop reservation and yield analytics platform for airline operations**

[![.NET](https://img.shields.io/badge/.NET-8.0--windows-512BD4?style=flat&logo=dotnet&logoColor=white)](docs/Development.md)
[![Version](https://img.shields.io/badge/version-6.0.0%20Mayday-ef4444?style=flat)](docs/releases/v6.0.0.md)
[![License: MIT](https://img.shields.io/badge/License-MIT-22c55e?style=flat)](LICENSE)
[![Platform](https://img.shields.io/badge/platform-Windows-64748b?style=flat)]()
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-0ea5e9?style=flat)](CONTRIBUTING.md)

Registers passengers, manages interactive 2-2 aircraft seating, calculates baggage allowances, renders live load factor & revenue analytics charts, exports audit reports, prints boarding passes, and triggers emergency Mayday protocols — all offline, no installer, no accounts.

[**Download .exe**](docs/releases/v6.0.0.md) · [**Changelog**](CHANGELOG.md) · [**Roadmap**](ROADMAP.md) · [**Report a Bug**](.github/ISSUE_TEMPLATE/bug_report.md)

</div>

---

## ✨ Features

### 🔐 User Portal & Authentication
- Dual-role access (Passenger / Operations Manager) with password eye toggle button (👁 / 🙈)
- Self-service registration with full name, passport, email, and instant credential validation

### 🛫 Flight Clearance Engine
- Flight search across 7 major domestic and international routes (KHI, LHE, ISB, DXB, LHR, KDU, DOH)
- Departure clearance verification and aircraft route details lookup

### 🛞 Interactive Cabin Seating Map
- 2-2 aircraft seating grid (24 seats) with real-time seat availability status colors
- Cabin class pricing tiers: Economy (1.0x), Business (1.85x), First Class (3.20x)

### 🛬 Baggage & In-Flight Engine
- Baggage allowance calculator enforcing 20kg (Economy), 35kg (Business), and 50kg (First Class) limits
- Excess baggage rate calculation ($12.50 / kg) and optional in-flight amenity add-on toggles

### 📊 Cruising Telemetry & Yield Analytics
- Live telemetry metrics (FL380 altitude, Mach 0.82 airspeed, fuel burn rate, cabin pressure)
- Custom GDI+ double-buffered canvas rendering cabin load factor capacity bar charts

### 📄 Executive Report Generation
- Dedicated audit report builder filtering passenger manifests, financial revenue, and safety logs
- Exporting support to formatted text (.TXT) and spreadsheet (.CSV) files

### 🎫 Printable Boarding Pass & Local History
- Electronic Boarding Pass card with PNR barcode aesthetic and Windows print spooler integration
- Auto-saves every booking to a local travel history text log (`Airline Reservation History/`)

### 🚨 Mayday Emergency Protocol
- Emergency Squawk 7700 flight abort trigger and real-time aircraft health telemetry
- Solo project contributor credits section

---

## 🏗️ Architecture

```
┌──────────────────────────────────────────────────────────┐
│                      WinForms UI                          │
│ Login ➔ Signup ➔ Clearance ➔ Taxi ➔ Ascent ➔ Cruising ➔ │
│            Report ➔ Touchdown ➔ Mayday                   │
└───────┬───────────────┬──────────────┬───────────────────┘
        │               │              │
        ▼               ▼              ▼
 FormNavigator     SoundHelper    FlightService & AuthService
 (screen swap)    (tap feedback)  (pricing & seats engine)
                                        │
                                        ▼
                                BookingHistoryService
                                (local text-file log)
```

Full breakdown in [docs/Architecture.md](docs/Architecture.md).

---

## 🛠️ Technology Stack

### .NET Framework & Packages

| Namespace / Package | Purpose |
|---------------------|---------|
| `System.Windows.Forms` | GUI framework — forms, controls, dialogs, DPI awareness |
| `System.Drawing` | Custom GDI+ double-buffered chart graphics and icons |
| `System.IO` | Travel history file persistence and CSV report exporter |
| `System.Media` | Audio tap sound feedback |

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

The app saves travel history to `<app directory>/Airline Reservation History/`. Full setup details in [docs/Development.md](docs/Development.md).

---

## 🗂️ Project Structure

```
airline-reservation-system/
├── .github/                # CI/CD workflows, issue/PR templates
├── assets/                 # Application branding logo
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
│       ├── Services/        # FlightService, AuthService, BookingHistoryService, FormNavigator
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

The app models 7 flight routes:

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

Builds in Release mode and stages `AirlineApp.exe` plus its config, runtime DLLs, and docs into `AirlineReservationSystem-v6.0.0.zip`. See [docs/Development.md](docs/Development.md).

---

## 🛡️ Security

This is a fully offline, single-user desktop app — no network calls, no external server database (see [docs/Architecture.md](docs/Architecture.md) and [docs/Database.md](docs/Database.md)). The only persisted state is a local travel-history text log. See [SECURITY.md](SECURITY.md) to report a vulnerability.

---

## 👤 Standalone Contributor

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/SufiyanAasim">
        <img src="https://github.com/SufiyanAasim.png" width="80" alt="SufiyanAasim"/><br/>
        <sub><b>Mohammad Sufiyan Aasim</b></sub>
      </a><br/>
      <sub>Sole System Architect & Developer</sub>
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
