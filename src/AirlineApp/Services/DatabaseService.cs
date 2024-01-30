namespace AirlineApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Data.Sqlite;
    using AirlineApp.Models;

    public static class DatabaseService
    {
        private static readonly string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AirlineSystem.db");
        private static readonly string ConnectionString = $"Data Source={DbPath}";

        static DatabaseService()
        {
            InitializeDatabase();
        }

        public static void InitializeDatabase()
        {
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();

                string createTableQuery = @"
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
                ";

                using var command = new SqliteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SQLite Initialization Error: {ex.Message}");
            }
        }

        public static bool SaveBookingToSqlite(Booking booking)
        {
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();

                string insertQuery = @"
                    INSERT OR REPLACE INTO Bookings (
                        PnrReference, PassengerName, Passport, FlightNumber, Airline,
                        Origin, Destination, Cabin, SeatNumber, BaggageKg, Meal,
                        BaseFare, CabinSurcharge, ExcessBaggageFee, AddonServicesFee,
                        AirportTax, TotalFare, Timestamp, FlightStatus
                    ) VALUES (
                        @Pnr, @Name, @Passport, @FlightNo, @Airline,
                        @Origin, @Dest, @Cabin, @Seat, @Baggage, @Meal,
                        @BaseFare, @CabinSurcharge, @ExcessFee, @Addons,
                        @Tax, @Total, @Time, @Status
                    );
                ";

                using var command = new SqliteCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@Pnr", booking.PnrReference);
                command.Parameters.AddWithValue("@Name", booking.PassengerDetails.FullName);
                command.Parameters.AddWithValue("@Passport", booking.PassengerDetails.PassportOrId);
                command.Parameters.AddWithValue("@FlightNo", booking.FlightDetails.FlightNumber);
                command.Parameters.AddWithValue("@Airline", booking.FlightDetails.Airline);
                command.Parameters.AddWithValue("@Origin", booking.FlightDetails.OriginCode);
                command.Parameters.AddWithValue("@Dest", booking.FlightDetails.DestinationCode);
                command.Parameters.AddWithValue("@Cabin", booking.PassengerDetails.Cabin.ToString());
                command.Parameters.AddWithValue("@Seat", booking.PassengerDetails.SeatNumber);
                command.Parameters.AddWithValue("@Baggage", booking.PassengerDetails.BaggageWeightKg);
                command.Parameters.AddWithValue("@Meal", booking.PassengerDetails.MealPreference);
                command.Parameters.AddWithValue("@BaseFare", (double)booking.BaseFare);
                command.Parameters.AddWithValue("@CabinSurcharge", (double)booking.CabinSurcharge);
                command.Parameters.AddWithValue("@ExcessFee", (double)booking.ExcessBaggageFee);
                command.Parameters.AddWithValue("@Addons", (double)booking.AddonServicesFee);
                command.Parameters.AddWithValue("@Tax", (double)booking.AirportTax);
                command.Parameters.AddWithValue("@Total", (double)booking.TotalFare);
                command.Parameters.AddWithValue("@Time", booking.BookingTimestamp.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@Status", booking.FlightPhaseStatus);

                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SQLite Save Error: {ex.Message}");
                return false;
            }
        }

        public static int GetBookingCount()
        {
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var command = new SqliteCommand("SELECT COUNT(*) FROM Bookings;", connection);
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch
            {
                return 0;
            }
        }
    }
}
