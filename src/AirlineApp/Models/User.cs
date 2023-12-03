namespace AirlineApp.Models
{
    using System;

    public class User
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Passenger"; // Passenger or Flight Operations Manager
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
