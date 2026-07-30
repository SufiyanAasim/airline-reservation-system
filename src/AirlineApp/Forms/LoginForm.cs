namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Models;
    using AirlineApp.Services;

    public class LoginForm : Form
    {
        private TextBox txtEmail = null!;
        private TextBox txtPassword = null!;
        private Button btnEyePassword = null!;
        private ComboBox comboRole = null!;
        private Panel container = null!;
        private bool isPasswordHidden = true;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — User Authentication & Clearance Portal";
            this.Size = new Size(1150, 750);
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(15, 23, 42);
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.Resize += (s, e) => CenterLoginCard();

            // Header Banner
            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblBadge = new Label
            {
                Text = "AUTHENTICATION & CLEARANCE PORTAL",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(14, 165, 233),
                BackColor = Color.FromArgb(12, 74, 110),
                Location = new Point(30, 18),
                AutoSize = true,
                Padding = new Padding(6, 3, 6, 3)
            };

            var lblHeader = new Label
            {
                Text = "Airline Reservation System — Operations & Passenger Portal",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(27, 48),
                AutoSize = true
            };

            // Credits Button on Login Screen Header Banner!
            var btnCreditsHeader = new Button
            {
                Text = "⭐ SYSTEM CREDITS",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(139, 92, 246),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(170, 38),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(940, 28),
                Cursor = Cursors.Hand
            };
            btnCreditsHeader.FlatAppearance.BorderSize = 0;
            btnCreditsHeader.Click += (s, e) =>
            {
                var dummyFlight = FlightService.GetFlights()[0];
                var dummyPassenger = new Passenger { FullName = "Capt. Sufiyan Aasim", PassportOrId = "PK-98234109" };
                FormNavigator.Navigate(this, new MaydayCreditsForm(FlightService.CalculateFullBooking(dummyFlight, dummyPassenger)));
            };

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);
            headerPanel.Controls.Add(btnCreditsHeader);

            // Centered Main Content Panel
            var pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42)
            };

            container = new Panel
            {
                Size = new Size(490, 490),
                BackColor = Color.FromArgb(30, 41, 59),
                BorderStyle = BorderStyle.FixedSingle
            };

            int y = 25;

            // Role Selector (RBAC)
            AddInputLabel(container, "Select System Access Role (RBAC):", y);
            comboRole = new ComboBox
            {
                Location = new Point(30, y + 26),
                Size = new Size(430, 32),
                Font = new Font("Segoe UI", 10.5F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White
            };
            comboRole.Items.AddRange(new object[] { "Flight Operations Manager", "Passenger / Customer" });
            comboRole.SelectedIndex = 0;
            container.Controls.Add(comboRole);
            y += 75;

            // Email Field
            AddInputLabel(container, "Email Address:", y);
            txtEmail = CreateTextBox("sufiyanaasim@outlook.com", y + 26, 430);
            container.Controls.Add(txtEmail);
            y += 75;

            // Password Field + Eye Toggle Button (PERFECTLY ALIGNED VERTICALLY & HORIZONTALLY!)
            AddInputLabel(container, "Account Password:", y);

            txtPassword = new TextBox
            {
                Text = "admin123",
                Font = new Font("Segoe UI", 11F),
                Location = new Point(30, y + 26),
                Size = new Size(372, 32),
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtPassword.UseSystemPasswordChar = true;

            btnEyePassword = new Button
            {
                Text = "👁",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(14, 165, 233),
                BackColor = Color.FromArgb(15, 23, 42),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(54, 32),
                Location = new Point(406, y + 26),
                Cursor = Cursors.Hand
            };
            btnEyePassword.FlatAppearance.BorderSize = 1;
            btnEyePassword.FlatAppearance.BorderColor = Color.FromArgb(51, 65, 85);
            btnEyePassword.Click += BtnEyePassword_Click;

            container.Controls.Add(txtPassword);
            container.Controls.Add(btnEyePassword);
            y += 85;

            // Login Button
            var btnLogin = new Button
            {
                Text = "AUTHENTICATE & ENTER PORTAL ➔",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(14, 165, 233),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(430, 48),
                Location = new Point(30, y),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;
            container.Controls.Add(btnLogin);
            y += 60;

            // Register Link Button
            var btnRegister = new Button
            {
                Text = "DONT HAVE AN ACCOUNT? REGISTER HERE",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                BackColor = Color.FromArgb(15, 23, 42),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(430, 38),
                Location = new Point(30, y),
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += (s, e) => FormNavigator.Navigate(this, new SignupForm());
            container.Controls.Add(btnRegister);

            pnlMain.Controls.Add(container);

            this.Controls.Add(pnlMain);
            this.Controls.Add(headerPanel);

            CenterLoginCard();
        }

        private void CenterLoginCard()
        {
            if (container != null && container.Parent != null)
            {
                int x = (container.Parent.ClientSize.Width - container.Width) / 2;
                int y = (container.Parent.ClientSize.Height - container.Height) / 2;
                container.Location = new Point(Math.Max(20, x), Math.Max(20, y));
            }
        }

        private void AddInputLabel(Panel parent, string text, int y)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(30, y),
                AutoSize = true
            };
            parent.Controls.Add(lbl);
        }

        private TextBox CreateTextBox(string defaultVal, int y, int width)
        {
            return new TextBox
            {
                Text = defaultVal,
                Font = new Font("Segoe UI", 11F),
                Location = new Point(30, y),
                Size = new Size(width, 32),
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private void BtnEyePassword_Click(object? sender, EventArgs e)
        {
            SoundHelper.PlayTap();
            isPasswordHidden = !isPasswordHidden;
            txtPassword.UseSystemPasswordChar = isPasswordHidden;
            btnEyePassword.Text = isPasswordHidden ? "👁" : "🙈";
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            SoundHelper.PlayTap();
            string selectedRole = comboRole.SelectedItem?.ToString() ?? "Passenger / Customer";
            var res = AuthService.Login(txtEmail.Text, txtPassword.Text, selectedRole);
            if (!res.Success)
            {
                CustomMessageBox.Show("AUTHENTICATION FAILURE", res.Message, true);
                return;
            }

            CustomMessageBox.Show("WELCOME ABOARD", $"Authenticated as {selectedRole}.\nRedirecting to Flight Departure Clearance Engine.");
            FormNavigator.Navigate(this, new WelcomeClearanceForm());
        }
    }
}
