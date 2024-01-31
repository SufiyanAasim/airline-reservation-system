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
            btnBack.Click += (s, e) =>
            {
                SoundHelper.PlayTap();
                FormNavigator.Navigate(this, new WelcomeClearanceForm());
            };

            var btnProceed = new Button
            {
                Text = "PROCEED TO ASCENT & BAGGAGE ➔",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(245, 158, 11),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(310, 45),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(600, 20),
                Cursor = Cursors.Hand
            };
            btnProceed.FlatAppearance.BorderSize = 0;
            btnProceed.Click += BtnProceed_Click;

            var btnExitFooter = new Button
            {
                Text = "❌ EXIT",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
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
                btnProceed.Location = new Point(btnExitFooter.Left - btnProceed.Width - 15, 20);
            };

            pnlFooter.Controls.Add(btnBack);
            pnlFooter.Controls.Add(btnProceed);
            pnlFooter.Controls.Add(btnExitFooter);

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

            // Left Section: Cabin Class & Selection Summary
            var grpCabin = new GroupBox
            {
                Text = "Cabin Class & Selection Summary",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 15, 0),
                Padding = new Padding(15),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var pnlCabinRows = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                BackColor = Color.FromArgb(30, 41, 59)
            };
            pnlCabinRows.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            pnlCabinRows.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            pnlCabinRows.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            // Row 1: Radio Buttons
            var pnlRadioCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 0, 10)
            };

            var rbEconomy = new RadioButton
            {
                Text = "Economy Class (1.0x Base Fare)",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(15, 12),
                AutoSize = true,
                Checked = true
            };
            rbEconomy.CheckedChanged += (s, e) => { if (rbEconomy.Checked) SetCabin(CabinClass.Economy); };

            var rbBusiness = new RadioButton
            {
                Text = "Business Class (1.85x Base Fare)",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(56, 189, 248),
                Location = new Point(15, 48),
                AutoSize = true
            };
            rbBusiness.CheckedChanged += (s, e) => { if (rbBusiness.Checked) SetCabin(CabinClass.Business); };

            var rbFirst = new RadioButton
            {
                Text = "First Class (3.20x Base Fare)",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(251, 191, 36),
                Location = new Point(15, 84),
                AutoSize = true
            };
            rbFirst.CheckedChanged += (s, e) => { if (rbFirst.Checked) SetCabin(CabinClass.FirstClass); };

            pnlRadioCard.Controls.Add(rbEconomy);
            pnlRadioCard.Controls.Add(rbBusiness);
            pnlRadioCard.Controls.Add(rbFirst);

            // Row 2: Selected Seat & Passenger Details Card
            var pnlSeatDisplayCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 0, 10)
            };

            lblSelectedSeatDisplay = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 11.5F),
                ForeColor = Color.FromArgb(14, 165, 233)
            };
            pnlSeatDisplayCard.Controls.Add(lblSelectedSeatDisplay);

            // Row 3: Fare Preview Calculation Card
            var pnlFareCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblFarePreview = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 11.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 185, 129)
            };
            pnlFareCard.Controls.Add(lblFarePreview);

            pnlCabinRows.Controls.Add(pnlRadioCard, 0, 0);
            pnlCabinRows.Controls.Add(pnlSeatDisplayCard, 0, 1);
            pnlCabinRows.Controls.Add(pnlFareCard, 0, 2);

            grpCabin.Controls.Add(pnlCabinRows);

            // Right Section: Interactive Seat Grid Map
            var grpSeatMap = new GroupBox
            {
                Text = "Aircraft Cabin Seating Map (2-2 Configuration)",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(15, 0, 0, 0),
                Padding = new Padding(15),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var pnlSeatMapRows = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                BackColor = Color.FromArgb(30, 41, 59)
            };
            pnlSeatMapRows.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlSeatMapRows.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

            var pnlGridCenter = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                BorderStyle = BorderStyle.FixedSingle,
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
                        Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                        Size = new Size(85, 55),
                        Location = new Point(45 + c * 100 + (c >= 2 ? 50 : 0), 20 + (r - 1) * 68),
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

                    pnlGridCenter.Controls.Add(btnSeat);
                    seatButtons[index++] = btnSeat;
                }
            }

            // Legend Bar
            var pnlLegend = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 41, 59),
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(15, 10, 0, 0)
            };

            AddLegendItem(pnlLegend, "Selected", Color.FromArgb(14, 165, 233));
            AddLegendItem(pnlLegend, "Occupied", Color.FromArgb(225, 29, 72));
            AddLegendItem(pnlLegend, "Available", Color.FromArgb(51, 65, 85));

            pnlSeatMapRows.Controls.Add(pnlGridCenter, 0, 0);
            pnlSeatMapRows.Controls.Add(pnlLegend, 0, 1);

            grpSeatMap.Controls.Add(pnlSeatMapRows);

            pnlMain.Controls.Add(grpCabin, 0, 0);
            pnlMain.Controls.Add(grpSeatMap, 1, 0);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);

            UpdateCalculations();
        }

        private void AddLegendItem(Panel parent, string text, Color color)
        {
            var pnlColor = new Panel
            {
                Size = new Size(20, 20),
                BackColor = color,
                Margin = new Padding(0, 3, 6, 0)
            };
            var lblText = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                AutoSize = true,
                Margin = new Padding(0, 3, 25, 0)
            };
            parent.Controls.Add(pnlColor);
            parent.Controls.Add(lblText);
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

            lblSelectedSeatDisplay.Text =
                $"SELECTED SEAT : {selectedSeat}\n\n" +
                $"PASSENGER     : {currentPassenger.FullName}\n\n" +
                $"CABIN CLASS   : {selectedCabin}";

            lblFarePreview.Text =
                $"BASE FARE RATE  : ${currentFlight.BaseFare:F2}\n\n" +
                $"CABIN SURCHARGE : ${cabinSurcharge:F2}\n\n" +
                $"SUBTOTAL FARE   : ${total:F2}";
        }

        private void BtnProceed_Click(object? sender, EventArgs e)
        {
            SoundHelper.PlayTap();
            FormNavigator.Navigate(this, new BaggageAscentForm(currentFlight, currentPassenger));
        }
    }
}
