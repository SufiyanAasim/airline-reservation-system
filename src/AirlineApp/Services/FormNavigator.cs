namespace AirlineApp.Services
{
    using System.Windows.Forms;

    public static class FormNavigator
    {
        public static void Navigate(Form current, Form next)
        {
            IconHelper.ApplyIcon(next);
            next.StartPosition = FormStartPosition.CenterScreen;
            next.Show();
            current.Hide();
        }
    }
}
