namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Models;
    using AirlineApp.Services;

    public class SeatTaxiForm : Form
    {
        private readonly Flight currentFlight;
        private readonly Passenger currentPassenger;
        private string selectedSeat = "03A";
        private CabinClass selectedCabin = CabinClass.Economy;
        private Label lblSelectedSeatDisplay = null!;
        private Label lblFarePreview = null!;
        private Button[] seatButtons = new Button[24];

        public SeatTaxiForm(Flight flight, Passenger passenger)
        {
            this.currentFlight = flight;
            this.currentPassenger = passenger;
            InitializeComponent();
            IconHelper.ApplyIcon(this);
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — v2.0.0 [Taxi Phase - Seat Allocation]";
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
                Text = "v2.0.0 TAXI & GROUND OPERATIONS",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(245, 158, 11),
                BackColor = Color.FromArgb(120, 53, 15),
                Location = new Point(25, 15),
                AutoSize = true,
                Padding = new Padding(6, 3, 6, 3)
            };

            var lblHeader = new Label
            {
                Text = $"Cabin Class Selection & Interactive Aircraft Seat Grid ({currentFlight.FlightNumber})",
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
                Location = new Point(940, 25),
                Cursor = Cursors.Hand
            };
            btnCreditsHeader.FlatAppearance.BorderSize = 0;
            btnCreditsHeader.Click += (s, e) => FormNavigator.Navigate(this, new MaydayCreditsForm(FlightService.CalculateFullBooking(currentFlight, currentPassenger)));

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);
            headerPanel.Controls.Add(btnCreditsHeader);

            // Footer Navigation
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 85,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var btnBack = new Button
            {
                Text = "⇦ BACK TO CLEARANCE",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 65, 85),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(220, 45),
                Location = new Point(25, 20),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) => FormNavigator.Navigate(this, new WelcomeClearanceForm());

            var btnProceed = new Button
            {
                Text = "PROCEED TO ASCENT & BAGGAGE ➔",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(245, 158, 11),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(310, 45),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(780, 20),
                Cursor = Cursors.Hand
            };
            btnProceed.FlatAppearance.BorderSize = 0;
            btnProceed.Click += BtnProceed_Click;

            pnlFooter.Resize += (s, e) =>
            {
                btnProceed.Location = new Point(pnlFooter.Width - btnProceed.Width - 30, 20);
            };

            pnlFooter.Controls.Add(btnBack);
            pnlFooter.Controls.Add(btnProceed);

            // Main Layout Container
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

            // Left Section: Cabin Class & Seat Selector Summary
            var grpCabin = new GroupBox
            {
                Text = "Cabin Class & Selection Summary",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 15, 0),
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var rbEconomy = new RadioButton
            {
                Text = "Economy Class (1.0x Base Fare)",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(25, 35),
                AutoSize = true,
                Checked = true
            };
            rbEconomy.CheckedChanged += (s, e) => { if (rbEconomy.Checked) SetCabin(CabinClass.Economy); };

            var rbBusiness = new RadioButton
            {
                Text = "Business Class (1.85x Base Fare)",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(56, 189, 248),
                Location = new Point(25, 75),
                AutoSize = true
            };
            rbBusiness.CheckedChanged += (s, e) => { if (rbBusiness.Checked) SetCabin(CabinClass.Business); };

            var rbFirst = new RadioButton
            {
                Text = "First Class (3.20x Base Fare)",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(251, 191, 36),
                Location = new Point(25, 115),
                AutoSize = true
            };
            rbFirst.CheckedChanged += (s, e) => { if (rbFirst.Checked) SetCabin(CabinClass.FirstClass); };

            grpCabin.Controls.Add(rbEconomy);
            grpCabin.Controls.Add(rbBusiness);
            grpCabin.Controls.Add(rbFirst);

            lblSelectedSeatDisplay = new Label
            {
                Font = new Font("Consolas", 11.5F),
                ForeColor = Color.FromArgb(14, 165, 233),
                Location = new Point(25, 170),
                Size = new Size(420, 100),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            grpCabin.Controls.Add(lblSelectedSeatDisplay);

            lblFarePreview = new Label
            {
                Font = new Font("Consolas", 11.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 185, 129),
                Location = new Point(25, 280),
                Size = new Size(420, 140),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            grpCabin.Controls.Add(lblFarePreview);

            // Right Section: Interactive Seat Grid Map
            var grpSeatMap = new GroupBox
            {
                Text = "Aircraft Cabin Seating Map (2-2 Configuration)",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(15, 0, 0, 0),
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var pnlGrid = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(20)
            };

            string[] rowLetters = { "A", "B", "C", "D" };
            int index = 0;
            for (int r = 1; r <= 6; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    string seatName = $"{r:D2}{rowLetters[c]}";
                    bool isOccupied = (index == 2 || index == 9 || index == 17);

                    var btnSeat = new Button
                    {
                        Text = seatName,
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                        Size = new Size(72, 48),
                        Location = new Point(40 + c * 90 + (c >= 2 ? 45 : 0), 25 + (r - 1) * 60),
                        Tag = seatName,
                        Cursor = isOccupied ? Cursors.No : Cursors.Hand,
                        Enabled = !isOccupied
                    };

                    if (isOccupied)
                    {
                        btnSeat.BackColor = Color.FromArgb(225, 29, 72);
                        btnSeat.ForeColor = Color.White;
                    }
                    else if (seatName == selectedSeat)
                    {
                        btnSeat.BackColor = Color.FromArgb(14, 165, 233);
                        btnSeat.ForeColor = Color.White;
                    }
                    else
                    {
                        btnSeat.BackColor = Color.FromArgb(51, 65, 85);
                        btnSeat.ForeColor = Color.FromArgb(226, 232, 240);
                    }

                    string captSeat = seatName;
                    btnSeat.Click += (s, e) => SelectSeat(captSeat);

                    pnlGrid.Controls.Add(btnSeat);
                    seatButtons[index++] = btnSeat;
                }
            }

            grpSeatMap.Controls.Add(pnlGrid);

            pnlMain.Controls.Add(grpCabin, 0, 0);
            pnlMain.Controls.Add(grpSeatMap, 1, 0);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);

            UpdateCalculations();
        }

        private void SetCabin(CabinClass cabin)
        {
            selectedCabin = cabin;
            SoundHelper.PlayTap();
            UpdateCalculations();
        }

        private void SelectSeat(string seatCode)
        {
            selectedSeat = seatCode;
            SoundHelper.PlayTap();

            foreach (var btn in seatButtons)
            {
                if (btn == null || !btn.Enabled) continue;
                string bSeat = btn.Tag?.ToString() ?? string.Empty;
                if (bSeat == selectedSeat)
                {
                    btn.BackColor = Color.FromArgb(14, 165, 233);
                    btn.ForeColor = Color.White;
                }
                else
                {
                    btn.BackColor = Color.FromArgb(51, 65, 85);
                    btn.ForeColor = Color.FromArgb(226, 232, 240);
                }
            }

            UpdateCalculations();
        }

        private void UpdateCalculations()
        {
            currentPassenger.Cabin = selectedCabin;
            currentPassenger.SeatNumber = selectedSeat;

            decimal mult = FlightService.GetCabinMultiplier(selectedCabin);
            decimal cabinSurcharge = currentFlight.BaseFare * (mult - 1.0m);
            decimal total = currentFlight.BaseFare + cabinSurcharge;

            lblSelectedSeatDisplay.Text = $"SELECTED SEAT : {selectedSeat}\n\nPASSENGER     : {currentPassenger.FullName}\n\nCABIN CLASS   : {selectedCabin}";
            lblFarePreview.Text = $"BASE FARE       : ${currentFlight.BaseFare:F2}\n\nCABIN SURCHARGE : ${cabinSurcharge:F2}\n\nSUBTOTAL FARE   : ${total:F2}";
        }

        private void BtnProceed_Click(object? sender, EventArgs e)
        {
            FormNavigator.Navigate(this, new BaggageAscentForm(currentFlight, currentPassenger));
        }
    }
}
