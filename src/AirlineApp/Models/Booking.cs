namespace AirlineApp.Models
{
    public class Booking
    {
        public string PnrReference { get; set; } = string.Empty;
        public Flight FlightDetails { get; set; } = new Flight();
        public Passenger PassengerDetails { get; set; } = new Passenger();
        
        public decimal BaseFare { get; set; }
        public decimal CabinMultiplier { get; set; } = 1.0m;
        public decimal CabinSurcharge { get; set; }
        public decimal ExcessBaggageFee { get; set; }
        public decimal AddonServicesFee { get; set; }
        public decimal AirportTax { get; set; }
        public decimal TotalFare { get; set; }
        
        public DateTime BookingTimestamp { get; set; } = DateTime.Now;
        public string FlightPhaseStatus { get; set; } = "Touchdown / Confirmed";
        public bool IsEmergencyAborted { get; set; } = false;
    }
}
