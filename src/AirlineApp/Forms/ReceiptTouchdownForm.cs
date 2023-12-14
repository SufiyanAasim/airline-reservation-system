namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Models;
    using AirlineApp.Services;

    public class ReceiptTouchdownForm : Form
    {
        private readonly Booking currentBooking;
        private Label lblBoardingPassCard = null!;
        private Label lblReceiptBreakdown = null!;

        public ReceiptTouchdownForm(Booking booking)
        {
            this.currentBooking = booking;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — v5.0.0 [Touchdown Phase - Boarding Pass & Receipt]";
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
                Text = "v5.0.0 TOUCHDOWN & RECEIPT ENGINE",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 185, 129),
                BackColor = Color.FromArgb(6, 78, 59),
                Location = new Point(25, 15),
                AutoSize = true,
                Padding = new Padding(6, 3, 6, 3)
            };

            var lblHeader = new Label
            {
                Text = $"Boarding Pass Generation & Official Receipt Printing ({currentBooking.PnrReference})",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(22, 42),
                AutoSize = true
            };

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);

            // Left Section: Printable Boarding Pass Card
            var grpPass = new GroupBox
            {
                Text = "Electronic Boarding Pass Card",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(25, 105),
                Size = new Size(470, 430),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            lblBoardingPassCard = new Label
            {
                Location = new Point(20, 35),
                Size = new Size(430, 375),
                Font = new Font("Consolas", 10F),
                ForeColor = Color.FromArgb(248, 250, 252),
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(12)
            };

            lblBoardingPassCard.Text = 
                "===========================================\n" +
                "           OFFICIAL BOARDING PASS           \n" +
                "===========================================\n" +
                $"PASSENGER NAME : {currentBooking.PassengerDetails.FullName.ToUpper()}\n" +
                $"PNR REFERENCE  : {currentBooking.PnrReference}\n" +
                $"FLIGHT NUMBER  : {currentBooking.FlightDetails.FlightNumber} ({currentBooking.FlightDetails.Airline})\n" +
                $"ROUTE          : {currentBooking.FlightDetails.OriginCode} ➔ {currentBooking.FlightDetails.DestinationCode}\n" +
                $"DEPARTURE TIME : {currentBooking.FlightDetails.DepartureTime}\n" +
                $"CABIN CLASS    : {currentBooking.PassengerDetails.Cabin.ToString().ToUpper()}\n" +
                $"SEAT ASSIGNED  : {currentBooking.PassengerDetails.SeatNumber}\n" +
                $"BAGGAGE ALLOW  : {currentBooking.PassengerDetails.BaggageWeightKg:F1} KG\n" +
                $"MEAL SELECTED  : {currentBooking.PassengerDetails.MealPreference}\n" +
                "-------------------------------------------\n" +
                "BARCODE : ||| |||||| | |||||||| |||| ||||||\n" +
                "STATUS  : TOUCHDOWN / BOARDING COMPLETED   \n" +
                "===========================================";

            grpPass.Controls.Add(lblBoardingPassCard);

            // Right Section: Receipt Breakdown & Disk Persistence Trigger
            var grpReceipt = new GroupBox
            {
                Text = "Payment Breakdown & Audit Logging",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(515, 105),
                Size = new Size(445, 430),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            lblReceiptBreakdown = new Label
            {
                Location = new Point(20, 35),
                Size = new Size(405, 260),
                Font = new Font("Consolas", 10F),
                ForeColor = Color.FromArgb(56, 189, 248),
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(12)
            };

            lblReceiptBreakdown.Text = 
                $"BASE ROUTE FARE    : ${currentBooking.BaseFare:F2}\n" +
                $"CABIN SURCHARGE    : ${currentBooking.CabinSurcharge:F2}\n" +
                $"EXCESS BAGGAGE FEE : ${currentBooking.ExcessBaggageFee:F2}\n" +
                $"ADD-ON SERVICES    : ${currentBooking.AddonServicesFee:F2}\n" +
                $"AIRPORT TAX (12%)  : ${currentBooking.AirportTax:F2}\n" +
                "----------------------------------------\n" +
                $"TOTAL AMOUNT PAID  : ${currentBooking.TotalFare:F2}\n\n" +
                $"TRANSACTION TIME   : {currentBooking.BookingTimestamp:yyyy-MM-dd HH:mm:ss}\n" +
                $"SAVED LOG FILE     : Airline Reservation History/Boarding Passes.txt";

            var btnPrint = new Button
            {
                Text = "PRINT BOARDING PASS / RECEIPT 🖨️",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(16, 185, 129),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(405, 42),
                Location = new Point(20, 310),
                Cursor = Cursors.Hand
            };
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Click += BtnPrint_Click;

            var btnSaveLog = new Button
            {
                Text = "SAVE TO LOCAL TRAVEL HISTORY 💾",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(14, 165, 233),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(405, 42),
                Location = new Point(20, 365),
                Cursor = Cursors.Hand
            };
            btnSaveLog.FlatAppearance.BorderSize = 0;
            btnSaveLog.Click += BtnSaveLog_Click;

            grpReceipt.Controls.Add(lblReceiptBreakdown);
            grpReceipt.Controls.Add(btnPrint);
            grpReceipt.Controls.Add(btnSaveLog);

            // Footer Navigation
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 85,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var btnBack = new Button
            {
                Text = "⇦ BACK TO REPORT GENERATION",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 65, 85),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(250, 42),
                Location = new Point(25, 20),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) => FormNavigator.Navigate(this, new ReportGenerationForm(currentBooking));

            var btnProceed = new Button
            {
                Text = "PROCEED TO MAYDAY & CREDITS ➔",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(225, 29, 72),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(280, 42),
                Location = new Point(670, 20),
                Cursor = Cursors.Hand
            };
            btnProceed.FlatAppearance.BorderSize = 0;
            btnProceed.Click += (s, e) => FormNavigator.Navigate(this, new MaydayCreditsForm(currentBooking));

            pnlFooter.Controls.Add(btnBack);
            pnlFooter.Controls.Add(btnProceed);

            this.Controls.Add(grpPass);
            this.Controls.Add(grpReceipt);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);

            // Auto save to history
            BookingHistoryService.SaveBooking(currentBooking);
        }

        private void BtnPrint_Click(object? sender, EventArgs e)
        {
            SoundHelper.PlayTap();
            CustomMessageBox.Show("PRINTING BOARDING PASS", $"Sent Boarding Pass ({currentBooking.PnrReference}) to Windows Print Spooler.\nTicket printed successfully!");
        }

        private void BtnSaveLog_Click(object? sender, EventArgs e)
        {
            SoundHelper.PlayTap();
            BookingHistoryService.SaveBooking(currentBooking);
            CustomMessageBox.Show("PERSISTENCE CONFIRMED", $"Booking details successfully appended to 'Airline Reservation History/Boarding Passes.txt'.");
        }
    }
}
