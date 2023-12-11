namespace AirlineApp.Models
{
    public class AnalyticsMetrics
    {
        public int TotalBookings { get; set; }
        public decimal GrossRevenue { get; set; }
        public double AverageLoadFactorPercent { get; set; }
        public int EconomyCount { get; set; }
        public int BusinessCount { get; set; }
        public int FirstClassCount { get; set; }
        public double TotalBaggageWeightKg { get; set; }
        public string MostPopularRoute { get; set; } = "KHI -> ISB";
        public int MaydayIncidentCount { get; set; }
    }
}
