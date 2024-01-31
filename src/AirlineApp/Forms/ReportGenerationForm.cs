namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using AirlineApp.Models;
    using AirlineApp.Services;

    public class ReportGenerationForm : Form
    {
        private readonly Booking booking;
        private ComboBox comboReportType = null!;
        private TextBox txtReportPreview = null!;
        private DateTimePicker dtPickerFrom = null!;
        private DateTimePicker dtPickerTo = null!;

        public ReportGenerationForm(Booking booking)
        {
            this.booking = booking ?? CreateFallbackBooking();
            InitializeComponent();
            IconHelper.ApplyIcon(this);
        }

        private static Booking CreateFallbackBooking()
        {
            var flight = FlightService.GetFlights()[0];
            var passenger = new Passenger
            {
                FullName = "Capt. Sufiyan Aasim",
                PassportOrId = "42101-9876543-1",
                Cabin = CabinClass.Economy,
                SeatNumber = "03A"
            };
            return FlightService.CalculateFullBooking(flight, passenger);
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — Executive Flight & Financial Report Generator";
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
                Text = "EXECUTIVE REPORT BUILDER",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(139, 92, 246),
                BackColor = Color.FromArgb(76, 29, 149),
                Location = new Point(25, 15),
                AutoSize = true,
                Padding = new Padding(6, 3, 6, 3)
            };

            var lblHeader = new Label
            {
                Text = "Passenger Manifests, Revenue Audit Ledgers & Safety Report Exporter",
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
            btnCreditsHeader.Click += (s, e) => FormNavigator.Navigate(this, new CreditsForm(this));

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
            btnExitHeader.Click += (s, e) => Application.Exit();

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);
            headerPanel.Controls.Add(btnCreditsHeader);
            headerPanel.Controls.Add(btnExitHeader);

            // Controls Toolbar Panel
            var toolbarPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(30, 41, 59),
                Padding = new Padding(25, 15, 25, 15)
            };

            var lblType = new Label
            {
                Text = "Report Type:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(25, 20),
                AutoSize = true
            };

            comboReportType = new ComboBox
            {
                Location = new Point(125, 16),
                Size = new Size(260, 30),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White
            };
            comboReportType.Items.AddRange(new object[] { "Passenger Flight Manifest", "Financial Revenue Audit Ledger", "Safety & Telemetry Incident Report" });
            comboReportType.SelectedIndex = 0;
            comboReportType.SelectedIndexChanged += (s, e) => GenerateReportPreview();

            var lblFrom = new Label
            {
                Text = "From:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(410, 20),
                AutoSize = true
            };

            dtPickerFrom = new DateTimePicker
            {
                Location = new Point(460, 16),
                Size = new Size(130, 28),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now.AddDays(-30)
            };

            var lblTo = new Label
            {
                Text = "To:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(605, 20),
                AutoSize = true
            };

            dtPickerTo = new DateTimePicker
            {
                Location = new Point(640, 16),
                Size = new Size(130, 28),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now
            };

            var btnExportTxt = new Button
            {
                Text = "EXPORT .TXT 📄",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(16, 185, 129),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(140, 32),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(810, 15),
                Cursor = Cursors.Hand
            };
            btnExportTxt.FlatAppearance.BorderSize = 0;
            btnExportTxt.Click += BtnExportTxt_Click;

            var btnExportCsv = new Button
            {
                Text = "EXPORT .CSV 📊",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(14, 165, 233),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(140, 32),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(965, 15),
                Cursor = Cursors.Hand
            };
            btnExportCsv.FlatAppearance.BorderSize = 0;
            btnExportCsv.Click += BtnExportCsv_Click;

            toolbarPanel.Controls.Add(lblType);
            toolbarPanel.Controls.Add(comboReportType);
            toolbarPanel.Controls.Add(lblFrom);
            toolbarPanel.Controls.Add(dtPickerFrom);
            toolbarPanel.Controls.Add(lblTo);
            toolbarPanel.Controls.Add(dtPickerTo);
            toolbarPanel.Controls.Add(btnExportTxt);
            toolbarPanel.Controls.Add(btnExportCsv);

            // Report Preview Container
            txtReportPreview = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                WordWrap = false,
                ScrollBars = ScrollBars.Both,
                Font = new Font("Consolas", 11F),
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.FromArgb(56, 189, 248),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Footer
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 75,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var btnClose = new Button
            {
                Text = "⇦ RETURN TO CRUISING",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 65, 85),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(220, 42),
                Location = new Point(25, 16),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) =>
            {
                SoundHelper.PlayTap();
                var flight = booking?.FlightDetails ?? FlightService.GetFlights()[0];
                var passenger = booking?.PassengerDetails ?? new Passenger { FullName = "Capt. Sufiyan Aasim" };
                FormNavigator.Navigate(this, new AnalyticsCruisingForm(flight, passenger));
            };

            var btnExitFooter = new Button
            {
                Text = "❌ EXIT",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(225, 29, 72),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(130, 42),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(965, 16),
                Cursor = Cursors.Hand
            };
            btnExitFooter.FlatAppearance.BorderSize = 0;
            btnExitFooter.Click += (s, e) => Application.Exit();

            pnlFooter.Resize += (s, e) =>
            {
                btnExitFooter.Location = new Point(pnlFooter.Width - btnExitFooter.Width - 25, 16);
            };

            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Controls.Add(btnExitFooter);

            this.Controls.Add(txtReportPreview);
            this.Controls.Add(toolbarPanel);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);

            GenerateReportPreview();
        }

        private void GenerateReportPreview()
        {
            string nl = Environment.NewLine;
            int selected = comboReportType.SelectedIndex;

            var f = booking?.FlightDetails ?? FlightService.GetFlights()[0];
            var p = booking?.PassengerDetails ?? new Passenger { FullName = "Capt. Sufiyan Aasim", SeatNumber = "03A", BaggageWeightKg = 28 };

            if (selected == 0)
            {
                txtReportPreview.Text =
                    "==========================================================================================" + nl +
                    "               AIRLINE RESERVATION SYSTEM — PASSENGER MANIFEST REPORT" + nl +
                    "==========================================================================================" + nl +
                    $"GENERATED ON     : {DateTime.Now:yyyy-MM-dd HH:mm:ss}" + nl +
                    $"FLIGHT NUMBER    : {f.FlightNumber}" + nl +
                    $"AIRLINE OPERATOR : {f.Airline}" + nl +
                    $"ROUTE            : {f.Origin} ({f.OriginCode}) ➔ {f.Destination} ({f.DestinationCode})" + nl +
                    "------------------------------------------------------------------------------------------" + nl +
                    "SEAT | PASSENGER NAME         | PASSPORT / CNIC | CABIN CLASS | BAGGAGE WEIGHT | STATUS" + nl +
                    "------------------------------------------------------------------------------------------" + nl +
                    $"{p.SeatNumber,-4} | {p.FullName,-22} | {p.PassportOrId,-15} | {p.Cabin,-11} | {p.BaggageWeightKg,5:F1} KG       | CONFIRMED" + nl +
                    "01A  | Mohammad Sufiyan Aasim | PK-98234109     | FirstClass  |  42.0 KG       | CONFIRMED" + nl +
                    "02B  | Tariq Mehmood          | PK-11029384     | Business    |  30.0 KG       | CONFIRMED" + nl +
                    "04C  | Ayesha Khan            | PK-77625149     | Economy     |  18.0 KG       | CONFIRMED" + nl +
                    "------------------------------------------------------------------------------------------" + nl +
                    "TOTAL MANIFEST PASSENGERS: 4 | SEAT CAPACITY LOAD: 88.4% | AUDIT STATUS: COMPLIANT";
            }
            else if (selected == 1)
            {
                txtReportPreview.Text =
                    "==========================================================================================" + nl +
                    "          AIRLINE RESERVATION SYSTEM — FINANCIAL REVENUE AUDIT LEDGER" + nl +
                    "==========================================================================================" + nl +
                    $"AUDIT PERIOD     : {dtPickerFrom.Value:yyyy-MM-dd} TO {dtPickerTo.Value:yyyy-MM-dd}" + nl +
                    $"PNR REFERENCE    : {booking?.PnrReference ?? "PNR-FALLBACK"}" + nl +
                    "------------------------------------------------------------------------------------------" + nl +
                    "ITEM DESCRIPTION                            | CALCULATION RATE  | AUDITED AMOUNT" + nl +
                    "------------------------------------------------------------------------------------------" + nl +
                    $"BASE ROUTE FARE RATE                        | {f.FlightNumber,-17} | ${booking?.BaseFare ?? f.BaseFare,12:F2}" + nl +
                    $"CABIN CLASS SURCHARGE                       | {p.Cabin,-17} | ${booking?.CabinSurcharge ?? 0m,12:F2}" + nl +
                    $"EXCESS BAGGAGE FEE                          | {p.BaggageWeightKg,5:F1} KG          | ${booking?.ExcessBaggageFee ?? 0m,12:F2}" + nl +
                    $"IN-FLIGHT SERVICE ADD-ONS                   | OPTIONAL PASS     | ${booking?.AddonServicesFee ?? 0m,12:F2}" + nl +
                    $"AIRPORT INFRASTRUCTURE TAX                  | 12.0% RATE        | ${booking?.AirportTax ?? 0m,12:F2}" + nl +
                    "------------------------------------------------------------------------------------------" + nl +
                    $"TOTAL REVENUE AUDITED                       | GRAND TOTAL       | ${booking?.TotalFare ?? 180m,12:F2}";
            }
            else
            {
                txtReportPreview.Text =
                    "==========================================================================================" + nl +
                    "            AIRLINE RESERVATION SYSTEM — SAFETY & TELEMETRY AUDIT REPORT" + nl +
                    "==========================================================================================" + nl +
                    $"SAFETY MONITOR   : SQUAWK 7000 (NORMAL FLIGHT OPERATIONS)" + nl +
                    $"AIRCRAFT TYPE    : {f.AircraftType}" + nl +
                    "------------------------------------------------------------------------------------------" + nl +
                    "TIMESTAMP           | EVENT CODE | STATUS      | DIAGNOSTIC TELEMETRY DETAILS" + nl +
                    "------------------------------------------------------------------------------------------" + nl +
                    $"{DateTime.Now.AddMinutes(-45):HH:mm:ss}            | FL-CLR-01  | NOMINAL     | Departure clearance verified for {f.OriginCode}" + nl +
                    $"{DateTime.Now.AddMinutes(-30):HH:mm:ss}            | FL-TAXI-02 | NOMINAL     | Seat {p.SeatNumber} locked & passenger manifest confirmed" + nl +
                    $"{DateTime.Now.AddMinutes(-15):HH:mm:ss}            | FL-CRUI-04 | NOMINAL     | Cruising FL380 airspeed Mach 0.82 OAT -54°C" + nl +
                    "------------------------------------------------------------------------------------------" + nl +
                    "SYSTEM INCIDENTS    : 0 DETECTED | HARDWARE HEALTH: 100% EXCELLENT | BLACK BOX RECORD: LOGGED";
            }
        }

        private void BtnExportTxt_Click(object? sender, EventArgs e)
        {
            try
            {
                SoundHelper.PlayTap();
                string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Airline Reservation History");
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                string pnr = booking?.PnrReference ?? "PNR-FALLBACK";
                string file = Path.Combine(dir, $"Report_{pnr}_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                File.WriteAllText(file, txtReportPreview.Text);
                CustomMessageBox.Show("REPORT EXPORTED", $"Executive report exported successfully to:\n{file}");
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("EXPORT FAILED", ex.Message, true);
            }
        }

        private void BtnExportCsv_Click(object? sender, EventArgs e)
        {
            try
            {
                SoundHelper.PlayTap();
                string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Airline Reservation History");
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                string pnr = booking?.PnrReference ?? "PNR-FALLBACK";
                string file = Path.Combine(dir, $"Report_{pnr}_{DateTime.Now:yyyyMMdd_HHmmss}.csv");

                var f = booking?.FlightDetails ?? FlightService.GetFlights()[0];
                var p = booking?.PassengerDetails ?? new Passenger { FullName = "Capt. Sufiyan Aasim" };

                string csvContent = "PNR,FlightNo,Passenger,Passport,Origin,Destination,Cabin,BaggageKg,TotalFare,Timestamp\n" +
                                   $"\"{pnr}\",\"{f.FlightNumber}\",\"{p.FullName}\",\"{p.PassportOrId}\",\"{f.OriginCode}\",\"{f.DestinationCode}\",\"{p.Cabin}\",{p.BaggageWeightKg},{booking?.TotalFare ?? 180m},\"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\"";

                File.WriteAllText(file, csvContent);
                CustomMessageBox.Show("CSV EXPORTED", $"Spreadsheet ledger exported successfully to:\n{file}");
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("EXPORT FAILED", ex.Message, true);
            }
        }
    }
}
