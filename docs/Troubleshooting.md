# Troubleshooting Guide

## Common Issues

### 1. High DPI / Scaling Issues
- **Symptom**: Form elements look blurry or misplaced.
- **Solution**: The application uses `<ApplicationHighDpiMode>PerMonitorV2</ApplicationHighDpiMode>`. Ensure Windows scaling is set to 100% or 125%.

### 2. Permission Denied Writing Travel History
- **Symptom**: Error popup when saving boarding pass log.
- **Solution**: Ensure write permissions are granted to `<app directory>/Airline Reservation History/`.
