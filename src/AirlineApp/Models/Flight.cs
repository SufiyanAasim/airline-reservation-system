namespace AirlineApp.Models
{
    public class Flight
    {
        public string FlightNumber { get; set; } = string.Empty;
        public string Airline { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string OriginCode { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string DestinationCode { get; set; } = string.Empty;
        public string DepartureTime { get; set; } = string.Empty;
        public string AircraftType { get; set; } = string.Empty;
        public decimal BaseFare { get; set; }
        public int DistanceKm { get; set; }

        public override string ToString()
        {
            return $"{FlightNumber} | {OriginCode} -> {DestinationCode} ({AircraftType}) - ${BaseFare:F2}";
        }
    }
}
