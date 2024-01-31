# Troubleshooting & FAQ

### Q1: App crashes on launching Mayday Emergency Control Deck
**Solution**: Ensure `ReceiptTouchdownForm` passes a non-null `Booking` object. Fallback constructors have been added in `MaydayForm` and `ReportGenerationForm` to gracefully generate default booking contexts.

### Q2: Audio clicks/taps are silent
**Solution**: `SoundHelper` synthesizes 44.1kHz 16-bit PCM WAV audio buffers directly into `System.Media.SoundPlayer`. Ensure audio playback devices are enabled in Windows Sound Settings.

### Q3: Where is booking data stored?
**Solution**: All bookings are stored in `AirlineSystem.db` (SQLite database) in the application directory, as well as `Airline Reservation History/Boarding Passes.txt`.
