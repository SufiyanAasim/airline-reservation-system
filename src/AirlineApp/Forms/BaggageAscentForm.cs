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
            btnBack.Click += (s, e) =>
            {
                SoundHelper.PlayTap();
                FormNavigator.Navigate(this, new SeatTaxiForm(currentFlight, currentPassenger));
            };

            var btnProceed = new Button
            {
                Text = "PROCEED TO CRUISING & ANALYTICS ➔",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(16, 185, 129),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(330, 45),
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
                Padding = new Padding(15),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var pnlBaggageRows = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                BackColor = Color.FromArgb(30, 41, 59)
            };
            pnlBaggageRows.RowStyles.Add(new RowStyle(SizeType.Absolute, 110F));
            pnlBaggageRows.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var pnlWeightCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 0, 10)
            };

            var lblWeight = new Label
            {
                Text = "Total Checked Baggage Weight (kg):",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(15, 15),
                AutoSize = true
            };

            numBaggageWeight = new NumericUpDown
            {
                Location = new Point(15, 45),
                Size = new Size(200, 32),
                Font = new Font("Segoe UI", 11.5F, FontStyle.Bold),
                Minimum = 0,
                Maximum = 100,
                Value = 28,
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White
            };
            numBaggageWeight.ValueChanged += (s, e) =>
            {
                SoundHelper.PlayTap();
                Recalculate();
            };

            pnlWeightCard.Controls.Add(lblWeight);
            pnlWeightCard.Controls.Add(numBaggageWeight);

            var pnlBaggageSummaryCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(20),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblBaggageSummary = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 11.5F),
                ForeColor = Color.FromArgb(56, 189, 248)
            };
            pnlBaggageSummaryCard.Controls.Add(lblBaggageSummary);

            pnlBaggageRows.Controls.Add(pnlWeightCard, 0, 0);
            pnlBaggageRows.Controls.Add(pnlBaggageSummaryCard, 0, 1);

            grpBaggage.Controls.Add(pnlBaggageRows);

            // Right Section: Meals & Amenity Toggles
            var grpServices = new GroupBox
            {
                Text = "In-Flight Dining & Luxury Add-Ons",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(15, 0, 0, 0),
                Padding = new Padding(15),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var pnlServiceRows = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                BackColor = Color.FromArgb(30, 41, 59)
            };
            pnlServiceRows.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F));
            pnlServiceRows.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var pnlDiningCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(15),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 0, 10)
            };

            var lblMeal = new Label
            {
                Text = "In-Flight Meal Preference:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(15, 12),
                AutoSize = true
            };

            comboMeal = new ComboBox
            {
                Location = new Point(15, 38),
                Size = new Size(350, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White
            };
            comboMeal.Items.AddRange(new object[] { "Executive Gourmet Halal Platter", "Vegan & Organic Salad Deluxe", "Diabetic & Low Sodium Option", "Gluten-Free Chef Special", "Child Friendly Meal Box" });
            comboMeal.SelectedIndex = 0;
            comboMeal.SelectedIndexChanged += (s, e) => SoundHelper.PlayTap();

            pnlDiningCard.Resize += (s, e) =>
            {
                comboMeal.Width = pnlDiningCard.Width - 30;
            };

            chkWifi = CreateCheckBox("High-Speed In-Flight Wi-Fi Pass (+$25.00)", 80);
            chkLounge = CreateCheckBox("VIP Airport Lounge Access (+$45.00)", 115);
            chkPriority = CreateCheckBox("Priority Express Boarding Pass (+$20.00)", 150);

            pnlDiningCard.Controls.Add(lblMeal);
            pnlDiningCard.Controls.Add(comboMeal);
            pnlDiningCard.Controls.Add(chkWifi);
            pnlDiningCard.Controls.Add(chkLounge);
            pnlDiningCard.Controls.Add(chkPriority);

            var pnlTotalCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(20),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblTotalPreview = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 11.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 185, 129)
            };
            pnlTotalCard.Controls.Add(lblTotalPreview);

            pnlServiceRows.Controls.Add(pnlDiningCard, 0, 0);
            pnlServiceRows.Controls.Add(pnlTotalCard, 0, 1);

            grpServices.Controls.Add(pnlServiceRows);

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
                Location = new Point(15, y),
                AutoSize = true,
                Cursor = Cursors.Hand
            };
            chk.CheckedChanged += (s, e) =>
            {
                SoundHelper.PlayTap();
                Recalculate();
            };
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
                $"CABIN ALLOWANCE : {allowance} KG\n\n" +
                $"CHECKED WEIGHT  : {currentPassenger.BaggageWeightKg:F1} KG\n\n" +
                $"EXCESS WEIGHT   : {excess:F1} KG\n\n" +
                $"EXCESS RATE     : $12.50 / KG\n\n" +
                $"EXCESS FEE      : ${excessFee:F2}\n\n" +
                $"BAGGAGE STATUS  : {(excess > 0 ? "EXCESS CHARGE APPLIED" : "ALLOWANCE COMPLIANT")}";

            decimal baseMult = FlightService.GetCabinMultiplier(currentPassenger.Cabin);
            decimal subtotal = currentFlight.BaseFare * baseMult;
            decimal grandTotal = subtotal + excessFee + addonTotal;

            lblTotalPreview.Text = 
                $"CABIN SUB-FARE  : ${subtotal:F2}\n\n" +
                $"EXCESS BAGGAGE  : ${excessFee:F2}\n\n" +
                $"SERVICE ADD-ONS : ${addonTotal:F2}\n\n" +
                $"RUNNING TOTAL   : ${grandTotal:F2}";
        }

        private void BtnProceed_Click(object? sender, EventArgs e)
        {
            SoundHelper.PlayTap();
            FormNavigator.Navigate(this, new AnalyticsCruisingForm(currentFlight, currentPassenger));
        }
    }
}
