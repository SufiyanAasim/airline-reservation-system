namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Models;
    using AirlineApp.Services;

    public class ReceiptTouchdownForm : Form
    {
        private readonly Booking booking;

        public ReceiptTouchdownForm(Booking booking)
        {
            this.booking = booking;
            InitializeComponent();
            BookingHistoryService.SaveBooking(booking);
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — v5.0.0 [Touchdown Phase - Boarding Pass & Receipt]";
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
                Text = "v5.0.0 TOUCHDOWN & ELECTRONIC BOARDING PASS",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 185, 129),
                BackColor = Color.FromArgb(6, 78, 59),
                Location = new Point(25, 15),
                AutoSize = true,
                Padding = new Padding(6, 3, 6, 3)
            };

            var lblHeader = new Label
            {
                Text = $"Boarding Pass Ticket Card & Transaction Summary (PNR: {booking.PnrReference})",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(22, 45),
                AutoSize = true
            };

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);

            // Main Ticket Card Container
            var pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(35, 20, 35, 20),
                BackColor = Color.FromArgb(15, 23, 42)
            };

            var pnlTicketCard = new Panel
            {
                Location = new Point(40, 20),
                Size = new Size(1000, 450),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(30, 41, 59),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Ticket Top Header Bar
            var pnlTicketHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = Color.FromArgb(14, 165, 233)
            };

            var lblAirlineTitle = new Label
            {
                Text = $"✈  {booking.FlightDetails.Airline.ToUpper()}  —  ELECTRONIC BOARDING PASS",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 18),
                AutoSize = true
            };

            var lblPnrDisplay = new Label
            {
                Text = $"PNR: {booking.PnrReference}",
                Font = new Font("Consolas", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(245, 158, 11),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(800, 18),
                AutoSize = true
            };

            pnlTicketHeader.Controls.Add(lblAirlineTitle);
            pnlTicketHeader.Controls.Add(lblPnrDisplay);

            // Ticket Body Label
            var lblTicketBody = new Label
            {
                Font = new Font("Consolas", 11F),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(25, 80),
                Size = new Size(950, 280),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            lblTicketBody.Text = 
                $"PASSENGER NAME : {booking.PassengerDetails.FullName.ToUpper()}\n" +
                $"PASSPORT / CNIC: {booking.PassengerDetails.PassportOrId}\n" +
                $"FLIGHT NUMBER  : {booking.FlightDetails.FlightNumber} ({booking.FlightDetails.AircraftType})\n" +
                $"ORIGIN ➔ DEST   : {booking.FlightDetails.Origin} ({booking.FlightDetails.OriginCode}) ➔ {booking.FlightDetails.Destination} ({booking.FlightDetails.DestinationCode})\n" +
                $"DEPARTURE TIME : {booking.FlightDetails.DepartureTime}\n" +
                $"SEAT ALLOCATION: {booking.PassengerDetails.SeatNumber} ({booking.PassengerDetails.Cabin} Class)\n" +
                $"BAGGAGE WEIGHT : {booking.PassengerDetails.BaggageWeightKg:F1} KG (Excess Fee: ${booking.ExcessBaggageFee:F2})\n" +
                $"MEAL PREFERENCE: {booking.PassengerDetails.MealPreference}\n" +
                $"TAX & CHARGES  : ${booking.AirportTax:F2} (12% Airport Tax Included)\n" +
                $"GRAND TOTAL    : ${booking.TotalFare:F2} (PAID IN FULL)\n" +
                $"STATUS         : TOUCHDOWN / CONFIRMED & LOGGED";

            // Barcode Aesthetic
            var lblBarcode = new Label
            {
                Text = "||| | ||||| || | |||| ||| |||| | ||||| ||| ||| |||| || | |||| |||",
                Font = new Font("Consolas", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Bottom,
                Height = 45,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlTicketCard.Controls.Add(pnlTicketHeader);
            pnlTicketCard.Controls.Add(lblTicketBody);
            pnlTicketCard.Controls.Add(lblBarcode);

            pnlMain.Controls.Add(pnlTicketCard);

            // Footer Navigation
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 90,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var btnPrint = new Button
            {
                Text = "PRINT BOARDING PASS 🖨️",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(16, 185, 129),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(240, 45),
                Location = new Point(25, 20),
                Cursor = Cursors.Hand
            };
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Click += BtnPrint_Click;

            var btnMayday = new Button
            {
                Text = "PROCEED TO MAYDAY & CREDITS 🚨",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(225, 29, 72),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(300, 45),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(740, 20),
                Cursor = Cursors.Hand
            };
            btnMayday.FlatAppearance.BorderSize = 0;
            btnMayday.Click += (s, e) => FormNavigator.Navigate(this, new MaydayCreditsForm(booking));

            pnlFooter.Controls.Add(btnPrint);
            pnlFooter.Controls.Add(btnMayday);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);
        }

        private void BtnPrint_Click(object? sender, EventArgs e)
        {
            SoundHelper.PlayTap();
            CustomMessageBox.Show("PRINT SPOOLER TRIGGERED", $"Boarding pass ticket for PNR {booking.PnrReference} sent to Windows Print Spooler.\nLocal copy persisted to disk.");
        }
    }
}
