namespace AirlineApp.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using AirlineApp.Services;

    public class CustomMessageBox : Form
    {
        private CustomMessageBox(string title, string message, bool isWarning = false)
        {
            SoundHelper.PlayAlert();
            this.Text = title;
            this.Size = new Size(460, 240);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(15, 23, 42); // Deep Navy

            var lblTitle = new Label
            {
                Text = title.ToUpper(),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = isWarning ? Color.FromArgb(245, 158, 11) : Color.FromArgb(14, 165, 233),
                Location = new Point(20, 15),
                AutoSize = true
            };

            var lblMsg = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(20, 50),
                Size = new Size(400, 100)
            };

            var btnOk = new Button
            {
                Text = "ACKNOWLEDGE",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = isWarning ? Color.FromArgb(225, 29, 72) : Color.FromArgb(14, 165, 233),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(130, 36),
                Location = new Point(290, 150),
                Cursor = Cursors.Hand
            };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Click += (s, e) => { this.DialogResult = DialogResult.OK; this.Close(); };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblMsg);
            this.Controls.Add(btnOk);
        }

        public static void Show(string title, string message, bool isWarning = false)
        {
            using var msgBox = new CustomMessageBox(title, message, isWarning);
            msgBox.ShowDialog();
        }
    }
}
