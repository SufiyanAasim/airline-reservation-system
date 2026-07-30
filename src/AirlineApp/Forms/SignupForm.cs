namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Models;
    using AirlineApp.Services;

    public class SignupForm : Form
    {
        private TextBox txtFullName = null!;
        private TextBox txtEmail = null!;
        private TextBox txtPassword = null!;
        private TextBox txtConfirmPassword = null!;
        private Button btnEyePassword1 = null!;
        private Button btnEyePassword2 = null!;
        private ComboBox comboRole = null!;
        private Panel container = null!;

        private bool pass1Hidden = true;
        private bool pass2Hidden = true;

        public SignupForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — New User Account Sign Up";
            this.Size = new Size(1150, 750);
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(15, 23, 42);
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.Resize += (s, e) => CenterSignupCard();

            // Header Banner
            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblBadge = new Label
            {
                Text = "ACCOUNT REGISTRATION",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 185, 129),
                BackColor = Color.FromArgb(6, 78, 59),
                Location = new Point(30, 18),
                AutoSize = true,
                Padding = new Padding(6, 3, 6, 3)
            };

            var lblHeader = new Label
            {
                Text = "Register New Passenger / Operations Staff Profile",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(27, 48),
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
                Size = new Size(530, 540),
                BackColor = Color.FromArgb(30, 41, 59),
                BorderStyle = BorderStyle.FixedSingle
            };

            int y = 20;

            // Full Name
            AddInputLabel(container, "Full Name:", y);
            txtFullName = CreateTextBox("Capt. Sufiyan Aasim", y + 24, 470);
            container.Controls.Add(txtFullName);
            y += 65;

            // Email
            AddInputLabel(container, "Email Address:", y);
            txtEmail = CreateTextBox("sufiyanaasim@outlook.com", y + 24, 470);
            container.Controls.Add(txtEmail);
            y += 65;

            // Role
            AddInputLabel(container, "User Role / Designation:", y);
            comboRole = new ComboBox
            {
                Location = new Point(30, y + 24),
                Size = new Size(470, 30),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White
            };
            comboRole.Items.AddRange(new object[] { "Passenger / Customer", "Flight Operations Manager" });
            comboRole.SelectedIndex = 0;
            container.Controls.Add(comboRole);
            y += 65;

            // Password + Eye Button 1 (ALIGNED INLINE!)
            AddInputLabel(container, "Password:", y);
            txtPassword = CreateTextBox("pass123", y + 24, 410);
            txtPassword.UseSystemPasswordChar = true;

            btnEyePassword1 = CreateEyeButton(y + 24, 446);
            btnEyePassword1.Click += (s, e) =>
            {
                SoundHelper.PlayTap();
                pass1Hidden = !pass1Hidden;
                txtPassword.UseSystemPasswordChar = pass1Hidden;
                btnEyePassword1.Text = pass1Hidden ? "👁" : "🙈";
            };

            container.Controls.Add(txtPassword);
            container.Controls.Add(btnEyePassword1);
            y += 65;

            // Confirm Password + Eye Button 2 (ALIGNED INLINE!)
            AddInputLabel(container, "Confirm Password:", y);
            txtConfirmPassword = CreateTextBox("pass123", y + 24, 410);
            txtConfirmPassword.UseSystemPasswordChar = true;

            btnEyePassword2 = CreateEyeButton(y + 24, 446);
            btnEyePassword2.Click += (s, e) =>
            {
                SoundHelper.PlayTap();
                pass2Hidden = !pass2Hidden;
                txtConfirmPassword.UseSystemPasswordChar = pass2Hidden;
                btnEyePassword2.Text = pass2Hidden ? "👁" : "🙈";
            };

            container.Controls.Add(txtConfirmPassword);
            container.Controls.Add(btnEyePassword2);
            y += 75;

            // Register Button
            var btnRegister = new Button
            {
                Text = "CREATE ACCOUNT & START FLIGHT CLEARANCE ➔",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(16, 185, 129),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(470, 44),
                Location = new Point(30, y),
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click;
            container.Controls.Add(btnRegister);
            y += 55;

            // Back to Login Link
            var btnBackLogin = new Button
            {
                Text = "ALREADY HAVE AN ACCOUNT? LOGIN HERE",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                BackColor = Color.FromArgb(15, 23, 42),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(470, 36),
                Location = new Point(30, y),
                Cursor = Cursors.Hand
            };
            btnBackLogin.FlatAppearance.BorderSize = 0;
            btnBackLogin.Click += (s, e) => FormNavigator.Navigate(this, new LoginForm());
            container.Controls.Add(btnBackLogin);

            pnlMain.Controls.Add(container);

            this.Controls.Add(pnlMain);
            this.Controls.Add(headerPanel);

            CenterSignupCard();
        }

        private void CenterSignupCard()
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
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
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
                Font = new Font("Segoe UI", 10.5F),
                Location = new Point(30, y),
                Size = new Size(width, 30),
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private Button CreateEyeButton(int y, int x)
        {
            var btn = new Button
            {
                Text = "👁",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(14, 165, 233),
                BackColor = Color.FromArgb(15, 23, 42),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(54, 30),
                Location = new Point(x, y),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(51, 65, 85);
            return btn;
        }

        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            SoundHelper.PlayTap();
            var res = AuthService.Register(txtFullName.Text, txtEmail.Text, txtPassword.Text, txtConfirmPassword.Text, comboRole.SelectedItem?.ToString() ?? "Passenger");
            if (!res.Success)
            {
                CustomMessageBox.Show("REGISTRATION ERROR", res.Message, true);
                return;
            }

            CustomMessageBox.Show("ACCOUNT CREATED", res.Message);
            FormNavigator.Navigate(this, new WelcomeClearanceForm());
        }
    }
}
