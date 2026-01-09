
using System.Reflection;
using AssemblyApp.Classes;

namespace AssemblyApp;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        Shown += MainForm_Shown;
    }

    private void MainForm_Shown(object? sender, EventArgs e)
    {
        Version? frameworkVersion = FrameworkExtensions.GetTargetFrameworkVersion();
        VersonLabel.Text = frameworkVersion is not null ? 
            $"Target Framework Version: {frameworkVersion}" : 
            "Target Framework Version: Unknown";
    }
}
