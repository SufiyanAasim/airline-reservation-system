namespace AirlineApp.Services
{
    using System;
    using System.Media;

    public static class SoundHelper
    {
        public static void PlayTap()
        {
            try
            {
                SystemSounds.Asterisk.Play();
            }
            catch
            {
                // Silently ignore if audio hardware unavailable
            }
        }

        public static void PlayAlert()
        {
            try
            {
                SystemSounds.Exclamation.Play();
            }
            catch
            {
                // Silently ignore
            }
        }
    }
}
