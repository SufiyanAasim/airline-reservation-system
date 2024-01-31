# System Architecture & Technical Specification

The **Airline Reservation System** is an enterprise-grade C# .NET 8 WinForms desktop software platform built using modular multi-tier architecture, custom GDI+ rendering engines, real-time animation loops, synthesized PCM wave audio, and SQLite database persistence.

---

## 🏛 Architecture Layers

```
                     ┌────────────────────────────────────────┐
                     │          WinForms View Layer           │
                     │  (LoginForm, WelcomeClearanceForm,     │
                     │   SeatTaxiForm, BaggageAscentForm,     │
                     │   AnalyticsCruisingForm, MaydayForm,   │
                     │   ReceiptTouchdownForm, CreditsForm)  │
                     └───────────────────┬────────────────────┘
                                         │
                                         ▼
                     ┌────────────────────────────────────────┐
                     │            Service Engine              │
                     │   (AuthService, FlightService,         │
                     │    BookingHistoryService,              │
                     │    FormNavigator, SoundHelper)         │
                     └───────────────────┬────────────────────┘
                                         │
                                         ▼
                     ┌────────────────────────────────────────┐
                     │           Persistence Layer            │
                     │   (SQLite Engine: AirlineSystem.db     │
                     │    Audit Ledger: Boarding Passes.txt)  │
                     └────────────────────────────────────────┘
```

---

## 🔑 Core Subsystems

### 1. Presentation & Form Navigation Engine
- **`FormNavigator`**: Manages seamless form transitions, centering windows, inheriting screen positions, and maintaining state.
- **`IconHelper`**: Extracts high-resolution embedded application icons to window titlebars and Windows Taskbar.
- **`CustomMessageBox`**: Dark-themed modal dialog replacement for native Win32 message boxes.

### 2. Audio & Visual Animation Engine
- **`SoundHelper`**: Custom 44.1kHz 16-bit PCM WAV synthesizer generating tactile click/tap sounds and emergency Mayday sirens without external media dependencies.
- **`GDI+ Canvas`**: Real-time fleet seat capacity bar chart canvas on `AnalyticsCruisingForm`.
- **`WinForms Timers`**: Multithreaded live telemetry pulse, pulsing radar beacon card, and smooth fuel jettison progress animation.

### 3. Persistence Engine (`AirlineSystem.db`)
- **`DatabaseService`**: Manages local **SQLite Database (`AirlineSystem.db`)** connection pooling, automatic schema initialization, and transactional PNR inserts.
- **`BookingHistoryService`**: Dual-writes complete booking transactions to both the SQLite database and local ASCII audit log files.
