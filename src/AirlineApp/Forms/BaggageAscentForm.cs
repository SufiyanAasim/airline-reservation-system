namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Models;
    using AirlineApp.Services;

    public class BaggageAscentForm : Form
    {
        private readonly Flight currentFlight;
        private readonly Passenger currentPassenger;
        private NumericUpDown numBaggageWeight = null!;
        private ComboBox comboMeal = null!;
        private CheckBox chkWifi = null!;
        private CheckBox chkLounge = null!;
        private CheckBox chkPriority = null!;
        private Label lblBaggageSummary = null!;
        private Label lblTotalPreview = null!;

        public BaggageAscentForm(Flight flight, Passenger passenger)
        {
            this.currentFlight = flight;
            this.currentPassenger = passenger;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — v3.0.0 [Ascent Phase - Baggage & Amenities]";
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
                Text = "v3.0.0 ASCENT & IN-FLIGHT SERVICES",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 185, 129),
                BackColor = Color.FromArgb(6, 78, 59),
                Location = new Point(25, 15),
                AutoSize = true,
                Padding = new Padding(6, 3, 6, 3)
            };

            var lblHeader = new Label
            {
                Text = $"Baggage Allowance & In-Flight Amenity Add-Ons ({currentFlight.FlightNumber})",
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

            // Footer Navigation Bar
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 85,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var btnBack = new Button
            {
                Text = "⇦ BACK TO TAXI",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 65, 85),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(190, 45),
                Location = new Point(25, 20),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) => FormNavigator.Navigate(this, new SeatTaxiForm(currentFlight, currentPassenger));

            var btnProceed = new Button
            {
                Text = "PROCEED TO CRUISING & ANALYTICS ➔",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(16, 185, 129),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(330, 45),
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

            // Left Section: Checked Baggage Allowance Calculator
            var grpBaggage = new GroupBox
            {
                Text = "Checked Baggage Weight Calculator",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 15, 0),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblWeight = new Label
            {
                Text = "Total Checked Baggage Weight (kg):",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(25, 40),
                AutoSize = true
            };

            numBaggageWeight = new NumericUpDown
            {
                Location = new Point(25, 70),
                Size = new Size(180, 30),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Minimum = 0,
                Maximum = 100,
                Value = 28,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White
            };
            numBaggageWeight.ValueChanged += (s, e) => Recalculate();

            lblBaggageSummary = new Label
            {
                Font = new Font("Consolas", 10.5F),
                ForeColor = Color.FromArgb(56, 189, 248),
                Location = new Point(25, 120),
                Size = new Size(450, 220),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            grpBaggage.Controls.Add(lblWeight);
            grpBaggage.Controls.Add(numBaggageWeight);
            grpBaggage.Controls.Add(lblBaggageSummary);

            // Right Section: Meals & Amenity Toggles
            var grpServices = new GroupBox
            {
                Text = "In-Flight Dining & Luxury Add-Ons",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(15, 0, 0, 0),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblMeal = new Label
            {
                Text = "In-Flight Meal Preference:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(25, 40),
                AutoSize = true
            };

            comboMeal = new ComboBox
            {
                Location = new Point(25, 70),
                Size = new Size(420, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White
            };
            comboMeal.Items.AddRange(new object[] { "Executive Gourmet Halal Platter", "Vegan & Organic Salad Deluxe", "Diabetic & Low Sodium Option", "Gluten-Free Chef Special", "Child Friendly Meal Box" });
            comboMeal.SelectedIndex = 0;

            chkWifi = CreateCheckBox("High-Speed In-Flight Wi-Fi Pass (+$25.00)", 125);
            chkLounge = CreateCheckBox("VIP Airport Lounge Access (+$45.00)", 165);
            chkPriority = CreateCheckBox("Priority Express Boarding Pass (+$20.00)", 205);

            lblTotalPreview = new Label
            {
                Font = new Font("Consolas", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 185, 129),
                Location = new Point(25, 260),
                Size = new Size(420, 100),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            grpServices.Controls.Add(lblMeal);
            grpServices.Controls.Add(comboMeal);
            grpServices.Controls.Add(chkWifi);
            grpServices.Controls.Add(chkLounge);
            grpServices.Controls.Add(chkPriority);
            grpServices.Controls.Add(lblTotalPreview);

            pnlMain.Controls.Add(grpBaggage, 0, 0);
            pnlMain.Controls.Add(grpServices, 1, 0);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);

            Recalculate();
        }

        private CheckBox CreateCheckBox(string text, int y)
        {
            var chk = new CheckBox
            {
                Text = text,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(25, y),
                AutoSize = true,
                Cursor = Cursors.Hand
            };
            chk.CheckedChanged += (s, e) => Recalculate();
            return chk;
        }

        private void Recalculate()
        {
            currentPassenger.BaggageWeightKg = (double)numBaggageWeight.Value;
            currentPassenger.MealPreference = comboMeal.SelectedItem?.ToString() ?? "Gourmet Halal";
            currentPassenger.WifiPass = chkWifi.Checked;
            currentPassenger.LoungeAccess = chkLounge.Checked;
            currentPassenger.PriorityBoarding = chkPriority.Checked;

            int allowance = currentPassenger.Cabin switch
            {
                CabinClass.Economy => 20,
                CabinClass.Business => 35,
                CabinClass.FirstClass => 50,
                _ => 20
            };

            double excess = Math.Max(0.0, currentPassenger.BaggageWeightKg - allowance);
            decimal excessFee = FlightService.CalculateExcessBaggageFee(currentPassenger.BaggageWeightKg, currentPassenger.Cabin);

            decimal addonTotal = 0m;
            if (chkWifi.Checked) addonTotal += 25.00m;
            if (chkLounge.Checked) addonTotal += 45.00m;
            if (chkPriority.Checked) addonTotal += 20.00m;

            lblBaggageSummary.Text = 
                $"CABIN CLASS ALLOWANCE : {allowance} KG\n" +
                $"TOTAL CHECKED WEIGHT  : {currentPassenger.BaggageWeightKg:F1} KG\n" +
                $"EXCESS WEIGHT WEIGHT  : {excess:F1} KG\n" +
                $"EXCESS RATE           : $12.50 / KG\n" +
                $"EXCESS BAGGAGE FEE    : ${excessFee:F2}\n" +
                $"STATUS                : {(excess > 0 ? "EXCESS CHARGE APPLIED" : "ALLOWANCE COMPLIANT")}";

            decimal baseMult = FlightService.GetCabinMultiplier(currentPassenger.Cabin);
            decimal subtotal = currentFlight.BaseFare * baseMult;
            decimal grandTotal = subtotal + excessFee + addonTotal;

            lblTotalPreview.Text = 
                $"CABIN SUB-FARE   : ${subtotal:F2}\n" +
                $"EXCESS BAGGAGE   : ${excessFee:F2}\n" +
                $"SERVICE ADD-ONS  : ${addonTotal:F2}\n" +
                $"RUNNING TOTAL    : ${grandTotal:F2}";
        }

        private void BtnProceed_Click(object? sender, EventArgs e)
        {
            FormNavigator.Navigate(this, new AnalyticsCruisingForm(currentFlight, currentPassenger));
        }
    }
}
