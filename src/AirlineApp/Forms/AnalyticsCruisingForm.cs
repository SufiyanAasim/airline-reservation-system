namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Models;
    using AirlineApp.Services;

    public class AnalyticsCruisingForm : Form
    {
        private readonly Flight currentFlight;
        private readonly Passenger currentPassenger;
        private Panel pnlChartCanvas = null!;
        private Label lblTelemetry = null!;
        private Label lblPricingMetrics = null!;

        public AnalyticsCruisingForm(Flight flight, Passenger passenger)
        {
            this.currentFlight = flight;
            this.currentPassenger = passenger;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — v4.0.0 [Cruising Phase - Dynamic Analytics]";
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
                Text = "v4.0.0 CRUISING & REVENUE ANALYTICS",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(168, 85, 247),
                BackColor = Color.FromArgb(88, 28, 135),
                Location = new Point(25, 15),
                AutoSize = true,
                Padding = new Padding(6, 3, 6, 3)
            };

            var lblHeader = new Label
            {
                Text = $"Real-Time Telemetry & Yield Revenue Dashboard ({currentFlight.FlightNumber})",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(22, 42),
                AutoSize = true
            };

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);

            // Left Section: Telemetry & Dynamic Pricing
            var grpTelemetry = new GroupBox
            {
                Text = "Flight Telemetry & Revenue Metrics",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(25, 105),
                Size = new Size(420, 430),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            lblTelemetry = new Label
            {
                Location = new Point(20, 35),
                Size = new Size(380, 150),
                Font = new Font("Consolas", 10F),
                ForeColor = Color.FromArgb(56, 189, 248),
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(12)
            };

            lblTelemetry.Text = 
                "FLIGHT PHASE     : CRUISING AT FL380\n" +
                "ALTITUDE         : 38,000 FT\n" +
                "AIRSPEED         : 890 KM/H (MACH 0.82)\n" +
                "OUTSIDE TEMP     : -54° C\n" +
                "CABIN PRESSURE   : 0.82 ATM\n" +
                "FUEL BURN RATE   : 2,450 KG / HOUR\n" +
                "GROSS LOAD FACTOR: 84.6% CAPACITY";

            lblPricingMetrics = new Label
            {
                Location = new Point(20, 205),
                Size = new Size(380, 205),
                Font = new Font("Consolas", 10F),
                ForeColor = Color.FromArgb(251, 191, 36),
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(12)
            };

            var booking = FlightService.CalculateFullBooking(currentFlight, currentPassenger);
            lblPricingMetrics.Text = 
                "DYNAMIC YIELD PRICING MODEL:\n" +
                "----------------------------------------\n" +
                $"BASE ROUTE FARE  : ${currentFlight.BaseFare:F2}\n" +
                $"DEMAND SURGE MULT: 1.15x (HIGH SEAS)\n" +
                $"CABIN YIELD SURGE: ${booking.CabinSurcharge:F2}\n" +
                $"TAXES & FEES     : ${booking.AirportTax + booking.ExcessBaggageFee + booking.AddonServicesFee:F2}\n" +
                $"FINAL TOTAL FARE : ${booking.TotalFare:F2}\n" +
                "PROFITABILITY    : OPTIMAL (+28.4%)";

            grpTelemetry.Controls.Add(lblTelemetry);
            grpTelemetry.Controls.Add(lblPricingMetrics);

            // Right Section: Custom GDI+ Rendered Chart Canvas
            var grpChart = new GroupBox
            {
                Text = "Interactive Cabin Capacity & Load Factor Breakdown",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(465, 105),
                Size = new Size(495, 430),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            pnlChartCanvas = new Panel
            {
                Location = new Point(20, 35),
                Size = new Size(455, 375),
                BackColor = Color.FromArgb(15, 23, 42)
            };
            pnlChartCanvas.Paint += PnlChartCanvas_Paint;

            grpChart.Controls.Add(pnlChartCanvas);

            // Footer Navigation
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 85,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var btnBack = new Button
            {
                Text = "⇦ BACK TO ASCENT",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 65, 85),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(180, 42),
                Location = new Point(25, 20),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) => FormNavigator.Navigate(this, new BaggageAscentForm(currentFlight, currentPassenger));

            var btnReport = new Button
            {
                Text = "EXECUTIVE REPORT BUILDER 📊",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(168, 85, 247),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(240, 42),
                Location = new Point(390, 20),
                Cursor = Cursors.Hand
            };
            btnReport.FlatAppearance.BorderSize = 0;
            btnReport.Click += (s, e) => FormNavigator.Navigate(this, new ReportGenerationForm(FlightService.CalculateFullBooking(currentFlight, currentPassenger)));

            var btnProceed = new Button
            {
                Text = "PROCEED TO TOUCHDOWN RECEIPT ➔",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(14, 165, 233),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(300, 42),
                Location = new Point(650, 20),
                Cursor = Cursors.Hand
            };
            btnProceed.FlatAppearance.BorderSize = 0;
            btnProceed.Click += (s, e) => FormNavigator.Navigate(this, new ReceiptTouchdownForm(FlightService.CalculateFullBooking(currentFlight, currentPassenger)));

            pnlFooter.Controls.Add(btnBack);
            pnlFooter.Controls.Add(btnReport);
            pnlFooter.Controls.Add(btnProceed);

            this.Controls.Add(grpTelemetry);
            this.Controls.Add(grpChart);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);
        }

        private void PnlChartCanvas_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var titleFont = new Font("Segoe UI", 11F, FontStyle.Bold);
            var labelFont = new Font("Segoe UI", 9F, FontStyle.Bold);
            var valFont = new Font("Consolas", 10F, FontStyle.Bold);

            g.DrawString("Cabin Load Factor & Capacity Metrics", titleFont, Brushes.White, 20, 15);

            // Bar Chart Parameters
            string[] categories = { "Economy (98/120)", "Business (32/40)", "First Class (12/15)" };
            float[] percentages = { 81.6f, 80.0f, 80.0f };
            Brush[] brushes = {
                Brushes.DeepSkyBlue,
                Brushes.MediumPurple,
                Brushes.Gold
            };

            int startY = 65;
            int barHeight = 35;
            int maxBarWidth = 260;

            for (int i = 0; i < categories.Length; i++)
            {
                g.DrawString(categories[i], labelFont, Brushes.LightGray, 20, startY + i * 75);

                // Background Track
                g.FillRectangle(new SolidBrush(Color.FromArgb(30, 41, 59)), 20, startY + 25 + i * 75, maxBarWidth, barHeight);

                // Filled Bar
                float filledWidth = maxBarWidth * (percentages[i] / 100.0f);
                g.FillRectangle(brushes[i], 20, startY + 25 + i * 75, filledWidth, barHeight);

                // Percentage Text
                g.DrawString($"{percentages[i]:F1}%", valFont, Brushes.White, 290, startY + 30 + i * 75);
            }

            // Summary Indicator Box
            g.FillRectangle(new SolidBrush(Color.FromArgb(30, 41, 59)), 20, 295, 415, 60);
            g.DrawRectangle(new Pen(Color.FromArgb(56, 189, 248), 1), 20, 295, 415, 60);
            g.DrawString("AVERAGE FLEET LOAD FACTOR: 84.6%", labelFont, Brushes.SkyBlue, 35, 305);
            g.DrawString("OPTIMAL YIELD TARGET ACHIEVED — NO OVERBOOKING RISK", new Font("Segoe UI", 8F), Brushes.LightGreen, 35, 328);
        }
    }
}
