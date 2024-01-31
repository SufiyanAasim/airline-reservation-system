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
        private System.Windows.Forms.Timer telemetryTimer = null!;
        private Random rnd = new Random();

        private int altitude = 38000;
        private double machSpeed = 0.82;
        private int oat = -54;

        public AnalyticsCruisingForm(Flight flight, Passenger passenger)
        {
            this.currentFlight = flight;
            this.currentPassenger = passenger;
            InitializeComponent();
            IconHelper.ApplyIcon(this);
            StartTelemetryAnimation();
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System";
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
                Text = "v6.0.0 Mayday",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(225, 29, 72),
                Location = new Point(25, 15),
                AutoSize = true,
                Padding = new Padding(8, 4, 8, 4)
            };

            var lblHeader = new Label
            {
                Text = $"Real-Time Telemetry & Fleet Load Factor Bar Chart Canvas ({currentFlight.FlightNumber})",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(22, 45),
                AutoSize = true
            };

            var btnCreditsHeader = new Button
            {
                Text = "⭐ SYSTEM CREDITS",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(139, 92, 246),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(170, 38),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(780, 25),
                Cursor = Cursors.Hand
            };
            btnCreditsHeader.FlatAppearance.BorderSize = 0;
            btnCreditsHeader.Click += (s, e) =>
            {
                SoundHelper.PlayTap();
                FormNavigator.Navigate(this, new CreditsForm(this));
            };

            var btnExitHeader = new Button
            {
                Text = "❌ EXIT SYSTEM",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(225, 29, 72),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(140, 38),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(965, 25),
                Cursor = Cursors.Hand
            };
            btnExitHeader.FlatAppearance.BorderSize = 0;
            btnExitHeader.Click += (s, e) =>
            {
                SoundHelper.PlayTap();
                Application.Exit();
            };

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);
            headerPanel.Controls.Add(btnCreditsHeader);
            headerPanel.Controls.Add(btnExitHeader);

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
                Size = new Size(190, 45),
                Location = new Point(25, 20),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) =>
            {
                SoundHelper.PlayTap();
                FormNavigator.Navigate(this, new BaggageAscentForm(currentFlight, currentPassenger));
            };

            var btnReports = new Button
            {
                Text = "EXECUTIVE REPORT BUILDER 📊",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(139, 92, 246),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(270, 45),
                Location = new Point(230, 20),
                Cursor = Cursors.Hand
            };
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.Click += (s, e) =>
            {
                SoundHelper.PlayTap();
                FormNavigator.Navigate(this, new ReportGenerationForm(FlightService.CalculateFullBooking(currentFlight, currentPassenger)));
            };

            var btnProceed = new Button
            {
                Text = "PROCEED TO TOUCHDOWN ➔",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(14, 165, 233),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(260, 45),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(640, 20),
                Cursor = Cursors.Hand
            };
            btnProceed.FlatAppearance.BorderSize = 0;
            btnProceed.Click += BtnProceed_Click;

            var btnExitFooter = new Button
            {
                Text = "❌ EXIT",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(225, 29, 72),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(130, 45),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(950, 20),
                Cursor = Cursors.Hand
            };
            btnExitFooter.FlatAppearance.BorderSize = 0;
            btnExitFooter.Click += (s, e) =>
            {
                SoundHelper.PlayTap();
                Application.Exit();
            };

            pnlFooter.Resize += (s, e) =>
            {
                btnExitFooter.Location = new Point(pnlFooter.Width - btnExitFooter.Width - 25, 20);
                btnProceed.Location = new Point(btnExitFooter.Left - btnProceed.Width - 15, 20);
            };

            pnlFooter.Controls.Add(btnBack);
            pnlFooter.Controls.Add(btnReports);
            pnlFooter.Controls.Add(btnProceed);
            pnlFooter.Controls.Add(btnExitFooter);

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
                Text = "Cruising Flight Telemetry (LIVE RADAR ACTIVE)",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 15, 0),
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            lblTelemetryData = new Label
            {
                Font = new Font("Consolas", 11F),
                ForeColor = Color.FromArgb(56, 189, 248),
                Dock = DockStyle.Fill
            };

            grpTelemetry.Controls.Add(lblTelemetryData);

            // Right Section: Custom GDI+ Load Factor Bar Chart Canvas
            var grpChart = new GroupBox
            {
                Text = "Custom GDI+ Load Factor & Seat Capacity Canvas",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(15, 0, 0, 0),
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            pnlChartCanvas = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlChartCanvas.Paint += PnlChartCanvas_Paint;
            pnlChartCanvas.Resize += (s, e) => pnlChartCanvas.Invalidate();

            grpChart.Controls.Add(pnlChartCanvas);

            pnlMain.Controls.Add(grpTelemetry, 0, 0);
            pnlMain.Controls.Add(grpChart, 1, 0);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);

            UpdateTelemetryDisplay();
        }

        private void StartTelemetryAnimation()
        {
            telemetryTimer = new System.Windows.Forms.Timer { Interval = 500 };
            telemetryTimer.Tick += (s, e) =>
            {
                altitude = 38000 + rnd.Next(-30, 30);
                machSpeed = 0.82 + (rnd.Next(-1, 2) * 0.005);
                oat = -54 + rnd.Next(-1, 2);
                UpdateTelemetryDisplay();
            };
            telemetryTimer.Start();

            this.FormClosing += (s, e) => telemetryTimer.Stop();
        }

        private void UpdateTelemetryDisplay()
        {
            lblTelemetryData.Text =
                $"CRUISING ALTITUDE : {altitude:N0} FT (FL380)\n\n" +
                $"AIRSPEED          : {machSpeed * 1085:F0} KM/H (MACH {machSpeed:F2})\n\n" +
                $"OUTSIDE AIR TEMP  : {oat} °C\n\n" +
                $"CABIN PRESSURE    : 10.9 PSI (8,000 FT EQUIV)\n\n" +
                $"FUEL BURN RATE    : 2,450 KG / HOUR\n\n" +
                $"GROSS TAKEOFF WT  : 64,200 KG\n\n" +
                $"WIND VECTOR       : 240° AT 42 KTS (HEADWIND)\n\n" +
                $"FLIGHT STATUS     : ON TIME / LIVE RADAR SYNCED\n\n" +
                $"PASSENGER MANIFEST: CONFIRMED FOR {currentPassenger.FullName.ToUpper()}";
        }

        private void PnlChartCanvas_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int width = pnlChartCanvas.Width;
            int height = pnlChartCanvas.Height;

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

            int barWidth = Math.Max(35, (width - 80) / (routes.Length * 2));
            int gap = (width - 60 - (routes.Length * barWidth)) / (routes.Length + 1);
            int startX = 40;
            int maxBarHeight = height - 110;

            using (Font fTitle = new Font("Segoe UI", 11F, FontStyle.Bold))
            using (Brush bTitle = new SolidBrush(Color.FromArgb(226, 232, 240)))
            {
                g.DrawString("Fleet Seat Load Factor % (Active Flight Operations)", fTitle, bTitle, 20, 15);
            }

            for (int i = 0; i < routes.Length; i++)
            {
                int val = loadFactors[i];
                int bHeight = Math.Max(20, (int)((val / 100.0) * maxBarHeight));
                int x = startX + i * (barWidth + gap);
                int y = height - 55 - bHeight;

                using (Brush bBar = new SolidBrush(barColors[i]))
                {
                    g.FillRectangle(bBar, x, y, barWidth, bHeight);
                }

                using (Font font = new Font("Segoe UI", 9.5F, FontStyle.Bold))
                using (Brush bText = new SolidBrush(Color.White))
                {
                    g.DrawString($"{val}%", font, bText, x + (barWidth / 4), y - 24);
                    g.DrawString(routes[i], font, bText, x, height - 42);
                }
            }
        }

        private void BtnProceed_Click(object? sender, EventArgs e)
        {
            SoundHelper.PlayTap();
            var booking = FlightService.CalculateFullBooking(currentFlight, currentPassenger);
            FormNavigator.Navigate(this, new ReceiptTouchdownForm(booking));
        }
    }
}
