<div align="center">

# Airline Reservation System

**An Enterprise Aviation & Flight Phase Reservation Desktop Platform for Windows**

[![.NET](https://img.shields.io/badge/.NET-8.0--windows-512BD4?style=flat&logo=dotnet&logoColor=white)](docs/Development.md)
[![Version](https://img.shields.io/badge/version-6.0.0%20Mayday-ef4444?style=flat)](docs/releases/v6.0.0.md)
[![License: MIT](https://img.shields.io/badge/License-MIT-22c55e?style=flat)](LICENSE)
[![Platform](https://img.shields.io/badge/platform-Windows-64748b?style=flat)]()
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-0ea5e9?style=flat)](CONTRIBUTING.md)

Integrated flight clearance, interactive 2-2 seating matrix, baggage allowance engine, real-time load factor & revenue analytics, customizable executive report generator, printable boarding passes, and emergency Mayday protocol — all offline, single-user, zero installer.

[**Download .exe**](docs/releases/v6.0.0.md) · [**Changelog**](CHANGELOG.md) · [**Roadmap**](ROADMAP.md) · [**Report a Bug**](.github/ISSUE_TEMPLATE/bug_report.md)

</div>

---

## ✨ Features Across Flight Phase Releases

### 🔐 Auth Phase: Secure User Portal
- Dual-role authentication (Passenger / Flight Operations Manager)
- Password input with interactive **Eye Toggle Button (👁 / 🙈)** to reveal/hide password text
- Account registration with instant validation and demo account seeding

### 🛫 Phase 1: v1.0.0 Clearance
- Flight search & route selection engine (KHI, LHE, ISB, DXB, LHR, KDU, DOH)
- Passenger registration and clearance validation

### 🛞 Phase 2: v2.0.0 Taxi
- Interactive 2-2 aircraft seating grid with real-time seat availability & selection
- Cabin class selector (Economy 1.0x, Business 1.85x, First Class 3.20x) with dynamic fare preview

### 🛬 Phase 3: v3.0.0 Ascent
- Checked baggage weight check-in calculator and excess weight fee enforcement ($12.50 / kg)
- In-flight meal preference menu & extra amenity toggles (Wi-Fi Pass, VIP Lounge, Priority Boarding)

### 📊 Phase 4: v4.0.0 Cruising
- Live flight telemetry (altitude, airspeed, fuel burn rate, cabin pressure)
- Custom GDI+ rendered dynamic cabin load factor bar charts & yield revenue analytics

### 📄 Reporting Phase: Executive Audit Generator
- Dedicated report builder with date range filtering
- Export flight manifests, financial audit summary, and safety logs in **.TXT** and **.CSV** formats

### 🎫 Phase 5: v5.0.0 Touchdown
- Printable electronic Boarding Pass & official receipt renderer
- Local travel history flat-file persistence saved to `Airline Reservation History/`

### 🚨 Phase 6: v6.0.0 Mayday
- Emergency Mayday Squawk 7700 flight abort & fail-safe rerouting overlay
- Live aircraft health telemetry & system diagnostic alerts
- Symmetrical project contributor credits card section

---

## 🏗️ System Architecture

```
┌────────────────────────────────────────────────────────────────────────┐
│                          C# WinForms Presentation                      │
│   LoginForm ➔ SignupForm ➔ WelcomeClearance ➔ SeatTaxi ➔               │
│   BaggageAscent ➔ AnalyticsCruising ➔ ReportGeneration ➔               │
│               ReceiptTouchdown ➔ MaydayCredits                         │
└────────────────┬───────────────────┬──────────────────┬────────────────┘
                 │                   │                  │
                 ▼                   ▼                  ▼
           FormNavigator        SoundHelper       FlightService
          (screen state)      (audio feedback)  (pricing & seats)
                                                        │
                                                        ▼
                                              BookingHistoryService
                                              (local flat-file log)
```

Full breakdown in [docs/Architecture.md](docs/Architecture.md).

---

## 🛠️ Technology Stack

| Technology | Purpose |
|------------|---------|
| **C# / .NET 8.0-windows** | Core desktop application framework |
| **System.Windows.Forms** | GUI controls, high DPI rendering, and double-buffered custom graphics |
| **System.Drawing (GDI+)** | Dynamic chart canvas rendering & custom dark UI graphics |
| **System.IO** | Flat-file travel history & audit report persistence |

---

## 🚀 Getting Started

### Requirements
- Windows OS
- .NET 8.0 SDK or Visual Studio 2022+

### Build & Run

```bash
git clone https://github.com/SufiyanAasim/airline-reservation-system.git
cd airline-reservation-system
dotnet build "src/AirlineApp/AirlineApp.csproj" -c Release
dotnet run --project "src/AirlineApp/AirlineApp.csproj"
```

---

## 📦 Packaging Executable Releases

```powershell
./scripts/package-release.ps1 -Version "6.0.0"
```

Packages `AirlineApp.exe`, DLLs, and documentation into `AirlineReservationSystem-v6.0.0.zip`.

---

## 🤝 Contributors

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

---

## 📄 License

[MIT License](LICENSE) © 2023-2026 AeroTech Systems Contributors.
