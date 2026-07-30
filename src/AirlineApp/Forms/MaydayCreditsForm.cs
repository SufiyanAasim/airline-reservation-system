namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Models;
    using AirlineApp.Services;

    public class MaydayCreditsForm : Form
    {
        private readonly Booking currentBooking;
        private Label lblMaydayStatus = null!;
        private Button btnTriggerMayday = null!;
        private bool isMaydayActive = false;

        public MaydayCreditsForm(Booking booking)
        {
            this.currentBooking = booking;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — v6.0.0 [Mayday & System Credits Phase]";
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
                Text = "v6.0.0 MAYDAY PROTOCOL & SYSTEM CREDITS",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(225, 29, 72),
                BackColor = Color.FromArgb(136, 19, 55),
                Location = new Point(25, 15),
                AutoSize = true,
                Padding = new Padding(6, 3, 6, 3)
            };

            var lblHeader = new Label
            {
                Text = "Emergency Fail-Safe System & Project Author Credits",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(22, 45),
                AutoSize = true
            };

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);

            // Main Content TableLayoutPanel (Stretches smoothly in Maximized mode!)
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

            // Left Panel: Mayday Emergency Control
            var grpMayday = new GroupBox
            {
                Text = "v6.0.0 Mayday Emergency Control Center",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(244, 63, 94),
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 15, 0),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var pnlMaydayInner = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            lblMaydayStatus = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 10.5F),
                ForeColor = Color.FromArgb(16, 185, 129),
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(15)
            };

            lblMaydayStatus.Text = 
                "STATUS           : ALL SYSTEMS NOMINAL\n" +
                "FLIGHT SENSORS   : OK (TRANSPONDER 7000)\n" +
                "ENGINE TELEMETRY : OK (N1 88.4% / N2 91.2%)\n" +
                "HYDRAULIC PRESS  : OK (3,000 PSI)\n" +
                "CABIN OXYGEN     : NORMAL (100% CAP)\n" +
                "WEATHER RADAR    : CLEAR / NO SEVERE TURBULENCE\n" +
                "EMERGENCY ROUTE  : STANDBY (NEAREST ALTERNATE: ISB)\n" +
                "FAIL-SAFE MODE   : READY FOR DEPLOYMENT";

            btnTriggerMayday = new Button
            {
                Text = "TRIGGER MAYDAY EMERGENCY PROTOCOL 🚨",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(225, 29, 72),
                FlatStyle = FlatStyle.Flat,
                Dock = DockStyle.Bottom,
                Height = 50,
                Cursor = Cursors.Hand
            };
            btnTriggerMayday.FlatAppearance.BorderSize = 0;
            btnTriggerMayday.Click += BtnTriggerMayday_Click;

            pnlMaydayInner.Controls.Add(lblMaydayStatus);
            pnlMaydayInner.Controls.Add(btnTriggerMayday);
            grpMayday.Controls.Add(pnlMaydayInner);

            // Right Panel: Project Credits & Standalone Author
            var grpCredits = new GroupBox
            {
                Text = "System Author & Architecture Credits",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(15, 0, 0, 0),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var pnlCreditsInner = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var pnlContribCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(25),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblContribName = new Label
            {
                Text = "Mohammad Sufiyan Aasim",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(14, 165, 233),
                Dock = DockStyle.Top,
                Height = 45,
                AutoSize = false
            };

            var lblContribRole = new Label
            {
                Text = "Sole System Architect & Lead Developer\nAI/MLOps · Full-Stack C# WinForms Engineer\n\nGitHub Profile : github.com/SufiyanAasim\nEmail Contact  : sufiyanaasim@outlook.com",
                Font = new Font("Segoe UI", 11.5F),
                ForeColor = Color.FromArgb(226, 232, 240),
                Dock = DockStyle.Fill,
                AutoSize = false
            };

            var lblTechDetails = new Label
            {
                Text = "Built with C# & .NET 8.0 Windows Desktop Framework\nTheme: Aviation & Flight Phases (v1.0.0 to v6.0.0)\nMIT License © 2023-2026 Airline Reservation System Contributors",
                Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Bottom,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlContribCard.Controls.Add(lblContribRole);
            pnlContribCard.Controls.Add(lblContribName);
            pnlContribCard.Controls.Add(lblTechDetails);

            pnlCreditsInner.Controls.Add(pnlContribCard);
            grpCredits.Controls.Add(pnlCreditsInner);

            pnlMain.Controls.Add(grpMayday, 0, 0);
            pnlMain.Controls.Add(grpCredits, 1, 0);

            // Footer Navigation (Clean Back / Next buttons!)
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 85,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var btnBack = new Button
            {
                Text = "⇦ BACK TO TOUCHDOWN",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 65, 85),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(230, 45),
                Location = new Point(25, 20),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) => FormNavigator.Navigate(this, new ReceiptTouchdownForm(currentBooking));

            var btnRestart = new Button
            {
                Text = "🔄 RESTART FLIGHT WIZARD (CLEARANCE)",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(14, 165, 233),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(340, 45),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(760, 20),
                Cursor = Cursors.Hand
            };
            btnRestart.FlatAppearance.BorderSize = 0;
            btnRestart.Click += (s, e) => FormNavigator.Navigate(this, new WelcomeClearanceForm());

            pnlFooter.Controls.Add(btnBack);
            pnlFooter.Controls.Add(btnRestart);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);
        }

        private void BtnTriggerMayday_Click(object? sender, EventArgs e)
        {
            SoundHelper.PlayAlert();
            isMaydayActive = !isMaydayActive;

            if (isMaydayActive)
            {
                currentBooking.IsEmergencyAborted = true;
                currentBooking.FlightPhaseStatus = "MAYDAY / EMERGENCY REROUTED";

                lblMaydayStatus.ForeColor = Color.FromArgb(225, 29, 72);
                lblMaydayStatus.Text = 
                    "🚨 MAYDAY EMERGENCY ACTIVATED! 🚨\n" +
                    "----------------------------------------\n" +
                    "TRANSPONDER      : SQUAWK 7700 (EMERGENCY)\n" +
                    "AUTOPILOT        : EMERGENCY DESCENT MODE\n" +
                    "FLIGHT REROUTED  : DIRECT DIVERSIFIED FIELD\n" +
                    "AIR TRAFFIC CTRL : PRIORITY CLEARANCE GRANTED\n" +
                    "PASSENGER SAFETY : OXYGEN MASKS DEPLOYED\n" +
                    "RECORDER LOG     : TELEMETRY SAVED TO DISK";

                btnTriggerMayday.Text = "DEACTIVATE MAYDAY PROTOCOL 🟢";
                btnTriggerMayday.BackColor = Color.FromArgb(16, 185, 129);

                CustomMessageBox.Show("EMERGENCY MAYDAY ACTIVATED", "Squawk 7700 transmitted to ATC. Aircraft priority clearance engaged and telemetry logged.", true);
            }
            else
            {
                currentBooking.IsEmergencyAborted = false;
                currentBooking.FlightPhaseStatus = "Touchdown / Confirmed";

                lblMaydayStatus.ForeColor = Color.FromArgb(16, 185, 129);
                lblMaydayStatus.Text = 
                    "STATUS           : ALL SYSTEMS NOMINAL\n" +
                    "FLIGHT SENSORS   : OK (TRANSPONDER 7000)\n" +
                    "ENGINE TELEMETRY : OK (N1 88.4% / N2 91.2%)\n" +
                    "HYDRAULIC PRESS  : OK (3,000 PSI)\n" +
                    "CABIN OXYGEN     : NORMAL (100% CAP)\n" +
                    "WEATHER RADAR    : CLEAR / NO SEVERE TURBULENCE\n" +
                    "EMERGENCY ROUTE  : STANDBY (NEAREST ALTERNATE: ISB)\n" +
                    "FAIL-SAFE MODE   : READY FOR DEPLOYMENT";

                btnTriggerMayday.Text = "TRIGGER MAYDAY EMERGENCY PROTOCOL 🚨";
                btnTriggerMayday.BackColor = Color.FromArgb(225, 29, 72);
            }
        }
    }
}
