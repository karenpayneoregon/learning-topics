
using ExportedVisualStudioExtensionsApp.Classes;
using ExportedVisualStudioExtensionsApp.Models;
using System.ComponentModel;
using System.Diagnostics;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ExportedVisualStudioExtensionsApp;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        Shown += MainForm_Shown!;
    }

    private BindingList<VsExtension>? _bindingList;
    private BindingSource _bindingSource;
    
    private void MainForm_Shown(object sender, EventArgs e)
    {
        var path = "File\\extensions2026.vsext";
        List<VsExtension>? extensions = ExtensionsReader.Get(path);

        _bindingList = new BindingList<VsExtension>(extensions ?? new List<VsExtension>());
        _bindingSource = new BindingSource();
        _bindingSource.DataSource = _bindingList;
        
        CoreNavigator.BindingSource = _bindingSource;

        ExtensionNameTextBox.DataBindings.Add("Text", _bindingSource, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
        LinkLabel1.DataBindings.Add("Text", _bindingSource, "MoreInfoUrl", true,
            DataSourceUpdateMode.OnPropertyChanged);

        LinkLabel1.LinkClicked += LinkLabel1_LinkClicked;
        CoreNavigator.BindingSource = _bindingSource;
    }

    private void LinkLabel1_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = _bindingList[_bindingSource.Position].MoreInfoUrl,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Unable to open link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
