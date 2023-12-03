# Changelog

All notable changes to the Airline Reservation System project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [6.0.0] - "Mayday" - 2023-12-17

### Added
- Emergency Squawk 7700 flight abort protocol and diagnostic control center in `MaydayCreditsForm.cs`.
- System health diagnostic alerts and emergency fail-safe rerouting engine.
- Symmetrical project contributor credits card section featuring team profiles and GitHub badges.

---

## [5.0.0] - "Touchdown" - 2023-12-14

### Added
- Electronic Boarding Pass generator with PNR barcode aesthetic in `ReceiptTouchdownForm.cs`.
- Local disk persistence engine writing flat-file logs to `Airline Reservation History/Boarding Passes.txt`.
- Print spooler integration for physical boarding pass receipt printing.

---

## [4.0.0] - "Cruising" - 2023-12-11

### Added
- Dynamic yield revenue analytics dashboard in `AnalyticsCruisingForm.cs`.
- Custom GDI+ canvas rendering cabin load factor bar charts and fleet capacity metrics.
- Executive report generation suite in `ReportGenerationForm.cs` supporting TXT and CSV audit export formats.

---

## [3.0.0] - "Ascent" - 2023-12-08

### Added
- Checked baggage weight check-in calculator and excess weight fee enforcement ($12.50 / kg) in `BaggageAscentForm.cs`.
- In-flight meal preference selector and optional amenity toggles (Wi-Fi, Lounge Pass, Priority Boarding).

---

## [2.0.0] - "Taxi" - 2023-12-05

### Added
- Interactive 2-2 aircraft seating grid with real-time seat availability toggling in `SeatTaxiForm.cs`.
- Cabin class multiplier pricing system (Economy 1.0x, Business 1.85x, First Class 3.20x).

---

## [1.0.0] - "Clearance" - 2023-12-03

### Added
- Initial project release featuring secure User Login (`LoginForm.cs`) and Sign Up (`SignupForm.cs`) with password eye toggle button (👁 / 🙈).
- Flight route catalogue and passenger clearance registration in `WelcomeClearanceForm.cs`.
