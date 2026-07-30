namespace AirlineApp.Forms
{
    using System;
    using System.Diagnostics;
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
            IconHelper.ApplyIcon(this);
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

            // Main Content TableLayoutPanel
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

            // Left Panel: Mayday Emergency Control
            var grpMayday = new GroupBox
            {
                Text = "v6.0.0 Mayday Emergency Control Center",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(244, 63, 94),
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 15, 0),
                Padding = new Padding(15),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var pnlMaydayInner = new Panel
            {
                Dock = DockStyle.Fill,
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

            // Right Panel: Project Contributors with Clickable Buttons!
            var grpCredits = new GroupBox
            {
                Text = "System Author & Contributors",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(15, 0, 0, 0),
                Padding = new Padding(15),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var pnlCreditsContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                BackColor = Color.FromArgb(30, 41, 59)
            };
            pnlCreditsContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            pnlCreditsContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            // Card 1: Mohammad Sufiyan Aasim
            var card1 = CreateContributorCard(
                "Mohammad Sufiyan Aasim",
                "System Architect · AI/MLOps · Docs",
                "https://github.com/SufiyanAasim",
                "sufiyanaasim@outlook.com",
                Color.FromArgb(14, 165, 233)
            );

            // Card 2: Fahad Bin Nasir
            var card2 = CreateContributorCard(
                "Fahad Bin Nasir",
                "Front-end Development",
                "https://github.com/FahadBinNasir",
                "fahadbinnasir@outlook.com",
                Color.FromArgb(16, 185, 129)
            );

            pnlCreditsContainer.Controls.Add(card1, 0, 0);
            pnlCreditsContainer.Controls.Add(card2, 0, 1);
            grpCredits.Controls.Add(pnlCreditsContainer);

            pnlMain.Controls.Add(grpMayday, 0, 0);
            pnlMain.Controls.Add(grpCredits, 1, 0);

            // Footer Navigation Bar
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

            pnlFooter.Resize += (s, e) =>
            {
                btnRestart.Location = new Point(pnlFooter.Width - btnRestart.Width - 30, 20);
            };

            pnlFooter.Controls.Add(btnBack);
            pnlFooter.Controls.Add(btnRestart);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);
        }

        private Panel CreateContributorCard(string name, string role, string githubUrl, string email, Color accentColor)
        {
            var pnl = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(15),
                Margin = new Padding(0, 5, 0, 5),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblName = new Label
            {
                Text = name,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = accentColor,
                Dock = DockStyle.Top,
                Height = 30
            };

            var lblRole = new Label
            {
                Text = role,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Dock = DockStyle.Top,
                Height = 25
            };

            var pnlButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 10, 0, 0)
            };

            var btnGithub = new Button
            {
                Text = "🌐 GITHUB PROFILE",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(30, 41, 59),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(185, 36),
                Margin = new Padding(0, 0, 10, 0),
                Cursor = Cursors.Hand
            };
            btnGithub.FlatAppearance.BorderSize = 1;
            btnGithub.FlatAppearance.BorderColor = accentColor;
            btnGithub.Click += (s, e) => OpenUrl(githubUrl);

            var btnEmail = new Button
            {
                Text = "✉️ EMAIL CONTACT",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(30, 41, 59),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(185, 36),
                Cursor = Cursors.Hand
            };
            btnEmail.FlatAppearance.BorderSize = 1;
            btnEmail.FlatAppearance.BorderColor = Color.FromArgb(148, 163, 184);
            btnEmail.Click += (s, e) => OpenUrl($"mailto:{email}");

            pnlButtons.Controls.Add(btnGithub);
            pnlButtons.Controls.Add(btnEmail);

            pnl.Controls.Add(pnlButtons);
            pnl.Controls.Add(lblRole);
            pnl.Controls.Add(lblName);

            return pnl;
        }

        private void OpenUrl(string url)
        {
            try
            {
                SoundHelper.PlayTap();
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("LINK OPEN ERROR", ex.Message, true);
            }
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
