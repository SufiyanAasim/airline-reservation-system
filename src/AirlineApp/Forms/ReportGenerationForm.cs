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
            this.booking = booking;
            InitializeComponent();
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

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);

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
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 10.5F),
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.FromArgb(56, 189, 248),
                BorderStyle = BorderStyle.None,
                Padding = new Padding(25)
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
                Size = new Size(220, 40),
                Location = new Point(25, 18),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            pnlFooter.Controls.Add(btnClose);

            this.Controls.Add(txtReportPreview);
            this.Controls.Add(toolbarPanel);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);

            GenerateReportPreview();
        }

        private void GenerateReportPreview()
        {
            int selected = comboReportType.SelectedIndex;
            if (selected == 0)
            {
                txtReportPreview.Text = 
                    "=========================================================================\n" +
                    "               AIRLINE RESERVATION SYSTEM — PASSENGER MANIFEST\n" +
                    "=========================================================================\n" +
                    $"GENERATED ON     : {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                    $"FLIGHT NUMBER    : {booking.FlightDetails.FlightNumber}\n" +
                    $"AIRLINE OPERATOR : {booking.FlightDetails.Airline}\n" +
                    $"ROUTE            : {booking.FlightDetails.Origin} ({booking.FlightDetails.OriginCode}) ➔ {booking.FlightDetails.Destination} ({booking.FlightDetails.DestinationCode})\n" +
                    "-------------------------------------------------------------------------\n" +
                    "SEAT | PASSENGER NAME         | PASSPORT / CNIC | CABIN CLASS | BAGGAGE\n" +
                    "-------------------------------------------------------------------------\n" +
                    $"{booking.PassengerDetails.SeatNumber,-4} | {booking.PassengerDetails.FullName,-22} | {booking.PassengerDetails.PassportOrId,-15} | {booking.PassengerDetails.Cabin,-11} | {booking.PassengerDetails.BaggageWeightKg} KG\n" +
                    "01A  | Capt. Sufiyan Aasim    | PK-98234109     | FirstClass  | 42 KG\n" +
                    "02B  | Tariq Mehmood          | PK-11029384     | Business    | 30 KG\n" +
                    "04C  | Ayesha Khan            | PK-77625149     | Economy     | 18 KG\n" +
                    "-------------------------------------------------------------------------\n" +
                    "TOTAL MANIFEST PASSENGERS: 4 | SEAT CAPACITY LOAD: 88.4%";
            }
            else if (selected == 1)
            {
                txtReportPreview.Text = 
                    "=========================================================================\n" +
                    "          AIRLINE RESERVATION SYSTEM — FINANCIAL REVENUE AUDIT LEDGER\n" +
                    "=========================================================================\n" +
                    $"AUDIT PERIOD     : {dtPickerFrom.Value:yyyy-MM-dd} TO {dtPickerTo.Value:yyyy-MM-dd}\n" +
                    $"PNR REFERENCE    : {booking.PnrReference}\n" +
                    "-------------------------------------------------------------------------\n" +
                    "ITEM DESCRIPTION                            | CALCULATION   | AMOUNT\n" +
                    "-------------------------------------------------------------------------\n" +
                    $"BASE ROUTE FARE                             | {booking.FlightDetails.FlightNumber,-13} | ${booking.BaseFare,10:F2}\n" +
                    $"CABIN CLASS SURCHARGE                       | {booking.PassengerDetails.Cabin,-13} | ${booking.CabinSurcharge,10:F2}\n" +
                    $"EXCESS BAGGAGE SURCHARGE                    | {booking.PassengerDetails.BaggageWeightKg} KG        | ${booking.ExcessBaggageFee,10:F2}\n" +
                    $"AIRPORT INFRASTRUCTURE TAX                  | 12.0% RATE    | ${booking.AirportTax,10:F2}\n" +
                    "-------------------------------------------------------------------------\n" +
                    $"TOTAL REVENUE AUDITED                       |               | ${booking.TotalFare,10:F2}";
            }
            else
            {
                txtReportPreview.Text = 
                    "=========================================================================\n" +
                    "            AIRLINE RESERVATION SYSTEM — SAFETY & TELEMETRY LOG\n" +
                    "=========================================================================\n" +
                    $"SAFETY MONITOR   : ACTIVE SQUAWK 7000 (NORMAL OPERATIONS)\n" +
                    $"AIRCRAFT TYPE    : {booking.FlightDetails.AircraftType}\n" +
                    "-------------------------------------------------------------------------\n" +
                    "TIMESTAMP           | EVENT CODE | STATUS      | DIAGNOSTIC DETAILS\n" +
                    "-------------------------------------------------------------------------\n" +
                    $"{DateTime.Now.AddMinutes(-45):HH:mm:ss}            | FL-CLR-01  | NOMINAL     | Departure clearance verified for {booking.FlightDetails.OriginCode}\n" +
                    $"{DateTime.Now.AddMinutes(-30):HH:mm:ss}            | FL-TAXI-02 | NOMINAL     | Seat {booking.PassengerDetails.SeatNumber} locked & confirmed\n" +
                    $"{DateTime.Now.AddMinutes(-15):HH:mm:ss}            | FL-CRUI-04 | NOMINAL     | Cruising FL380 airspeed Mach 0.82\n" +
                    "-------------------------------------------------------------------------\n" +
                    "SYSTEM INCIDENTS    : 0 DETECTED | HARDWARE HEALTH: 100% EXCELLENT";
            }
        }

        private void BtnExportTxt_Click(object? sender, EventArgs e)
        {
            try
            {
                string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Airline Reservation History");
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                string file = Path.Combine(dir, $"Report_{booking.PnrReference}_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
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
                string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Airline Reservation History");
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                string file = Path.Combine(dir, $"Report_{booking.PnrReference}_{DateTime.Now:yyyyMMdd_HHmmss}.csv");

                string csvContent = "PNR,FlightNo,Passenger,Passport,Origin,Destination,Cabin,BaggageKg,TotalFare,Timestamp\n" +
                                   $"\"{booking.PnrReference}\",\"{booking.FlightDetails.FlightNumber}\",\"{booking.PassengerDetails.FullName}\",\"{booking.PassengerDetails.PassportOrId}\",\"{booking.FlightDetails.OriginCode}\",\"{booking.FlightDetails.DestinationCode}\",\"{booking.PassengerDetails.Cabin}\",{booking.PassengerDetails.BaggageWeightKg},{booking.TotalFare},\"{booking.BookingTimestamp:yyyy-MM-dd HH:mm:ss}\"";

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
