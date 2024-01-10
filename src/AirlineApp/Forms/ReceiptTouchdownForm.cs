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
            IconHelper.ApplyIcon(this);
            BookingHistoryService.SaveBooking(booking);
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — Boarding Pass & Receipt";
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
                Text = $"Boarding Pass Ticket Card & Transaction Summary (PNR: {booking.PnrReference})",
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
            btnCreditsHeader.Click += (s, e) => FormNavigator.Navigate(this, new CreditsForm());

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

            // Footer Navigation
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 85,
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
                Text = "PROCEED TO MAYDAY CONTROL 🚨",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(225, 29, 72),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(310, 45),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(780, 20),
                Cursor = Cursors.Hand
            };
            btnMayday.FlatAppearance.BorderSize = 0;
            btnMayday.Click += (s, e) => FormNavigator.Navigate(this, new MaydayForm(booking));

            pnlFooter.Resize += (s, e) =>
            {
                btnMayday.Location = new Point(pnlFooter.Width - btnMayday.Width - 30, 20);
            };

            pnlFooter.Controls.Add(btnPrint);
            pnlFooter.Controls.Add(btnMayday);

            // Main Ticket Card Container
            var pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(25, 15, 25, 15),
                BackColor = Color.FromArgb(15, 23, 42)
            };

            var pnlTicketCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 41, 59),
                BorderStyle = BorderStyle.FixedSingle
            };

            var pnlTicketHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = Color.FromArgb(14, 165, 233)
            };

            var lblAirlineTitle = new Label
            {
                Text = $"✈  {booking.FlightDetails.Airline.ToUpper()}  —  ELECTRONIC BOARDING PASS",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(25, 18),
                AutoSize = true
            };

            var lblPnrDisplay = new Label
            {
                Text = $"PNR: {booking.PnrReference}",
                Font = new Font("Consolas", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(245, 158, 11),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(850, 18),
                AutoSize = true
            };

            pnlTicketHeader.Controls.Add(lblAirlineTitle);
            pnlTicketHeader.Controls.Add(lblPnrDisplay);

            var lblTicketBody = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 11.5F),
                ForeColor = Color.FromArgb(226, 232, 240),
                Padding = new Padding(30, 25, 30, 20)
            };

            lblTicketBody.Text = 
                $"PASSENGER NAME : {booking.PassengerDetails.FullName.ToUpper()}\n\n" +
                $"PASSPORT / CNIC: {booking.PassengerDetails.PassportOrId}\n\n" +
                $"FLIGHT NUMBER  : {booking.FlightDetails.FlightNumber} ({booking.FlightDetails.AircraftType})\n\n" +
                $"ORIGIN ➔ DEST   : {booking.FlightDetails.Origin} ({booking.FlightDetails.OriginCode}) ➔ {booking.FlightDetails.Destination} ({booking.FlightDetails.DestinationCode})\n\n" +
                $"DEPARTURE TIME : {booking.FlightDetails.DepartureTime}\n\n" +
                $"SEAT ALLOCATION: {booking.PassengerDetails.SeatNumber} ({booking.PassengerDetails.Cabin} Class)\n\n" +
                $"BAGGAGE WEIGHT : {booking.PassengerDetails.BaggageWeightKg:F1} KG (Excess Fee: ${booking.ExcessBaggageFee:F2})\n\n" +
                $"MEAL PREFERENCE: {booking.PassengerDetails.MealPreference}\n\n" +
                $"TAX & CHARGES  : ${booking.AirportTax:F2} (12% Airport Tax Included)\n\n" +
                $"GRAND TOTAL    : ${booking.TotalFare:F2} (PAID IN FULL)\n\n" +
                $"BOOKING STATUS : CONFIRMED & LOGGED";

            var lblBarcode = new Label
            {
                Text = "||| | ||||| || | |||| ||| |||| | ||||| ||| ||| |||| || | |||| ||| ||| ||| |||||",
                Font = new Font("Consolas", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Bottom,
                Height = 45,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlTicketCard.Controls.Add(lblTicketBody);
            pnlTicketCard.Controls.Add(pnlTicketHeader);
            pnlTicketCard.Controls.Add(lblBarcode);

            pnlMain.Controls.Add(pnlTicketCard);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);
        }

        private void BtnPrint_Click(object? sender, EventArgs e)
        {
            SoundHelper.PlaySuccess();
            CustomMessageBox.Show("PRINT SPOOLER TRIGGERED", $"Boarding pass ticket for PNR {booking.PnrReference} sent to Windows Print Spooler.\nLocal copy persisted to disk.");
        }
    }
}
