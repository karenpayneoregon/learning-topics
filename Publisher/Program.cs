using Publisher.Classes;
using System;
using System.Windows.Forms;

namespace Publisher;

static class Program
{
    /// <summary>
    /// The main entry point for the Publisher application.
    /// </summary>
    /// <remarks>
    /// This method initializes application-wide settings such as high DPI mode, visual styles, 
    /// and text rendering compatibility. It also configures logging for the development environment 
    /// and starts the main application form.
    /// </remarks>
    [STAThread]
    static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        
        SetupLogging.Development();
        
        Application.Run(new Form1());
    }
}