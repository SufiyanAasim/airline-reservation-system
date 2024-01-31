namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Models;
    using AirlineApp.Services;

    public class MaydayForm : Form
    {
        private readonly Booking currentBooking;
        private TextBox txtConsoleLog = null!;
        private Button btnSquawk = null!;
        private Button btnDumpFuel = null!;
        private Button btnExtinguishEng1 = null!;
        private Button btnExtinguishEng2 = null!;
        private ComboBox comboDiversionAirport = null!;
        private ProgressBar pbFuelGauge = null!;
        private Label lblFuelStatus = null!;
        private Panel pnlRadarBeacon = null!;
        private Label lblBeaconStatus = null!;
        private Label lblTelemetryCard = null!;

        private System.Windows.Forms.Timer beaconTimer = null!;
        private System.Windows.Forms.Timer fuelDumpTimer = null!;

        private bool isSquawkActive = false;
        private bool isEng1Extinguished = false;
        private bool isEng2Extinguished = false;
        private int currentFuelPct = 85;
        private bool beaconState = false;

        public MaydayForm(Booking booking)
        {
            this.currentBooking = booking ?? CreateFallbackBooking();
            InitializeComponent();
            IconHelper.ApplyIcon(this);
            StartLiveAnimations();
        }

        private static Booking CreateFallbackBooking()
        {
            var flight = FlightService.GetFlights()[0];
            var passenger = new Passenger { FullName = "Capt. Sufiyan Aasim", SeatNumber = "03A" };
            return FlightService.CalculateFullBooking(flight, passenger);
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
                Text = "Emergency Control Center & Transponder Squawk 7700 Protocol",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(22, 46),
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
            btnBack.Click += (s, e) =>
            {
                SoundHelper.PlayTap();
                FormNavigator.Navigate(this, new ReceiptTouchdownForm(currentBooking));
            };

            var btnRestart = new Button
            {
                Text = "🔄 RESTART FLIGHT WIZARD (CLEARANCE)",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(14, 165, 233),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(330, 45),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(600, 20),
                Cursor = Cursors.Hand
            };
            btnRestart.FlatAppearance.BorderSize = 0;
            btnRestart.Click += (s, e) =>
            {
                SoundHelper.PlayTap();
                FormNavigator.Navigate(this, new WelcomeClearanceForm());
            };

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
                btnRestart.Location = new Point(btnExitFooter.Left - btnRestart.Width - 15, 20);
            };

            pnlFooter.Controls.Add(btnBack);
            pnlFooter.Controls.Add(btnRestart);
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
            pnlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Left Section: Emergency Controls & Fuel Jettison Panel
            var grpControls = new GroupBox
            {
                Text = "Aircraft Emergency Action Controls",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(244, 63, 94),
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 15, 0),
                Padding = new Padding(15),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var pnlControlsTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 6,
                BackColor = Color.FromArgb(30, 41, 59)
            };
            pnlControlsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));  // Radar Beacon Card
            pnlControlsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));  // Squawk 7700 Button
            pnlControlsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));  // Diversion Airport Selector
            pnlControlsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));  // Fuel Gauge & Jettison (Expanded to 82F for 100% button visibility!)
            pnlControlsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));  // Fire Extinguishers
            pnlControlsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Cockpit Telemetry Card

            // Row 0: Live Radar / Transponder Beacon Card
            var pnlBeaconCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 0, 8)
            };

            pnlRadarBeacon = new Panel
            {
                Location = new Point(12, 14),
                Size = new Size(20, 20),
                BackColor = Color.FromArgb(16, 185, 129)
            };

            lblBeaconStatus = new Label
            {
                Text = "LIVE RADAR BEACON: SQUAWK 7000 MONITORED (NORMAL)",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 185, 129),
                Location = new Point(42, 14),
                AutoSize = true
            };

            pnlBeaconCard.Controls.Add(pnlRadarBeacon);
            pnlBeaconCard.Controls.Add(lblBeaconStatus);

            // Row 1: Transponder Squawk Switch
            btnSquawk = new Button
            {
                Text = "TRANSPONDER: SQUAWK 7000 (NORMAL) 🟢",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(16, 185, 129),
                FlatStyle = FlatStyle.Flat,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 8),
                Cursor = Cursors.Hand
            };
            btnSquawk.FlatAppearance.BorderSize = 0;
            btnSquawk.Click += BtnSquawk_Click;

            // Row 2: Emergency Diversion Airport Selector Panel
            var pnlDiversionCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 0, 8)
            };

            var lblDiversion = new Label
            {
                Text = "Select Emergency Diversion Field:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Dock = DockStyle.Top,
                Height = 22
            };

            comboDiversionAirport = new ComboBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White
            };
            comboDiversionAirport.Items.AddRange(new object[] {
                "ISB — Islamabad International (Head: 045° | Dist: 180 KM)",
                "KHI — Jinnah International Karachi (Head: 210° | Dist: 420 KM)",
                "LHE — Allama Iqbal International Lahore (Head: 090° | Dist: 290 KM)",
                "DXB — Dubai International (Head: 260° | Dist: 1120 KM)"
            });
            comboDiversionAirport.SelectedIndex = 0;
            comboDiversionAirport.SelectedIndexChanged += (s, e) =>
            {
                SoundHelper.PlayTap();
                LogEvent($"ATC REROUTE: Diversion field updated to {comboDiversionAirport.SelectedItem}");
            };

            pnlDiversionCard.Controls.Add(comboDiversionAirport);
            pnlDiversionCard.Controls.Add(lblDiversion);

            // Row 3: Fuel Gauge & Jettison Controls Panel (Expanded 82F Height)
            var pnlFuelCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(12, 8, 12, 8),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 0, 8)
            };

            lblFuelStatus = new Label
            {
                Text = $"Fuel Quantity: {currentFuelPct}% (Jettison Ready for Max Landing Weight)",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(245, 158, 11),
                Dock = DockStyle.Top,
                Height = 24
            };

            var pnlFuelControls = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.FromArgb(15, 23, 42),
                Margin = new Padding(0, 4, 0, 0)
            };
            pnlFuelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            pnlFuelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));

            pbFuelGauge = new ProgressBar
            {
                Dock = DockStyle.Fill,
                Value = currentFuelPct,
                Maximum = 100,
                Margin = new Padding(0, 3, 10, 3)
            };

            btnDumpFuel = new Button
            {
                Text = "JETTISON FUEL ⛽",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(245, 158, 11),
                FlatStyle = FlatStyle.Flat,
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Cursor = Cursors.Hand
            };
            btnDumpFuel.FlatAppearance.BorderSize = 0;
            btnDumpFuel.Click += BtnDumpFuel_Click;

            pnlFuelControls.Controls.Add(pbFuelGauge, 0, 0);
            pnlFuelControls.Controls.Add(btnDumpFuel, 1, 0);

            pnlFuelCard.Controls.Add(pnlFuelControls);
            pnlFuelCard.Controls.Add(lblFuelStatus);

            // Row 4: Engine Fire Extinguisher Switches
            var pnlEngControls = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 8),
                BackColor = Color.FromArgb(30, 41, 59)
            };
            pnlEngControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlEngControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            btnExtinguishEng1 = new Button
            {
                Text = "DISCHARGE ENG 1 FIRE BOTTLE 🔥",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(225, 29, 72),
                FlatStyle = FlatStyle.Flat,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 5, 0),
                Cursor = Cursors.Hand
            };
            btnExtinguishEng1.FlatAppearance.BorderSize = 0;
            btnExtinguishEng1.Click += (s, e) => ToggleEngineExtinguisher(1);

            btnExtinguishEng2 = new Button
            {
                Text = "DISCHARGE ENG 2 FIRE BOTTLE 🔥",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(225, 29, 72),
                FlatStyle = FlatStyle.Flat,
                Dock = DockStyle.Fill,
                Margin = new Padding(5, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            btnExtinguishEng2.FlatAppearance.BorderSize = 0;
            btnExtinguishEng2.Click += (s, e) => ToggleEngineExtinguisher(2);

            pnlEngControls.Controls.Add(btnExtinguishEng1, 0, 0);
            pnlEngControls.Controls.Add(btnExtinguishEng2, 1, 0);

            // Row 5: Emergency Cockpit Telemetry Log Card
            var pnlTelemetryCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblTelemetryCard = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 10.5F),
                ForeColor = Color.FromArgb(56, 189, 248)
            };

            UpdateCockpitTelemetryCard();
            pnlTelemetryCard.Controls.Add(lblTelemetryCard);

            pnlControlsTable.Controls.Add(pnlBeaconCard, 0, 0);
            pnlControlsTable.Controls.Add(btnSquawk, 0, 1);
            pnlControlsTable.Controls.Add(pnlDiversionCard, 0, 2);
            pnlControlsTable.Controls.Add(pnlFuelCard, 0, 3);
            pnlControlsTable.Controls.Add(pnlEngControls, 0, 4);
            pnlControlsTable.Controls.Add(pnlTelemetryCard, 0, 5);

            grpControls.Controls.Add(pnlControlsTable);

            // Right Section: ATC Black Box Console Log
            var grpConsole = new GroupBox
            {
                Text = "ATC Transponder & Flight Recorder Telemetry Log",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(15, 0, 0, 0),
                Padding = new Padding(15),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            txtConsoleLog = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 10F),
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.FromArgb(56, 189, 248),
                BorderStyle = BorderStyle.FixedSingle
            };

            grpConsole.Controls.Add(txtConsoleLog);

            pnlMain.Controls.Add(grpControls, 0, 0);
            pnlMain.Controls.Add(grpConsole, 1, 0);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);

            LogEvent("SYSTEM INIT: Flight Data Recorder and Mayday Control Deck Operational.");
            LogEvent($"PASSENGER : {currentBooking.PassengerDetails.FullName} (PNR: {currentBooking.PnrReference})");
        }

        private void UpdateCockpitTelemetryCard()
        {
            var f = currentBooking.FlightDetails;
            var p = currentBooking.PassengerDetails;

            lblTelemetryCard.Text =
                $"EMERGENCY DECK  : OPERATIONAL\n\n" +
                $"FLIGHT / ROUTE  : {f.FlightNumber} ({f.OriginCode} ➔ {f.DestinationCode})\n\n" +
                $"PASSENGER PNR   : {currentBooking.PnrReference} ({p.FullName})\n\n" +
                $"SQLITE DB LOG   : AirlineSystem.db (PERSISTED)\n\n" +
                $"TRANSPONDER MODE: {(isSquawkActive ? "SQUAWK 7700 (MAYDAY)" : "SQUAWK 7000 (NORMAL)")}\n\n" +
                $"ENGINE STATUS   : ENG 1 {(isEng1Extinguished ? "[OFF/HALON]" : "[NORM]")} | ENG 2 {(isEng2Extinguished ? "[OFF/HALON]" : "[NORM]")}";
        }

        private void StartLiveAnimations()
        {
            beaconTimer = new System.Windows.Forms.Timer { Interval = 400 };
            beaconTimer.Tick += (s, e) =>
            {
                beaconState = !beaconState;
                if (isSquawkActive)
                {
                    pnlRadarBeacon.BackColor = beaconState ? Color.FromArgb(225, 29, 72) : Color.FromArgb(153, 27, 27);
                    lblBeaconStatus.Text = beaconState ? "🚨 LIVE MAYDAY BEACON: SQUAWK 7700 BROADCASTING (ACTIVE)" : "   LIVE MAYDAY BEACON: SQUAWK 7700 BROADCASTING (ACTIVE)";
                    lblBeaconStatus.ForeColor = Color.FromArgb(225, 29, 72);
                }
                else
                {
                    pnlRadarBeacon.BackColor = beaconState ? Color.FromArgb(16, 185, 129) : Color.FromArgb(6, 95, 70);
                    lblBeaconStatus.Text = "LIVE RADAR BEACON: SQUAWK 7000 MONITORED (NORMAL)";
                    lblBeaconStatus.ForeColor = Color.FromArgb(16, 185, 129);
                }
            };
            beaconTimer.Start();

            fuelDumpTimer = new System.Windows.Forms.Timer { Interval = 150 };
            fuelDumpTimer.Tick += (s, e) =>
            {
                if (currentFuelPct > 35)
                {
                    currentFuelPct -= 1;
                    pbFuelGauge.Value = currentFuelPct;
                    lblFuelStatus.Text = $"Fuel Quantity: {currentFuelPct}% (Jettisoning Fuel to Safe MLW)";
                    LogEvent($"FUEL DUMP TICK: Jettison active. Fuel remaining: {currentFuelPct}%.");
                }
                else
                {
                    fuelDumpTimer.Stop();
                    lblFuelStatus.Text = $"Fuel Quantity: {currentFuelPct}% (Target MLW Reached & Halted)";
                    LogEvent("FUEL DUMP HALTED: Reached minimum reserve capacity (35%).");
                    CustomMessageBox.Show("JETTISON COMPLETE", "Fuel dumped to safe Maximum Landing Weight (35%).");
                }
            };

            this.FormClosing += (s, e) =>
            {
                beaconTimer.Stop();
                fuelDumpTimer.Stop();
            };
        }

        private void BtnSquawk_Click(object? sender, EventArgs e)
        {
            isSquawkActive = !isSquawkActive;
            UpdateCockpitTelemetryCard();

            if (isSquawkActive)
            {
                SoundHelper.PlayMaydayAlarm();
                btnSquawk.Text = "TRANSPONDER: SQUAWK 7700 (MAYDAY ACTIVATED) 🚨";
                btnSquawk.BackColor = Color.FromArgb(225, 29, 72);
                currentBooking.IsEmergencyAborted = true;
                currentBooking.FlightPhaseStatus = "MAYDAY EMERGENCY REROUTED";

                LogEvent("🚨 SQUAWK 7700 TRANSMITTED TO ATC EMER FREQ 121.5 MHz.");
                LogEvent("ATC HANDSHAKE: Emergency priority landing clearance granted.");
                CustomMessageBox.Show("MAYDAY SQUAWK 7700 ENGAGED", "Emergency transponder code 7700 broadcast. Priority air traffic landing clearance active.", true);
            }
            else
            {
                SoundHelper.PlayTap();
                btnSquawk.Text = "TRANSPONDER: SQUAWK 7000 (NORMAL) 🟢";
                btnSquawk.BackColor = Color.FromArgb(16, 185, 129);
                currentBooking.IsEmergencyAborted = false;
                currentBooking.FlightPhaseStatus = "Touchdown / Confirmed";

                LogEvent("🟢 SQUAWK RESET TO 7000 NORMAL TRANSPONDER CODE.");
            }
        }

        private void BtnDumpFuel_Click(object? sender, EventArgs e)
        {
            SoundHelper.PlayTap();
            if (currentFuelPct > 35)
            {
                fuelDumpTimer.Start();
                LogEvent("FUEL DUMP INITIATED: Jettison pumps engaged.");
            }
            else
            {
                CustomMessageBox.Show("JETTISON WARNING", "Fuel quantity at minimum safe reserve limit (35%). Jettison halted.");
            }
        }

        private void ToggleEngineExtinguisher(int engineNum)
        {
            SoundHelper.PlayAlert();
            if (engineNum == 1)
            {
                isEng1Extinguished = !isEng1Extinguished;
                btnExtinguishEng1.Text = isEng1Extinguished ? "ENG 1 FIRE SUPPRESSED 🟢" : "DISCHARGE ENG 1 FIRE BOTTLE 🔥";
                btnExtinguishEng1.BackColor = isEng1Extinguished ? Color.FromArgb(16, 185, 129) : Color.FromArgb(225, 29, 72);
                LogEvent(isEng1Extinguished ? "ENG 1: Halon extinguisher bottle discharged. Fire suppressed." : "ENG 1: Fire bottle reset.");
            }
            else
            {
                isEng2Extinguished = !isEng2Extinguished;
                btnExtinguishEng2.Text = isEng2Extinguished ? "ENG 2 FIRE SUPPRESSED 🟢" : "DISCHARGE ENG 2 FIRE BOTTLE 🔥";
                btnExtinguishEng2.BackColor = isEng2Extinguished ? Color.FromArgb(16, 185, 129) : Color.FromArgb(225, 29, 72);
                LogEvent(isEng2Extinguished ? "ENG 2: Halon extinguisher bottle discharged. Fire suppressed." : "ENG 2: Fire bottle reset.");
            }
            UpdateCockpitTelemetryCard();
        }

        private void LogEvent(string msg)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            txtConsoleLog.AppendText($"[{timestamp}] {msg}\r\n");
        }
    }
}
