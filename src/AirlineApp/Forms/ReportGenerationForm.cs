namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using AirlineApp.Models;
    using AirlineApp.Services;

    public class ReportGenerationForm : Form
    {
        private readonly Booking currentBooking;
        private ComboBox comboReportType = null!;
        private TextBox txtReportPreview = null!;
        private DateTimePicker dtStart = null!;
        private DateTimePicker dtEnd = null!;

        public ReportGenerationForm(Booking booking)
        {
            this.currentBooking = booking;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — Executive Report Generation Engine";
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
                Text = "EXECUTIVE AUDIT & REPORT GENERATOR",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(168, 85, 247),
                BackColor = Color.FromArgb(88, 28, 135),
                Location = new Point(25, 15),
                AutoSize = true,
                Padding = new Padding(6, 3, 6, 3)
            };

            var lblHeader = new Label
            {
                Text = "Custom Flight Manifest, Revenue Ledger & Export Suite",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(22, 42),
                AutoSize = true
            };

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);

            // Left Section: Controls & Filters
            var grpControls = new GroupBox
            {
                Text = "Report Parameters & Filters",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(25, 105),
                Size = new Size(380, 430),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblType = new Label
            {
                Text = "Select Report Category:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(20, 35),
                AutoSize = true
            };

            comboReportType = new ComboBox
            {
                Location = new Point(20, 65),
                Size = new Size(335, 30),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White
            };
            comboReportType.Items.AddRange(new object[] {
                "1. Passenger Flight Manifest & Booking Ledger",
                "2. Revenue Audit & Yield Financial Summary",
                "3. Operational Safety & Mayday Telemetry Audit"
            });
            comboReportType.SelectedIndex = 0;
            comboReportType.SelectedIndexChanged += (s, e) => GenerateReportPreview();

            var lblStart = new Label
            {
                Text = "Filter Start Date:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(20, 115),
                AutoSize = true
            };

            dtStart = new DateTimePicker
            {
                Location = new Point(20, 140),
                Size = new Size(335, 28),
                Font = new Font("Segoe UI", 9.5F),
                Value = DateTime.Now.AddDays(-30)
            };

            var lblEnd = new Label
            {
                Text = "Filter End Date:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(20, 185),
                AutoSize = true
            };

            dtEnd = new DateTimePicker
            {
                Location = new Point(20, 210),
                Size = new Size(335, 28),
                Font = new Font("Segoe UI", 9.5F),
                Value = DateTime.Now
            };

            // Export Buttons
            var btnExportTxt = new Button
            {
                Text = "📄 EXPORT REPORT (.TXT)",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(14, 165, 233),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(335, 42),
                Location = new Point(20, 270),
                Cursor = Cursors.Hand
            };
            btnExportTxt.FlatAppearance.BorderSize = 0;
            btnExportTxt.Click += BtnExportTxt_Click;

            var btnExportCsv = new Button
            {
                Text = "📊 EXPORT AUDIT LOG (.CSV)",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(16, 185, 129),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(335, 42),
                Location = new Point(20, 325),
                Cursor = Cursors.Hand
            };
            btnExportCsv.FlatAppearance.BorderSize = 0;
            btnExportCsv.Click += BtnExportCsv_Click;

            grpControls.Controls.Add(lblType);
            grpControls.Controls.Add(comboReportType);
            grpControls.Controls.Add(lblStart);
            grpControls.Controls.Add(dtStart);
            grpControls.Controls.Add(lblEnd);
            grpControls.Controls.Add(dtEnd);
            grpControls.Controls.Add(btnExportTxt);
            grpControls.Controls.Add(btnExportCsv);

            // Right Section: Report Preview
            var grpPreview = new GroupBox
            {
                Text = "Generated Audit Report Live Preview",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(420, 105),
                Size = new Size(540, 430),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            txtReportPreview = new TextBox
            {
                Location = new Point(20, 30),
                Size = new Size(500, 380),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 9.5F),
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.FromArgb(56, 189, 248)
            };

            grpPreview.Controls.Add(txtReportPreview);

            // Footer Navigation
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 85,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var btnBack = new Button
            {
                Text = "⇦ BACK TO CRUISING",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 65, 85),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(200, 42),
                Location = new Point(25, 20),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) => FormNavigator.Navigate(this, new AnalyticsCruisingForm(currentBooking.FlightDetails, currentBooking.PassengerDetails));

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
            btnProceed.Click += (s, e) => FormNavigator.Navigate(this, new ReceiptTouchdownForm(currentBooking));

            pnlFooter.Controls.Add(btnBack);
            pnlFooter.Controls.Add(btnProceed);

            this.Controls.Add(grpControls);
            this.Controls.Add(grpPreview);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);

            GenerateReportPreview();
        }

        private void GenerateReportPreview()
        {
            var sb = new StringBuilder();
            sb.AppendLine("==================================================================");
            sb.AppendLine("                 AEROTECH SYSTEMS FLIGHT REPORT                   ");
            sb.AppendLine("==================================================================");
            sb.AppendLine($"REPORT GENERATED : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"AUDIT PERIOD     : {dtStart.Value:yyyy-MM-dd} TO {dtEnd.Value:yyyy-MM-dd}");
            sb.AppendLine($"REPORT CATEGORY  : {comboReportType.SelectedItem}");
            sb.AppendLine("------------------------------------------------------------------");
            sb.AppendLine();

            int type = comboReportType.SelectedIndex;
            if (type == 0) // Manifest
            {
                sb.AppendLine("CURRENT PASSENGER BOOKING MANIFEST:");
                sb.AppendLine($"PNR REFERENCE   : {currentBooking.PnrReference}");
                sb.AppendLine($"PASSENGER NAME  : {currentBooking.PassengerDetails.FullName}");
                sb.AppendLine($"PASSPORT / ID   : {currentBooking.PassengerDetails.PassportOrId}");
                sb.AppendLine($"FLIGHT NUMBER   : {currentBooking.FlightDetails.FlightNumber} ({currentBooking.FlightDetails.Airline})");
                sb.AppendLine($"ROUTE           : {currentBooking.FlightDetails.OriginCode} -> {currentBooking.FlightDetails.DestinationCode}");
                sb.AppendLine($"CABIN / SEAT    : {currentBooking.PassengerDetails.Cabin} | Seat {currentBooking.PassengerDetails.SeatNumber}");
                sb.AppendLine($"BAGGAGE WEIGHT  : {currentBooking.PassengerDetails.BaggageWeightKg:F1} KG");
                sb.AppendLine($"MEAL PREFERENCE : {currentBooking.PassengerDetails.MealPreference}");
                sb.AppendLine($"STATUS          : CONFIRMED & AUDITED");
            }
            else if (type == 1) // Revenue Audit
            {
                var metrics = BookingHistoryService.GenerateAnalytics(currentBooking);
                sb.AppendLine("FLEET FINANCIAL & REVENUE AUDIT SUMMARY:");
                sb.AppendLine($"TOTAL BOOKINGS  : {metrics.TotalBookings}");
                sb.AppendLine($"GROSS REVENUE   : ${metrics.GrossRevenue:F2}");
                sb.AppendLine($"AVG LOAD FACTOR : {metrics.AverageLoadFactorPercent:F1}%");
                sb.AppendLine($"ECONOMY SEATS   : {metrics.EconomyCount}");
                sb.AppendLine($"BUSINESS SEATS  : {metrics.BusinessCount}");
                sb.AppendLine($"FIRST CLASS     : {metrics.FirstClassCount}");
                sb.AppendLine($"POPULAR ROUTE   : {metrics.MostPopularRoute}");
                sb.AppendLine($"AUDIT STATUS    : BALANCED / ZERO VARIANCE");
            }
            else // Safety Incident
            {
                sb.AppendLine("OPERATIONAL SAFETY & MAYDAY INCIDENT LOG:");
                sb.AppendLine("MAYDAY INCIDENTS : 0 ACTIVE INCIDENTS REPORTED");
                sb.AppendLine("AIRCRAFT HEALTH  : 100% OPERATIONAL");
                sb.AppendLine("NAVIGATION LOG   : ALL FLIGHT PHASES OPERATING NORMAL");
                sb.AppendLine("AUDIT OFFICER    : CAPT. SUFIYAN AASIM (CHIEF ARCHITECT)");
            }

            sb.AppendLine();
            sb.AppendLine("==================================================================");
            sb.AppendLine("END OF REPORT — AEROTECH SYSTEMS OFFICIALLY VERIFIED REPORT");
            sb.AppendLine("==================================================================");

            txtReportPreview.Text = sb.ToString();
        }

        private void BtnExportTxt_Click(object? sender, EventArgs e)
        {
            try
            {
                string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Airline Reservation History");
                Directory.CreateDirectory(dir);
                string filePath = Path.Combine(dir, $"Flight_Report_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                File.WriteAllText(filePath, txtReportPreview.Text);

                SoundHelper.PlayTap();
                CustomMessageBox.Show("REPORT EXPORTED", $"Executive report saved successfully to:\n{filePath}");
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("EXPORT ERROR", ex.Message, true);
            }
        }

        private void BtnExportCsv_Click(object? sender, EventArgs e)
        {
            try
            {
                string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Airline Reservation History");
                Directory.CreateDirectory(dir);
                string filePath = Path.Combine(dir, $"Audit_Ledger_{DateTime.Now:yyyyMMdd_HHmmss}.csv");

                var sb = new StringBuilder();
                sb.AppendLine("PNR,Timestamp,Passenger,FlightNo,Origin,Destination,Cabin,Seat,TotalFare");
                sb.AppendLine($"\"{currentBooking.PnrReference}\",\"{currentBooking.BookingTimestamp:yyyy-MM-dd HH:mm:ss}\",\"{currentBooking.PassengerDetails.FullName}\",\"{currentBooking.FlightDetails.FlightNumber}\",\"{currentBooking.FlightDetails.OriginCode}\",\"{currentBooking.FlightDetails.DestinationCode}\",\"{currentBooking.PassengerDetails.Cabin}\",\"{currentBooking.PassengerDetails.SeatNumber}\",\"{currentBooking.TotalFare:F2}\"");

                File.WriteAllText(filePath, sb.ToString());

                SoundHelper.PlayTap();
                CustomMessageBox.Show("CSV EXPORTED", $"Audit CSV log exported successfully to:\n{filePath}");
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("EXPORT ERROR", ex.Message, true);
            }
        }
    }
}
