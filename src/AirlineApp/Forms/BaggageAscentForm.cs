namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Models;
    using AirlineApp.Services;

    public class BaggageAscentForm : Form
    {
        private readonly Flight currentFlight;
        private readonly Passenger currentPassenger;
        private NumericUpDown numBaggage = null!;
        private ComboBox comboMeal = null!;
        private CheckBox chkWifi = null!;
        private CheckBox chkLounge = null!;
        private CheckBox chkPriority = null!;
        private Label lblBaggageStatus = null!;
        private Label lblBreakdown = null!;

        public BaggageAscentForm(Flight flight, Passenger passenger)
        {
            this.currentFlight = flight;
            this.currentPassenger = passenger;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — v3.0.0 [Ascent Phase - Baggage & In-flight Engine]";
            this.Size = new Size(1000, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(15, 23, 42);
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Header Banner
            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblBadge = new Label
            {
                Text = "v3.0.0 ASCENT & IN-FLIGHT SERVICES",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 185, 129),
                BackColor = Color.FromArgb(6, 78, 59),
                Location = new Point(25, 15),
                AutoSize = true,
                Padding = new Padding(6, 3, 6, 3)
            };

            var lblHeader = new Label
            {
                Text = $"Baggage Manifest, Weight Check & In-Flight Service Customization ({currentFlight.FlightNumber})",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(22, 42),
                AutoSize = true
            };

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);

            // Left Panel: Baggage Calculator
            var grpBaggage = new GroupBox
            {
                Text = "Baggage Weight Check & Allowance",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(25, 105),
                Size = new Size(440, 430),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblWeight = new Label
            {
                Text = "Total Checked Baggage Weight (KG):",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(20, 35),
                AutoSize = true
            };

            numBaggage = new NumericUpDown
            {
                Location = new Point(20, 65),
                Size = new Size(200, 32),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Minimum = 0,
                Maximum = 70,
                Value = (decimal)currentPassenger.BaggageWeightKg,
                DecimalPlaces = 1,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White
            };
            numBaggage.ValueChanged += Recalculate;

            lblBaggageStatus = new Label
            {
                Location = new Point(20, 115),
                Size = new Size(395, 120),
                Font = new Font("Consolas", 10F),
                ForeColor = Color.FromArgb(56, 189, 248),
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(10)
            };

            var lblMeal = new Label
            {
                Text = "In-Flight Meal Preference:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(20, 250),
                AutoSize = true
            };

            comboMeal = new ComboBox
            {
                Location = new Point(20, 280),
                Size = new Size(395, 30),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White
            };
            comboMeal.Items.AddRange(new object[] {
                "Standard Halal Gourmet",
                "Vegetarian / Vegan Meal",
                "Diabetic Friendly Special",
                "Child / Infant Meal",
                "Gluten-Free Executive Menu"
            });
            comboMeal.SelectedIndex = 0;
            comboMeal.SelectedIndexChanged += Recalculate;

            grpBaggage.Controls.Add(lblWeight);
            grpBaggage.Controls.Add(numBaggage);
            grpBaggage.Controls.Add(lblBaggageStatus);
            grpBaggage.Controls.Add(lblMeal);
            grpBaggage.Controls.Add(comboMeal);

            // Right Panel: Add-on Services & Live Cost Breakdown
            var grpAddons = new GroupBox
            {
                Text = "In-Flight Extras & Fare Breakdown",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(485, 105),
                Size = new Size(475, 430),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            chkWifi = new CheckBox
            {
                Text = "High-Speed Satellite Wi-Fi Pass (+$25.00)",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(25, 40),
                AutoSize = true,
                Checked = currentPassenger.WifiPass
            };
            chkWifi.CheckedChanged += Recalculate;

            chkLounge = new CheckBox
            {
                Text = "VIP Departure Executive Lounge Pass (+$45.00)",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(25, 80),
                AutoSize = true,
                Checked = currentPassenger.LoungeAccess
            };
            chkLounge.CheckedChanged += Recalculate;

            chkPriority = new CheckBox
            {
                Text = "Priority Express Boarding & Fast-Track (+$20.00)",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(25, 120),
                AutoSize = true,
                Checked = currentPassenger.PriorityBoarding
            };
            chkPriority.CheckedChanged += Recalculate;

            lblBreakdown = new Label
            {
                Location = new Point(20, 175),
                Size = new Size(435, 235),
                Font = new Font("Consolas", 10F),
                ForeColor = Color.FromArgb(16, 185, 129),
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(12)
            };

            grpAddons.Controls.Add(chkWifi);
            grpAddons.Controls.Add(chkLounge);
            grpAddons.Controls.Add(chkPriority);
            grpAddons.Controls.Add(lblBreakdown);

            // Footer Navigation
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 85,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var btnBack = new Button
            {
                Text = "⇦ BACK TO TAXI",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 65, 85),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(210, 42),
                Location = new Point(25, 20),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) => FormNavigator.Navigate(this, new SeatTaxiForm(currentFlight, currentPassenger));

            var btnProceed = new Button
            {
                Text = "PROCEED TO CRUISING ANALYTICS ➔",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(16, 185, 129),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(310, 42),
                Location = new Point(650, 20),
                Cursor = Cursors.Hand
            };
            btnProceed.FlatAppearance.BorderSize = 0;
            btnProceed.Click += BtnProceed_Click;

            pnlFooter.Controls.Add(btnBack);
            pnlFooter.Controls.Add(btnProceed);

            this.Controls.Add(grpBaggage);
            this.Controls.Add(grpAddons);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);

            UpdateSummary();
        }

        private void Recalculate(object? sender, EventArgs e)
        {
            SoundHelper.PlayTap();
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            currentPassenger.BaggageWeightKg = (double)numBaggage.Value;
            currentPassenger.MealPreference = comboMeal.SelectedItem?.ToString() ?? "Standard Halal";
            currentPassenger.WifiPass = chkWifi.Checked;
            currentPassenger.LoungeAccess = chkLounge.Checked;
            currentPassenger.PriorityBoarding = chkPriority.Checked;

            double allowed = currentPassenger.Cabin switch
            {
                CabinClass.Economy => 20.0,
                CabinClass.Business => 35.0,
                CabinClass.FirstClass => 50.0,
                _ => 20.0
            };

            decimal excessFee = FlightService.CalculateExcessBaggageFee(currentPassenger.BaggageWeightKg, currentPassenger.Cabin);

            lblBaggageStatus.Text = 
                $"CABIN CLASS ALLOWANCE : {allowed:F1} KG\n" +
                $"CURRENT WEIGHT        : {currentPassenger.BaggageWeightKg:F1} KG\n" +
                $"EXCESS WEIGHT         : {Math.Max(0, currentPassenger.BaggageWeightKg - allowed):F1} KG\n" +
                $"EXCESS RATE           : $12.50 / KG\n" +
                $"EXCESS BAGGAGE FEE    : ${excessFee:F2}";

            var booking = FlightService.CalculateFullBooking(currentFlight, currentPassenger);

            lblBreakdown.Text = 
                $"BASE FARE ({currentFlight.FlightNumber}): ${booking.BaseFare:F2}\n" +
                $"CABIN SURCHARGE ({currentPassenger.Cabin}) : ${booking.CabinSurcharge:F2}\n" +
                $"EXCESS BAGGAGE FEE        : ${booking.ExcessBaggageFee:F2}\n" +
                $"IN-FLIGHT ADD-ONS TOTAL    : ${booking.AddonServicesFee:F2}\n" +
                $"AIRPORT AVIATION TAX (12%): ${booking.AirportTax:F2}\n" +
                $"----------------------------------------\n" +
                $"ESTIMATED TOTAL FARE      : ${booking.TotalFare:F2}";
        }

        private void BtnProceed_Click(object? sender, EventArgs e)
        {
            FormNavigator.Navigate(this, new AnalyticsCruisingForm(currentFlight, currentPassenger));
        }
    }
}
