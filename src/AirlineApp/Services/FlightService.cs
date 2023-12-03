namespace AirlineApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AirlineApp.Models;

    public class FlightService
    {
        private static readonly List<Flight> AvailableFlights = new()
        {
            new Flight { FlightNumber = "PK-301", Airline = "PIA Airlines", Origin = "Karachi", OriginCode = "KHI", Destination = "Islamabad", DestinationCode = "ISB", DepartureTime = "08:30 AM", AircraftType = "Boeing 777-300ER", BaseFare = 180.00m, DistanceKm = 1140 },
            new Flight { FlightNumber = "PK-302", Airline = "PIA Airlines", Origin = "Lahore", OriginCode = "LHE", Destination = "Karachi", DestinationCode = "KHI", DepartureTime = "11:15 AM", AircraftType = "Airbus A320-200", BaseFare = 165.00m, DistanceKm = 1020 },
            new Flight { FlightNumber = "PA-200", Airline = "Airblue", Origin = "Karachi", OriginCode = "KHI", Destination = "Dubai", DestinationCode = "DXB", DepartureTime = "02:45 PM", AircraftType = "Airbus A321neo", BaseFare = 340.00m, DistanceKm = 1190 },
            new Flight { FlightNumber = "ER-501", Airline = "Serene Air", Origin = "Islamabad", OriginCode = "ISB", Destination = "Skardu", DestinationCode = "KDU", DepartureTime = "06:00 AM", AircraftType = "Boeing 737-800", BaseFare = 140.00m, DistanceKm = 290 },
            new Flight { FlightNumber = "EK-605", Airline = "Emirates", Origin = "Karachi", OriginCode = "KHI", Destination = "London Heathrow", DestinationCode = "LHR", DepartureTime = "09:00 PM", AircraftType = "Boeing 777-200LR", BaseFare = 850.00m, DistanceKm = 6310 },
            new Flight { FlightNumber = "QR-611", Airline = "Qatar Airways", Origin = "Lahore", OriginCode = "LHE", Destination = "Doha", DestinationCode = "DOH", DepartureTime = "04:30 AM", AircraftType = "Boeing 787-9 Dreamliner", BaseFare = 520.00m, DistanceKm = 2150 }
        };

        public static List<Flight> GetFlights() => AvailableFlights;

        public static Flight GetFlightByNumber(string flightNumber)
        {
            return AvailableFlights.FirstOrDefault(f => f.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase)) 
                   ?? AvailableFlights[0];
        }

        public static string GeneratePnr()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new char[6];
            for (int i = 0; i < 6; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            return new string(result);
        }

        public static decimal CalculateExcessBaggageFee(double weightKg, CabinClass cabin)
        {
            double allowedWeight = cabin switch
            {
                CabinClass.Economy => 20.0,
                CabinClass.Business => 35.0,
                CabinClass.FirstClass => 50.0,
                _ => 20.0
            };

            if (weightKg <= allowedWeight) return 0.0m;

            double excess = weightKg - allowedWeight;
            return (decimal)(excess * 12.50); // $12.50 per excess kg
        }

        public static decimal GetCabinMultiplier(CabinClass cabin)
        {
            return cabin switch
            {
                CabinClass.Economy => 1.0m,
                CabinClass.Business => 1.85m,
                CabinClass.FirstClass => 3.20m,
                _ => 1.0m
            };
        }

        public static Booking CalculateFullBooking(Flight flight, Passenger passenger)
        {
            decimal cabinMult = GetCabinMultiplier(passenger.Cabin);
            decimal baseFare = flight.BaseFare;
            decimal cabinSurcharge = baseFare * (cabinMult - 1.0m);
            decimal excessBaggage = CalculateExcessBaggageFee(passenger.BaggageWeightKg, passenger.Cabin);

            decimal addons = 0.0m;
            if (passenger.WifiPass) addons += 25.00m;
            if (passenger.LoungeAccess) addons += 45.00m;
            if (passenger.PriorityBoarding) addons += 20.00m;

            decimal airportTax = (baseFare + cabinSurcharge) * 0.12m; // 12% airport aviation tax
            decimal total = baseFare + cabinSurcharge + excessBaggage + addons + airportTax;

            return new Booking
            {
                PnrReference = GeneratePnr(),
                FlightDetails = flight,
                PassengerDetails = passenger,
                BaseFare = baseFare,
                CabinMultiplier = cabinMult,
                CabinSurcharge = cabinSurcharge,
                ExcessBaggageFee = excessBaggage,
                AddonServicesFee = addons,
                AirportTax = airportTax,
                TotalFare = total,
                BookingTimestamp = DateTime.Now,
                FlightPhaseStatus = "Touchdown / Confirmed"
            };
        }
    }
}
