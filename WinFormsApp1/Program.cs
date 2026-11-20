using System.Diagnostics;

namespace WinFormsApp1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.SetColorMode(SystemColorMode.Dark);
            
            Application.SystemVisualSettingsChanged += (sender, e) =>
            {
                if ((e.Changed & SystemVisualSettingsCategories.AccentColor) == 0) return;
                Color accent = e.NewSettings.AccentColor;
                Debug.WriteLine($"Accent color changed to {accent.Name}.");

            };
            
            Application.Run(new Form1());
        }
    }
}