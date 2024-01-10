namespace AirlineApp.Forms
{
    using AirlineApp.Models;

    // Backwards-compatibility alias wrapper delegating to MaydayForm
    public class MaydayCreditsForm : MaydayForm
    {
        public MaydayCreditsForm(Booking booking) : base(booking)
        {
        }
    }
}
