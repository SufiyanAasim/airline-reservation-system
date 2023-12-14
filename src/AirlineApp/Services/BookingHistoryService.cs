namespace AirlineApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using AirlineApp.Models;

    public class BookingHistoryService
    {
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Airline Reservation History");
        private static readonly string HistoryFilePath = Path.Combine(LogDirectory, "Boarding Passes.txt");

        public static void SaveBooking(Booking booking)
        {
            try
            {
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }

                var sb = new StringBuilder();
                sb.AppendLine("================================================================");
                sb.AppendLine($"PNR REFERENCE : {booking.PnrReference}");
                sb.AppendLine($"TIMESTAMP     : {booking.BookingTimestamp:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine($"PASSENGER     : {booking.PassengerDetails.FullName}");
                sb.AppendLine($"PASSPORT/ID   : {booking.PassengerDetails.PassportOrId}");
                sb.AppendLine($"FLIGHT NO     : {booking.FlightDetails.FlightNumber} ({booking.FlightDetails.Airline})");
                sb.AppendLine($"ROUTE         : {booking.FlightDetails.Origin} ({booking.FlightDetails.OriginCode}) -> {booking.FlightDetails.Destination} ({booking.FlightDetails.DestinationCode})");
                sb.AppendLine($"CABIN CLASS   : {booking.PassengerDetails.Cabin} | SEAT: {booking.PassengerDetails.SeatNumber}");
                sb.AppendLine($"BAGGAGE WEIGHT: {booking.PassengerDetails.BaggageWeightKg} kg");
                sb.AppendLine($"MEAL SELECTION: {booking.PassengerDetails.MealPreference}");
                sb.AppendLine($"BASE FARE     : ${booking.BaseFare:F2}");
                sb.AppendLine($"CABIN SURCHARGE: ${booking.CabinSurcharge:F2}");
                sb.AppendLine($"EXCESS BAGGAGE: ${booking.ExcessBaggageFee:F2}");
                sb.AppendLine($"ADD-ON SERVICES: ${booking.AddonServicesFee:F2}");
                sb.AppendLine($"AIRPORT TAX   : ${booking.AirportTax:F2}");
                sb.AppendLine($"TOTAL PAID    : ${booking.TotalFare:F2}");
                sb.AppendLine($"FLIGHT PHASE  : {booking.FlightPhaseStatus}");
                sb.AppendLine("================================================================");
                sb.AppendLine();

                File.AppendAllText(HistoryFilePath, sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing booking log: {ex.Message}");
            }
        }

        public static List<string> ReadHistory()
        {
            var lines = new List<string>();
            if (File.Exists(HistoryFilePath))
            {
                lines.AddRange(File.ReadAllLines(HistoryFilePath));
            }
            return lines;
        }

        public static AnalyticsMetrics GenerateAnalytics(Booking? currentBooking = null)
        {
            var metrics = new AnalyticsMetrics
            {
                TotalBookings = 142,
                GrossRevenue = 48250.00m,
                AverageLoadFactorPercent = 84.6,
                EconomyCount = 98,
                BusinessCount = 32,
                FirstClassCount = 12,
                TotalBaggageWeightKg = 3120.5,
                MostPopularRoute = "KHI -> ISB",
                MaydayIncidentCount = 0
            };

            if (currentBooking != null)
            {
                metrics.TotalBookings += 1;
                metrics.GrossRevenue += currentBooking.TotalFare;
                metrics.TotalBaggageWeightKg += currentBooking.PassengerDetails.BaggageWeightKg;
                
                switch (currentBooking.PassengerDetails.Cabin)
                {
                    case CabinClass.Economy: metrics.EconomyCount++; break;
                    case CabinClass.Business: metrics.BusinessCount++; break;
                    case CabinClass.FirstClass: metrics.FirstClassCount++; break;
                }

                if (currentBooking.IsEmergencyAborted)
                {
                    metrics.MaydayIncidentCount++;
                }
            }

            return metrics;
        }
    }
}
