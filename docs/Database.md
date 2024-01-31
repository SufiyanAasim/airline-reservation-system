# Database & Storage Specification

The **Airline Reservation System** utilizes an embedded **SQLite Database (`AirlineSystem.db`)** via `Microsoft.Data.Sqlite` for ACID-compliant structured storage, supplemented by structured ASCII text ledgers.

---

## 🗄 SQLite Database (`AirlineSystem.db`)

- **Database File**: `AirlineSystem.db` (Located in application root directory)
- **Primary Table**: `Bookings`

### Schema Definition
```sql
CREATE TABLE IF NOT EXISTS Bookings (
    PnrReference TEXT PRIMARY KEY,
    PassengerName TEXT NOT NULL,
    Passport TEXT,
    FlightNumber TEXT NOT NULL,
    Airline TEXT NOT NULL,
    Origin TEXT NOT NULL,
    Destination TEXT NOT NULL,
    Cabin TEXT NOT NULL,
    SeatNumber TEXT NOT NULL,
    BaggageKg REAL NOT NULL,
    Meal TEXT,
    BaseFare REAL NOT NULL,
    CabinSurcharge REAL NOT NULL,
    ExcessBaggageFee REAL NOT NULL,
    AddonServicesFee REAL NOT NULL,
    AirportTax REAL NOT NULL,
    TotalFare REAL NOT NULL,
    Timestamp TEXT NOT NULL,
    FlightStatus TEXT NOT NULL
);
```

---

## 📄 Audit Ledger Logs
- **Log Path**: `Airline Reservation History/Boarding Passes.txt`
- **Report Exports**: `.txt` and `.csv` files exported on demand via `ReportGenerationForm`.
