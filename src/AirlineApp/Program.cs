namespace AirlineApp
{
    using System;
    using System.Windows.Forms;
    using AirlineApp.Forms;

    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }
    }
}
