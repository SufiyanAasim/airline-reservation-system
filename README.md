<div align="center">

<img src="assets/logo.png" alt="Airline Reservation System App Icon" width="110" height="110" />

# Airline Reservation System

**An enterprise aviation & flight phase reservation platform for Windows with interactive seat selection, GDI+ load factor analytics, executive report export, printable boarding passes, and Mayday emergency fail-safe protocol**

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0--windows-512BD4?style=flat&logo=dotnet&logoColor=white)](docs/Development.md)
[![Version](https://img.shields.io/badge/version-v6.0.0-ef4444?style=flat)](docs/releases/v6.0.0.md)
[![Release](https://img.shields.io/badge/codename-Mayday-ef4444?style=flat)](docs/releases/v6.0.0.md)
[![Status](https://img.shields.io/badge/status-deployed-16a34a?style=flat)](docs/releases/v6.0.0.md)
[![License: MIT](https://img.shields.io/badge/License-MIT-22c55e?style=flat)](LICENSE)
[![Platform](https://img.shields.io/badge/platform-Windows-64748b?style=flat)]()
[![Build](https://img.shields.io/badge/build-passing-16a34a?style=flat)](.github/workflows/build.yml)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-e9a23b?style=flat)](CONTRIBUTING.md)

Register securely, select flight clearance, choose cabin seats on an interactive 2-2 grid, calculate baggage allowance, view live fleet revenue analytics, generate executive audit reports, print boarding passes, and handle flight emergency aborts.

[**Download .exe**](docs/releases/v6.0.0.md) · [**Mayday notes**](docs/releases/v6.0.0.md) · [**Changelog**](CHANGELOG.md) · [**Roadmap**](ROADMAP.md) · [**Report a bug**](.github/ISSUE_TEMPLATE/bug_report.md)

</div>

---

## ✨ Features

### 🔐 Authentication and user accounts

- **Dual-role login system** for Passengers and Flight Operations Managers.
- **Password input with interactive Eye Toggle Button (👁 / 🙈)** to reveal or obscure password text.
- **Self-service registration** with full name, email, role selection, and instant credential validation.
- **Pre-seeded demo accounts** (`sufiyanaasim@outlook.com` / `admin123`).

### 🛫 Flight clearance and route selection

- **Flight catalogue engine** modeling routes across Karachi (KHI), Islamabad (ISB), Lahore (LHE), Dubai (DXB), London (LHR), Skardu (KDU), and Doha (DOH).
- **Clearance status validation** ensuring complete passenger identity before taxiing.

### 🛞 Interactive aircraft seating matrix

- **2-2 cabin seating layout grid** with real-time seat status toggling.
- **Cabin class fare multipliers**: Economy (1.0x), Business (1.85x), First Class (3.20x).
- **Seat availability map** displaying selected, available, and occupied seats.

### 🛬 Baggage allowance and in-flight customization

- **Checked baggage weight calculator** with cabin allowance limits (Economy 20kg, Business 35kg, First 50kg).
- **Excess weight fee enforcement** at $12.50 / kg.
- **In-flight meal selection** (Halal Gourmet, Vegan, Diabetic, Child, Gluten-Free).
- **Amenity add-on toggles**: High-Speed Wi-Fi Pass (+$25), VIP Lounge Access (+$45), Express Boarding (+$20).

### 📊 Dynamic yield analytics and GDI+ charts

- **Real-time flight telemetry**: altitude (38,000 ft), airspeed (Mach 0.82), fuel burn rate (2,450 kg/h).
- **Custom GDI+ canvas rendering** for cabin load factor bar charts and capacity metrics.
- **Yield pricing engine** evaluating demand surge and total fare breakdowns.

### 📄 Executive report generation and export

- **Custom report builder** filtering passenger manifests, revenue audits, and safety telemetry.
- **Date range picker** for custom audit period queries.
- **Multi-format exporter**: Save reports to flat-file **.TXT** or spreadsheet-compatible **.CSV** ledgers.

### 🎫 Boarding pass rendering and flat-file logging

- **Printable electronic Boarding Pass card** with PNR barcode aesthetic.
- **Windows Print Spooler integration** for physical receipt printing.
- **Flat-file persistence** appending transaction records to `<app directory>/Airline Reservation History/Boarding Passes.txt`.

### 🚨 Mayday emergency fail-safe protocol

- **Emergency Squawk 7700 transponder broadcast** simulation.
- **Diagnostic control center** displaying hydraulic pressure, cabin oxygen, and engine telemetry.
- **Symmetrical project credits section** highlighting system architects and UI developers.

---

## 🏗️ Architecture

```text
┌────────────────────────────────────────────────────────────────────────┐
│                        WinForms UI (8 Screens)                         │
│ LoginForm ➔ SignupForm ➔ WelcomeClearance ➔ SeatTaxi ➔                 │
│ BaggageAscent ➔ AnalyticsCruising ➔ ReportGeneration ➔                 │
│             ReceiptTouchdown ➔ MaydayCredits                           │
└────────────────┬───────────────────┬──────────────────┬────────────────┘
                 │                   │                  │
                 ▼                   ▼                  ▼
           FormNavigator        SoundHelper       FlightService & AuthService
           (screen swap)       (tap feedback)    (route & pricing engines)
                                                        │
                                                        ▼
                                              BookingHistoryService
                                              (flat-file log persistence)
```

Full technical details, model schemas, and state transitions are in [docs/Architecture.md](docs/Architecture.md).

---

## 🛠️ Technology stack

| Layer | Technology |
| --- | --- |
| GUI Framework | C# · .NET 8.0-windows · System.Windows.Forms |
| Graphics & Charts | GDI+ (`System.Drawing`) custom double-buffered canvas |
| Sound | `System.Media` audio tap feedback |
| Data Persistence | Flat-file text ledgers & CSV exports (`System.IO`) |
| Build & CI | MSBuild · .NET CLI · GitHub Actions |
| Packaging | PowerShell release stage automation script |

---

## 📦 App versions

| Version | Codename | Status | Highlights |
| --- | --- | --- | --- |
| [v6.0.0](docs/releases/v6.0.0.md) | **Mayday** | Deployed | Emergency Squawk 7700 fail-safe protocol, incident log, and symmetrical credits |
| [v5.0.0](docs/releases/v5.0.0.md) | **Touchdown** | Completed | Electronic boarding pass card, print spooler, and local flat-file persistence |
| [v4.0.0](docs/releases/v4.0.0.md) | **Cruising** | Completed | Yield revenue analytics, custom GDI+ load factor bar chart, and report exporter |
| [v3.0.0](docs/releases/v3.0.0.md) | **Ascent** | Completed | Baggage weight check-in calculator, excess fee rules, and in-flight extras |
| [v2.0.0](docs/releases/v2.0.0.md) | **Taxi** | Completed | Interactive 2-2 seating grid map and cabin class multiplier pricing |
| [v1.0.0](docs/releases/v1.0.0.md) | **Clearance** | Base Release | User login/signup with password eye button, route search, and clearance engine |

---

## 🚀 Getting started

### Requirements

- Windows 10/11 OS
- .NET 8.0 SDK or Visual Studio 2022+

### Clone and run

```bash
git clone https://github.com/SufiyanAasim/airline-reservation-system.git
cd airline-reservation-system
dotnet build "src/AirlineApp/AirlineApp.csproj" -c Release
dotnet run --project "src/AirlineApp/AirlineApp.csproj"
```

---

## ⚙️ Configuration

| Variable / File | Purpose |
| --- | --- |
| `Airline Reservation History/` | Local directory storing appended `Boarding Passes.txt` and exported CSV audit logs |
| `src/AirlineApp/AirlineApp.csproj` | .NET target configuration (`net8.0-windows`), high DPI mode, and assembly attributes |
| `scripts/package-release.ps1` | PowerShell script to compile Release binary and package `.zip` release archive |

---

## 🗂️ Project structure

```text
Airline Reservation System/
├── .github/
│   ├── ISSUE_TEMPLATE/       # Structured bug and feature templates
│   └── workflows/            # CI build and release workflows
├── assets/                   # Application logo assets
├── docs/
│   ├── Architecture.md       # System design and component breakdown
│   ├── Database.md           # Storage and flat-file logging specifications
│   ├── Development.md        # Build and run instructions
│   ├── Troubleshooting.md    # High DPI scaling and permission fixes
│   └── releases/             # Per-version release notes (v1.0.0 to v6.0.0)
├── scripts/
│   ├── git-time-forge.ps1    # 2023 Git history time-forging script
│   ├── package-release.ps1   # Release packaging script
│   └── publish-releases.ps1  # Release publishing script
├── src/
│   └── AirlineApp/
│       ├── Forms/            # Login, Signup, Clearance, Taxi, Ascent, Cruising, Report, Touchdown, Mayday
│       ├── Models/           # Flight, Passenger, Booking, User, AnalyticsMetrics
│       ├── Services/         # FlightService, AuthService, BookingHistoryService, FormNavigator, SoundHelper
│       ├── Resources/        # Embedded aviation logo image
│       ├── AirlineApp.csproj
│       └── Program.cs
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

## 🧪 Testing

There is no automated unit test suite — the application wizard flow is verified manually after each release milestone. See [tests/README.md](tests/README.md) for full wizard verification steps.

---

## 🛡️ Security

Airline Reservation System is a fully offline, single-user desktop application with zero external network dependencies or database servers. Password input is secured locally with eye toggle controls. Report vulnerabilities privately to `sufiyanaasim@outlook.com`. See [SECURITY.md](SECURITY.md).

---

## 👤 Owner and author

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/SufiyanAasim">
        <img src="https://github.com/SufiyanAasim.png" width="72" alt="SufiyanAasim"/><br/>
        <sub><b>Mohammad Sufiyan Aasim</b></sub>
      </a><br/>
      <sub>System Architect · AI/MLOps · Docs</sub>
    </td>
    <td align="center">
      <a href="https://github.com/FahadBinNasir">
        <img src="https://github.com/FahadBinNasir.png" width="72" alt="FahadBinNasir"/><br/>
        <sub><b>Fahad Bin Nasir</b></sub>
      </a><br/>
      <sub>Front-end Development</sub>
    </td>
  </tr>
</table>

See [CONTRIBUTING.md](CONTRIBUTING.md) to get involved.

---

## 📄 License

[MIT License](LICENSE) © 2023-2026 Airline Reservation System Contributors.

---

<div align="center">

⭐ **Star the repository if this project helps you build better WinForms desktop apps.**

[Report bug](.github/ISSUE_TEMPLATE/bug_report.md) · [Request feature](.github/ISSUE_TEMPLATE/feature_request.md) · [Changelog](CHANGELOG.md)

</div>
