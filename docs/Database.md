# Database & Storage Model

Airline Reservation System is designed as a fully offline desktop application that utilizes flat-file storage instead of a relational database server.

## Persisted Data Files

### 1. Boarding Pass & Booking Logs
- **File Location**: `<app directory>/Airline Reservation History/Boarding Passes.txt`
- **Format**: Flat text key-value ledger appended per booking transaction.

### 2. Exported Audit CSV Logs
- **File Location**: `<app directory>/Airline Reservation History/Audit_Ledger_<timestamp>.csv`
- **Format**: Standard comma-separated values (PNR, Timestamp, Passenger, FlightNo, Origin, Destination, Cabin, Seat, TotalFare).
