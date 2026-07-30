namespace AirlineApp.Services
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public static class IconHelper
    {
        private static Icon? loadedIcon;

        public static void ApplyIcon(Form form)
        {
            try
            {
                if (loadedIcon == null)
                {
                    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    string[] candidatePaths = new[]
                    {
                        Path.Combine(baseDir, "Resources", "app_icon.ico"),
                        Path.Combine(baseDir, "app_icon.ico"),
                        Path.Combine(baseDir, "..", "..", "..", "Resources", "app_icon.ico"),
                        Path.Combine(baseDir, "..", "..", "..", "..", "assets", "app_icon.ico")
                    };

                    foreach (var path in candidatePaths)
                    {
                        string fullPath = Path.GetFullPath(path);
                        if (File.Exists(fullPath))
                        {
                            loadedIcon = new Icon(fullPath);
                            break;
                        }
                    }
                }

                if (loadedIcon != null)
                {
                    form.Icon = loadedIcon;
                }
            }
            catch
            {
                // Fallback gracefully if icon rendering is constrained
            }
        }
    }
}
