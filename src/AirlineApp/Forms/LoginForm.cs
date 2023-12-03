namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Services;

    public class LoginForm : Form
    {
        private TextBox txtEmail = null!;
        private TextBox txtPassword = null!;
        private Button btnEyePassword = null!;
        private ComboBox comboRole = null!;
        private bool isPasswordHidden = true;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — User Authentication & Login";
            this.Size = new Size(520, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(15, 23, 42); // Navy Dark
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Header Banner
            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblBadge = new Label
            {
                Text = "SECURE AVIATION PORTAL",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(14, 165, 233),
                BackColor = Color.FromArgb(12, 74, 110),
                Location = new Point(25, 20),
                AutoSize = true,
                Padding = new Padding(6, 3, 6, 3)
            };

            var lblHeader = new Label
            {
                Text = "Airline Reservation System Login",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(22, 50),
                AutoSize = true
            };

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);

            // Container Panel
            var container = new Panel
            {
                Location = new Point(35, 130),
                Size = new Size(435, 430),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            // Email Field
            var lblEmail = new Label
            {
                Text = "Email Address:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(25, 25),
                AutoSize = true
            };

            txtEmail = new TextBox
            {
                Text = "sufiyanaasim@outlook.com",
                Font = new Font("Segoe UI", 11F),
                Location = new Point(25, 55),
                Size = new Size(380, 32),
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Password Field + Eye Button
            var lblPassword = new Label
            {
                Text = "Password:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(25, 105),
                AutoSize = true
            };

            txtPassword = new TextBox
            {
                Text = "admin123",
                Font = new Font("Segoe UI", 11F),
                Location = new Point(25, 135),
                Size = new Size(330, 32),
                UseSystemPasswordChar = true,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Eye Toggle Button (👁 / 🙈)
            btnEyePassword = new Button
            {
                Text = "👁",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(14, 165, 233),
                BackColor = Color.FromArgb(15, 23, 42),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(46, 32),
                Location = new Point(360, 135),
                Cursor = Cursors.Hand
            };
            btnEyePassword.FlatAppearance.BorderSize = 1;
            btnEyePassword.FlatAppearance.BorderColor = Color.FromArgb(51, 65, 85);
            btnEyePassword.Click += BtnEyePassword_Click;

            // Role Selector
            var lblRole = new Label
            {
                Text = "Login Access Role:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(25, 185),
                AutoSize = true
            };

            comboRole = new ComboBox
            {
                Location = new Point(25, 215),
                Size = new Size(380, 32),
                Font = new Font("Segoe UI", 10.5F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White
            };
            comboRole.Items.AddRange(new object[] { "Flight Operations Manager", "Passenger / Customer" });
            comboRole.SelectedIndex = 0;

            // Login Button
            var btnLogin = new Button
            {
                Text = "LOGIN TO FLIGHT ENGINE ➔",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(14, 165, 233),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(380, 45),
                Location = new Point(25, 275),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            // Signup Redirect Button
            var btnSignupRedirect = new Button
            {
                Text = "NEW USER? CREATE AN ACCOUNT",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                BackColor = Color.FromArgb(15, 23, 42),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(380, 38),
                Location = new Point(25, 335),
                Cursor = Cursors.Hand
            };
            btnSignupRedirect.FlatAppearance.BorderSize = 0;
            btnSignupRedirect.Click += (s, e) => FormNavigator.Navigate(this, new SignupForm());

            container.Controls.Add(lblEmail);
            container.Controls.Add(txtEmail);
            container.Controls.Add(lblPassword);
            container.Controls.Add(txtPassword);
            container.Controls.Add(btnEyePassword);
            container.Controls.Add(lblRole);
            container.Controls.Add(comboRole);
            container.Controls.Add(btnLogin);
            container.Controls.Add(btnSignupRedirect);

            this.Controls.Add(container);
            this.Controls.Add(headerPanel);
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
            var res = AuthService.Login(txtEmail.Text, txtPassword.Text);
            if (!res.Success)
            {
                CustomMessageBox.Show("AUTHENTICATION FAILED", res.Message, true);
                return;
            }

            CustomMessageBox.Show("LOGIN SUCCESSFUL", $"Welcome back, {res.User?.FullName} ({res.User?.Role})! Initializing Clearance Phase.");
            FormNavigator.Navigate(this, new WelcomeClearanceForm());
        }
    }
}
