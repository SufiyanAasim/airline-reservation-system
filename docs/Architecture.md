# Airline Reservation System Architecture

## System Design

The application employs a **layered desktop architecture** separating presentation, business logic, route services, and flat-file persistence.

```
┌──────────────────────────────────────────────────────────┐
│                   WinForms UI (8 Screens)                │
│ LoginForm ➔ SignupForm ➔ WelcomeClearance ➔ SeatTaxi ➔   │
│ BaggageAscent ➔ AnalyticsCruising ➔ ReportGeneration ➔   │
│            ReceiptTouchdown ➔ MaydayCredits              │
└───────┬───────────────┬──────────────┬───────────────────┘
        │               │              │
        ▼               ▼              ▼
  FormNavigator    SoundHelper   FlightService & AuthService
  (screen swap)   (tap sound)    (route & pricing engines)
                                       │
                                       ▼
                             BookingHistoryService
                             (flat-file log persistence)
```

## Component Breakdown

1. **Authentication Tier**: `AuthService.cs` manages user account validation, registration, and session memory.
2. **Flight Engine Tier**: `FlightService.cs` calculates base fares, cabin multipliers (Economy 1.0x, Business 1.85x, First 3.20x), excess baggage fees ($12.50/kg), and airport taxes (12%).
3. **Analytics & Reporting Tier**: `AnalyticsCruisingForm.cs` renders custom GDI+ capacity bar charts. `ReportGenerationForm.cs` compiles flight manifests and exports TXT/CSV logs.
4. **Persistence Tier**: `BookingHistoryService.cs` writes flat-file travel records to `Airline Reservation History/Boarding Passes.txt`.
5. **Emergency Fail-Safe Tier**: `MaydayCreditsForm.cs` simulates Squawk 7700 emergency broadcast and system health diagnostics.
