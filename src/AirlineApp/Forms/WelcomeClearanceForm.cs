namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Models;
    using AirlineApp.Services;

    public class WelcomeClearanceForm : Form
    {
        private TextBox txtFullName = null!;
        private TextBox txtPassport = null!;
        private ComboBox comboFlights = null!;
        private Label lblFlightDetails = null!;
        private Label lblTicker = null!;
        private System.Windows.Forms.Timer tickerTimer = null!;
        private int tickerIndex = 0;

        private static readonly string[] TickerMessages = new string[]
        {
            "🟢 ALL DEPARTURE GATES OPERATIONAL",
            "✈️ PK-301 KARACHI TO ISLAMABAD BOARDING NOW",
            "🌤️ EN-ROUTE WEATHER: CLEAR / SMOOTH CRUISING",
            "🛡️ AUTOMATED FLIGHT DATA RECORDER SYNCED"
        };

        public WelcomeClearanceForm()
        {
            InitializeComponent();
            IconHelper.ApplyIcon(this);
            StartTickerAnimation();
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
                Height = 95,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblBadge = new Label
            {
                Text = "v6.0.0 Mayday",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(225, 29, 72),
                Location = new Point(25, 12),
                AutoSize = true,
                Padding = new Padding(8, 4, 8, 4)
            };

            var lblHeader = new Label
            {
                Text = "Flight Departure Clearance & Passenger Manifest Dispatch",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(22, 40),
                AutoSize = true
            };

            lblTicker = new Label
            {
                Text = TickerMessages[0],
                Font = new Font("Consolas", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(14, 165, 233),
                Location = new Point(24, 70),
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
            headerPanel.Controls.Add(lblTicker);
            headerPanel.Controls.Add(btnCreditsHeader);
            headerPanel.Controls.Add(btnExitHeader);

            // Footer Navigation Bar (Dock = Bottom)
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 85,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var btnLogout = new Button
            {
                Text = "🚪 LOGOUT / RETURN TO LOGIN",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 65, 85),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(260, 45),
                Location = new Point(25, 20),
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) =>
            {
                SoundHelper.PlayTap();
                AuthService.Logout();
                FormNavigator.Navigate(this, new LoginForm());
            };

            var btnProceed = new Button
            {
                Text = "PROCEED TO TAXI & SEAT ALLOCATION ➔",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(14, 165, 233),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(330, 45),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(600, 20),
                Cursor = Cursors.Hand
            };
            btnProceed.FlatAppearance.BorderSize = 0;
            btnProceed.Click += BtnProceed_Click;

            var btnExitFooter = new Button
            {
                Text = "❌ EXIT",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
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

            pnlFooter.Controls.Add(btnLogout);
            pnlFooter.Controls.Add(btnProceed);
            pnlFooter.Controls.Add(btnExitFooter);

            // Main Content Layout Container (Dock = Fill)
            var pnlMain = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(25, 15, 25, 15),
                BackColor = Color.FromArgb(15, 23, 42)
            };
            pnlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Left Section: Passenger Identification Form
            var grpPassenger = new GroupBox
            {
                Text = "Passenger Identification & Clearance Manifest",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 15, 0),
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblName = new Label
            {
                Text = "Passenger Full Legal Name:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(20, 35),
                AutoSize = true
            };

            txtFullName = new TextBox
            {
                Text = AuthService.CurrentUser?.FullName ?? "Capt. Sufiyan Aasim",
                Font = new Font("Segoe UI", 11F),
                Location = new Point(20, 65),
                Size = new Size(460, 32),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblPassport = new Label
            {
                Text = "CNIC / Passport Identification Number:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(20, 120),
                AutoSize = true
            };

            txtPassport = new TextBox
            {
                Text = "42101-9876543-1",
                Font = new Font("Segoe UI", 11F),
                Location = new Point(20, 150),
                Size = new Size(460, 32),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            grpPassenger.Controls.Add(lblName);
            grpPassenger.Controls.Add(txtFullName);
            grpPassenger.Controls.Add(lblPassport);
            grpPassenger.Controls.Add(txtPassport);

            // Right Section: Flight Schedule Selection
            var grpFlight = new GroupBox
            {
                Text = "Available Scheduled Commercial Flights",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(15, 0, 0, 0),
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var pnlFlightContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblSelectFlight = new Label
            {
                Text = "Select Active Commercial Route:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(15, 10),
                AutoSize = true
            };

            comboFlights = new ComboBox
            {
                Location = new Point(15, 38),
                Size = new Size(420, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10.5F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White
            };

            pnlFlightContainer.Resize += (s, e) =>
            {
                comboFlights.Width = pnlFlightContainer.Width - 30;
            };

            var flights = FlightService.GetFlights();
            foreach (var f in flights)
            {
                comboFlights.Items.Add($"{f.FlightNumber} — {f.Airline} ({f.OriginCode} ➔ {f.DestinationCode})");
            }
            comboFlights.SelectedIndex = 0;
            comboFlights.SelectedIndexChanged += ComboFlights_SelectedIndexChanged;

            lblFlightDetails = new Label
            {
                Font = new Font("Consolas", 11F),
                ForeColor = Color.FromArgb(56, 189, 248),
                Location = new Point(15, 85),
                Size = new Size(450, 260),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            pnlFlightContainer.Controls.Add(lblSelectFlight);
            pnlFlightContainer.Controls.Add(comboFlights);
            pnlFlightContainer.Controls.Add(lblFlightDetails);

            grpFlight.Controls.Add(pnlFlightContainer);

            pnlMain.Controls.Add(grpPassenger, 0, 0);
            pnlMain.Controls.Add(grpFlight, 1, 0);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);

            UpdateFlightDetailsCard();
        }

        private void StartTickerAnimation()
        {
            tickerTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            tickerTimer.Tick += (s, e) =>
            {
                tickerIndex = (tickerIndex + 1) % TickerMessages.Length;
                lblTicker.Text = TickerMessages[tickerIndex];
            };
            tickerTimer.Start();

            this.FormClosing += (s, e) => tickerTimer.Stop();
        }

        private void ComboFlights_SelectedIndexChanged(object? sender, EventArgs e)
        {
            SoundHelper.PlayTap();
            UpdateFlightDetailsCard();
        }

        private void UpdateFlightDetailsCard()
        {
            var flights = FlightService.GetFlights();
            if (comboFlights.SelectedIndex >= 0 && comboFlights.SelectedIndex < flights.Count)
            {
                var f = flights[comboFlights.SelectedIndex];
                lblFlightDetails.Text = 
                    $"AIRLINE        : {f.Airline}\n\n" +
                    $"FLIGHT NUMBER  : {f.FlightNumber}\n\n" +
                    $"AIRCRAFT MODEL : {f.AircraftType}\n\n" +
                    $"ORIGIN         : {f.Origin} ({f.OriginCode})\n\n" +
                    $"DESTINATION    : {f.Destination} ({f.DestinationCode})\n\n" +
                    $"SCHEDULED DEP  : {f.DepartureTime}\n\n" +
                    $"BASE FARE RATE : ${f.BaseFare:F2}";
            }
        }

        private void BtnProceed_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                CustomMessageBox.Show("VALIDATION ERROR", "Please enter passenger legal name.", true);
                return;
            }

            var flights = FlightService.GetFlights();
            var selectedFlight = flights[comboFlights.SelectedIndex];

            var passenger = new Passenger
            {
                FullName = txtFullName.Text.Trim(),
                PassportOrId = txtPassport.Text.Trim(),
                Cabin = CabinClass.Economy,
                SeatNumber = "01A"
            };

            SoundHelper.PlayTap();
            FormNavigator.Navigate(this, new SeatTaxiForm(selectedFlight, passenger));
        }
    }
}
