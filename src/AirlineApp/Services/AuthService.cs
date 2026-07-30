namespace AirlineApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AirlineApp.Models;

    public static class AuthService
    {
        private static readonly List<User> RegisteredUsers = new()
        {
            new User { FullName = "Capt. Sufiyan Aasim", Email = "sufiyanaasim@outlook.com", Password = "admin123", Role = "Flight Operations Manager" }
        };

        public static User? CurrentUser { get; set; } = RegisteredUsers[0];

        public static (bool Success, string Message, User? User) Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "Email and Password cannot be empty.", null);
            }

            var user = RegisteredUsers.FirstOrDefault(u => u.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                return (false, "Account not found. Please check your email or Sign Up.", null);
            }

            if (user.Password != password)
            {
                return (false, "Invalid password provided. Please try again.", null);
            }

            CurrentUser = user;
            return (true, "Authentication successful. Welcome aboard!", user);
        }

        public static (bool Success, string Message) Register(string fullName, string email, string password, string confirmPassword, string role)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "All mandatory fields must be completed.");
            }

            if (password != confirmPassword)
            {
                return (false, "Passwords do not match. Please re-enter.");
            }

            if (RegisteredUsers.Any(u => u.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                return (false, "An account with this email address already exists.");
            }

            var newUser = new User
            {
                FullName = fullName.Trim(),
                Email = email.Trim(),
                Password = password,
                Role = role
            };

            RegisteredUsers.Add(newUser);
            CurrentUser = newUser;
            return (true, "Registration successful! You are now logged in.");
        }
    }
}
