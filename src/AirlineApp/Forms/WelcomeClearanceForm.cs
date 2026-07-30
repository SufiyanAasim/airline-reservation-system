namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Models;
    using AirlineApp.Services;

    public class WelcomeClearanceForm : Form
    {
        private ComboBox comboFlights = null!;
        private TextBox txtFullName = null!;
        private TextBox txtPassport = null!;
        private TextBox txtPhone = null!;
        private TextBox txtEmail = null!;
        private Label lblRouteDetails = null!;

        public WelcomeClearanceForm()
        {
            InitializeComponent();
            IconHelper.ApplyIcon(this);
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — v1.0.0 [Clearance Phase]";
            this.Size = new Size(1150, 750);
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(15, 23, 42);
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;

            // Header Banner (Dock = Top)
            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblBadge = new Label
            {
                Text = "v1.0.0 CLEARANCE PHASE",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(14, 165, 233),
                BackColor = Color.FromArgb(12, 74, 110),
                Location = new Point(25, 15),
                AutoSize = true,
                Padding = new Padding(6, 3, 6, 3)
            };

            var lblHeader = new Label
            {
                Text = "Flight Registration & Departure Clearance Engine",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
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
                Location = new Point(940, 25),
                Cursor = Cursors.Hand
            };
            btnCreditsHeader.FlatAppearance.BorderSize = 0;
            btnCreditsHeader.Click += (s, e) =>
            {
                var flight = (Flight)comboFlights.SelectedItem!;
                var passenger = new Passenger { FullName = txtFullName.Text.Trim(), PassportOrId = txtPassport.Text.Trim() };
                FormNavigator.Navigate(this, new MaydayCreditsForm(FlightService.CalculateFullBooking(flight, passenger)));
            };

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);
            headerPanel.Controls.Add(btnCreditsHeader);

            // Navigation Bar / Action Button Footer (Dock = Bottom)
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 85,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var btnLogout = new Button
            {
                Text = "⇦ LOGOUT / PORTAL HOME",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 65, 85),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(230, 45),
                Location = new Point(25, 20),
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) => FormNavigator.Navigate(this, new LoginForm());

            var btnProceed = new Button
            {
                Text = "PROCEED TO TAXI & SEAT SELECTION ➔",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(14, 165, 233),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(340, 45),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(760, 20),
                Cursor = Cursors.Hand
            };
            btnProceed.FlatAppearance.BorderSize = 0;
            btnProceed.Click += BtnProceed_Click;

            pnlFooter.Resize += (s, e) =>
            {
                btnProceed.Location = new Point(pnlFooter.Width - btnProceed.Width - 30, 20);
            };

            pnlFooter.Controls.Add(btnLogout);
            pnlFooter.Controls.Add(btnProceed);

            // Main Content Panel (Dock = Fill)
            var pnlMain = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(25, 15, 25, 15),
                BackColor = Color.FromArgb(15, 23, 42)
            };
            pnlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
            pnlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));

            // Left Section: Passenger Registration
            var grpPassenger = new GroupBox
            {
                Text = "Passenger Information",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 15, 0),
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            int y = 35;
            AddInputField(grpPassenger, "Full Name:", ref txtFullName, "Capt. Sufiyan Aasim", ref y);
            AddInputField(grpPassenger, "Passport / CNIC No:", ref txtPassport, "PK-98234109", ref y);
            AddInputField(grpPassenger, "Phone Number:", ref txtPhone, "+92 300 1234567", ref y);
            AddInputField(grpPassenger, "Email Address:", ref txtEmail, "sufiyanaasim@outlook.com", ref y);

            // Right Section: Flight Selection & Clearance Status
            var grpFlight = new GroupBox
            {
                Text = "Flight Clearance & Aircraft Info",
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
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblSelectFlight = new Label
            {
                Text = "Select Available Flight:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Dock = DockStyle.Top,
                Height = 25
            };

            comboFlights = new ComboBox
            {
                Dock = DockStyle.Top,
                Height = 32,
                Font = new Font("Segoe UI", 10.5F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White
            };

            foreach (var f in FlightService.GetFlights())
            {
                comboFlights.Items.Add(f);
            }
            comboFlights.SelectedIndex = 0;
            comboFlights.SelectedIndexChanged += ComboFlights_SelectedIndexChanged;

            // Spacing label
            var pnlSpacer = new Panel { Dock = DockStyle.Top, Height = 15, BackColor = Color.Transparent };

            // Details Container (STRETCHES FULLY TO FILL RIGHT GROUPBOX!)
            var pnlFlightDetails = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(20)
            };

            lblRouteDetails = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 11.5F),
                ForeColor = Color.FromArgb(56, 189, 248)
            };
            pnlFlightDetails.Controls.Add(lblRouteDetails);

            pnlFlightContainer.Controls.Add(pnlFlightDetails);
            pnlFlightContainer.Controls.Add(pnlSpacer);
            pnlFlightContainer.Controls.Add(comboFlights);
            pnlFlightContainer.Controls.Add(lblSelectFlight);

            grpFlight.Controls.Add(pnlFlightContainer);

            pnlMain.Controls.Add(grpPassenger, 0, 0);
            pnlMain.Controls.Add(grpFlight, 1, 0);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);

            UpdateFlightSummary();
        }

        private void AddInputField(GroupBox parent, string labelText, ref TextBox txt, string defaultVal, ref int y)
        {
            var lbl = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(20, y),
                AutoSize = true
            };
            txt = new TextBox
            {
                Text = defaultVal,
                Font = new Font("Segoe UI", 11F),
                Location = new Point(20, y + 26),
                Size = new Size( parent.Width - 40, 32),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);
            y += 72;
        }

        private void ComboFlights_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateFlightSummary();
        }

        private void UpdateFlightSummary()
        {
            if (comboFlights.SelectedItem is Flight f)
            {
                lblRouteDetails.Text = 
                    $"AIRLINE OPERATOR : {f.Airline}\n\n" +
                    $"FLIGHT NUMBER    : {f.FlightNumber}\n\n" +
                    $"ORIGIN DEPARTURE : {f.Origin} ({f.OriginCode})\n\n" +
                    $"DESTINATION      : {f.Destination} ({f.DestinationCode})\n\n" +
                    $"SCHEDULED DEPART : {f.DepartureTime}\n\n" +
                    $"AIRCRAFT MODEL   : {f.AircraftType}\n\n" +
                    $"FLIGHT DISTANCE  : {f.DistanceKm} KM\n\n" +
                    $"BASE FARE RATE   : ${f.BaseFare:F2}\n\n" +
                    $"DEPART CLEARANCE : APPROVED / READY FOR TAXI";
            }
        }

        private void BtnProceed_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtPassport.Text))
            {
                CustomMessageBox.Show("CLEARANCE ERROR", "Passenger Full Name and Passport/ID are required for departure clearance.", true);
                return;
            }

            var passenger = new Passenger
            {
                FullName = txtFullName.Text.Trim(),
                PassportOrId = txtPassport.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            var selectedFlight = (Flight)comboFlights.SelectedItem!;
            FormNavigator.Navigate(this, new SeatTaxiForm(selectedFlight, passenger));
        }
    }
}
