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
        private Label lblTelemetryData = null!;

        public AnalyticsCruisingForm(Flight flight, Passenger passenger)
        {
            this.currentFlight = flight;
            this.currentPassenger = passenger;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — v4.0.0 [Cruising Phase - Flight Telemetry & Yield Analytics]";
            this.Size = new Size(1150, 750);
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(15, 23, 42);
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;

            // Header Banner
            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblBadge = new Label
            {
                Text = "v4.0.0 CRUISING & YIELD ANALYTICS",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(56, 189, 248),
                BackColor = Color.FromArgb(12, 74, 110),
                Location = new Point(25, 15),
                AutoSize = true,
                Padding = new Padding(6, 3, 6, 3)
            };

            var lblHeader = new Label
            {
                Text = $"Real-Time Telemetry & Fleet Load Factor Bar Chart Canvas ({currentFlight.FlightNumber})",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(22, 45),
                AutoSize = true
            };

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);

            // Main Content Layout
            var pnlMain = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(25, 15, 25, 15),
                BackColor = Color.FromArgb(15, 23, 42)
            };
            pnlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            pnlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));

            // Left Section: Cruising Telemetry Metrics
            var grpTelemetry = new GroupBox
            {
                Text = "Cruising Flight Telemetry",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 15, 0),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            lblTelemetryData = new Label
            {
                Font = new Font("Consolas", 10.5F),
                ForeColor = Color.FromArgb(56, 189, 248),
                Location = new Point(20, 35),
                Size = new Size(410, 380),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            lblTelemetryData.Text = 
                $"CRUISING ALTITUDE : 38,000 FT (FL380)\n" +
                $"AIRSPEED          : 890 KM/H (MACH 0.82)\n" +
                $"OUTSIDE AIR TEMP  : -54 °C\n" +
                $"CABIN PRESSURE    : 10.9 PSI (8,000 FT EQUIV)\n" +
                $"FUEL BURN RATE    : 2,450 KG / HOUR\n" +
                $"GROSS TAKEOFF WT  : 64,200 KG\n" +
                $"WIND VECTOR       : 240° AT 42 KTS (HEADWIND)\n" +
                $"FLIGHT STATUS     : ON TIME / SMOOTH CRUISING\n" +
                $"PASSENGER MANIFEST: CONFIRMED FOR {currentPassenger.FullName.ToUpper()}\n" +
                $"REVENUE AUDIT     : REAL-TIME YIELD RECORDED";

            grpTelemetry.Controls.Add(lblTelemetryData);

            // Right Section: Custom GDI+ Load Factor Bar Chart Canvas
            var grpChart = new GroupBox
            {
                Text = "Custom GDI+ Load Factor & Seat Capacity Canvas",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(15, 0, 0, 0),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            pnlChartCanvas = new Panel
            {
                Location = new Point(20, 35),
                Size = new Size(500, 380),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(15, 23, 42),
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlChartCanvas.Paint += PnlChartCanvas_Paint;

            grpChart.Controls.Add(pnlChartCanvas);

            pnlMain.Controls.Add(grpTelemetry, 0, 0);
            pnlMain.Controls.Add(grpChart, 1, 0);

            // Footer Navigation
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 90,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var btnBack = new Button
            {
                Text = "⇦ BACK TO ASCENT",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 65, 85),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(180, 45),
                Location = new Point(25, 20),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) => FormNavigator.Navigate(this, new BaggageAscentForm(currentFlight, currentPassenger));

            var btnReports = new Button
            {
                Text = "OPEN EXECUTIVE REPORT BUILDER 📊",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(139, 92, 246),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(290, 45),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(480, 20),
                Cursor = Cursors.Hand
            };
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.Click += (s, e) => FormNavigator.Navigate(this, new ReportGenerationForm(FlightService.CalculateFullBooking(currentFlight, currentPassenger)));

            var btnProceed = new Button
            {
                Text = "PROCEED TO TOUCHDOWN ➔",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(14, 165, 233),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(240, 45),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(780, 20),
                Cursor = Cursors.Hand
            };
            btnProceed.FlatAppearance.BorderSize = 0;
            btnProceed.Click += BtnProceed_Click;

            pnlFooter.Controls.Add(btnBack);
            pnlFooter.Controls.Add(btnReports);
            pnlFooter.Controls.Add(btnProceed);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);
        }

        private void PnlChartCanvas_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int width = pnlChartCanvas.Width;
            int height = pnlChartCanvas.Height;

            // Draw Bar Chart: Seat Occupancy per Flight Route
            string[] routes = { "PK-301", "PK-302", "PA-200", "ER-501", "EK-605", "QR-611" };
            int[] loadFactors = { 88, 75, 92, 64, 98, 82 };
            Color[] barColors = {
                Color.FromArgb(14, 165, 233),
                Color.FromArgb(16, 185, 129),
                Color.FromArgb(245, 158, 11),
                Color.FromArgb(139, 92, 246),
                Color.FromArgb(225, 29, 72),
                Color.FromArgb(6, 182, 212)
            };

            int barWidth = 45;
            int gap = (width - 60 - (routes.Length * barWidth)) / (routes.Length + 1);
            int startX = 50;
            int maxBarHeight = height - 100;

            // Title text
            using (Font fTitle = new Font("Segoe UI", 10F, FontStyle.Bold))
            using (Brush bTitle = new SolidBrush(Color.FromArgb(226, 232, 240)))
            {
                g.DrawString("Fleet Seat Load Factor % (Active Flight Operations)", fTitle, bTitle, 15, 10);
            }

            for (int i = 0; i < routes.Length; i++)
            {
                int val = loadFactors[i];
                int bHeight = (int)((val / 100.0) * maxBarHeight);
                int x = startX + i * (barWidth + gap);
                int y = height - 50 - bHeight;

                // Draw Bar
                using (Brush bBar = new SolidBrush(barColors[i]))
                {
                    g.FillRectangle(bBar, x, y, barWidth, bHeight);
                }

                // Draw Label & %
                using (Font font = new Font("Segoe UI", 8.5F, FontStyle.Bold))
                using (Brush bText = new SolidBrush(Color.White))
                {
                    g.DrawString($"{val}%", font, bText, x + 6, y - 20);
                    g.DrawString(routes[i], font, bText, x + 2, height - 38);
                }
            }
        }

        private void BtnProceed_Click(object? sender, EventArgs e)
        {
            var booking = FlightService.CalculateFullBooking(currentFlight, currentPassenger);
            FormNavigator.Navigate(this, new ReceiptTouchdownForm(booking));
        }
    }
}
