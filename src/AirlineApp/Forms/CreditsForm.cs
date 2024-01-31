namespace AirlineApp.Forms
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Services;

    public class CreditsForm : Form
    {
        private readonly Form? previousForm;

        public CreditsForm(Form? previousForm = null)
        {
            this.previousForm = previousForm;
            InitializeComponent();
            IconHelper.ApplyIcon(this);
        }

        private void InitializeComponent()
        {
            this.Text = "Airline Reservation System — System Credits & Architecture";
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
                Text = "System Architecture & Lead Developer Credits",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(22, 46),
                AutoSize = true
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
            btnExitHeader.Click += (s, e) => Application.Exit();

            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(lblHeader);
            headerPanel.Controls.Add(btnExitHeader);

            // Footer Navigation
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 85,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            bool isLoggedIn = AuthService.CurrentUser != null;
            string backText = previousForm != null ? "⇦ BACK TO PREVIOUS SCREEN" : (isLoggedIn ? "⇦ RETURN TO PORTAL" : "⇦ BACK TO LOGIN");

            var btnBack = new Button
            {
                Text = backText,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(51, 65, 85),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(260, 45),
                Location = new Point(25, 20),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) =>
            {
                if (previousForm != null)
                {
                    FormNavigator.Navigate(this, previousForm);
                }
                else if (AuthService.CurrentUser != null)
                {
                    FormNavigator.Navigate(this, new WelcomeClearanceForm());
                }
                else
                {
                    FormNavigator.Navigate(this, new LoginForm());
                }
            };

            pnlFooter.Controls.Add(btnBack);

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

            // Left Section: Author Profile Card
            var grpAuthor = new GroupBox
            {
                Text = "Lead Developer & System Architect",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 15, 0),
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var pnlAuthorCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(25),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblAuthorName = new Label
            {
                Text = "Mohammad Sufiyan Aasim",
                Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                ForeColor = Color.FromArgb(14, 165, 233),
                Dock = DockStyle.Top,
                Height = 55
            };

            var lblAuthorRole = new Label
            {
                Text = "System Architect · AI/MLOps · Docs\nFull-Stack C# .NET Windows Desktop Engineer",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Dock = DockStyle.Top,
                Height = 65
            };

            var pnlAuthorButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 60,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 10, 0, 0)
            };

            var btnGithub = new Button
            {
                Text = "🌐 GITHUB PROFILE",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(30, 41, 59),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(200, 42),
                Margin = new Padding(0, 0, 15, 0),
                Cursor = Cursors.Hand
            };
            btnGithub.FlatAppearance.BorderSize = 1;
            btnGithub.FlatAppearance.BorderColor = Color.FromArgb(14, 165, 233);
            btnGithub.Click += (s, e) => OpenUrl("https://github.com/SufiyanAasim");

            var btnEmail = new Button
            {
                Text = "✉️ EMAIL CONTACT",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(30, 41, 59),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(200, 42),
                Cursor = Cursors.Hand
            };
            btnEmail.FlatAppearance.BorderSize = 1;
            btnEmail.FlatAppearance.BorderColor = Color.FromArgb(16, 185, 129);
            btnEmail.Click += (s, e) => OpenUrl("mailto:sufiyanaasim@outlook.com");

            pnlAuthorButtons.Controls.Add(btnGithub);
            pnlAuthorButtons.Controls.Add(btnEmail);

            var lblAuthorBio = new Label
            {
                Text = "Designed and engineered the complete 8-phase flight clearance wizard, interactive 2-2 aircraft seating grid, GDI+ yield analytics engine, flat-file booking logger, and emergency Mayday protocol.",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 15, 0, 0)
            };

            pnlAuthorCard.Controls.Add(lblAuthorBio);
            pnlAuthorCard.Controls.Add(pnlAuthorButtons);
            pnlAuthorCard.Controls.Add(lblAuthorRole);
            pnlAuthorCard.Controls.Add(lblAuthorName);

            grpAuthor.Controls.Add(pnlAuthorCard);

            // Right Section: Project Architecture & Specs
            var grpSpecs = new GroupBox
            {
                Text = "Project Specifications & License",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                Dock = DockStyle.Fill,
                Margin = new Padding(15, 0, 0, 0),
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 41, 59)
            };

            var lblSpecsBody = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 11F),
                ForeColor = Color.FromArgb(56, 189, 248),
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(20)
            };

            lblSpecsBody.Text =
                "PROJECT NAME    : Airline Reservation System\n\n" +
                "FRAMEWORK       : .NET 8.0 Windows Desktop (WinForms)\n\n" +
                "VERSION TAG     : v6.0.0 Mayday Release\n\n" +
                "GUI GRAPHICS    : System.Drawing (Custom GDI+ Double Buffer)\n\n" +
                "PERSISTENCE     : Offline Flat-File Travel Ledger\n\n" +
                "SOUND ENGINE    : Custom WAV PCM Wave Synthesizer\n\n" +
                "REPOSITORY      : github.com/SufiyanAasim/airline-reservation-system\n\n" +
                "LICENSE         : MIT License © 2023-2026";

            grpSpecs.Controls.Add(lblSpecsBody);

            pnlMain.Controls.Add(grpAuthor, 0, 0);
            pnlMain.Controls.Add(grpSpecs, 1, 0);

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(headerPanel);
        }

        private void OpenUrl(string url)
        {
            try
            {
                SoundHelper.PlayTap();
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("LINK OPEN ERROR", ex.Message, true);
            }
        }
    }
}
