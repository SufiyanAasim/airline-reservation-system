namespace AirlineApp.Models
{
    public enum CabinClass
    {
        Economy,
        Business,
        FirstClass
    }

    public class Passenger
    {
        public string FullName { get; set; } = string.Empty;
        public string PassportOrId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public CabinClass Cabin { get; set; } = CabinClass.Economy;
        public string SeatNumber { get; set; } = "12A";
        public double BaggageWeightKg { get; set; } = 15.0;
        public string MealPreference { get; set; } = "Standard Halal";
        public bool WifiPass { get; set; } = false;
        public bool LoungeAccess { get; set; } = false;
        public bool PriorityBoarding { get; set; } = false;
    }
}
