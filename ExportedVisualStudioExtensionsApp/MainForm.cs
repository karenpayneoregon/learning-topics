
using System.ComponentModel;
using ExportedVisualStudioExtensionsApp.Classes;
using ExportedVisualStudioExtensionsApp.Models;

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
        var path = "C:\\OED\\DotnetLand\\extensions2026.vsext";
        List<VsExtension>? extensions = ExtensionsReader.Get(path);

        _bindingList = new BindingList<VsExtension>(extensions ?? new List<VsExtension>());
        _bindingSource = new BindingSource();
        _bindingSource.DataSource = _bindingList;
        
        CoreNavigator.BindingSource = _bindingSource;

        ExtensionNameTextBox.DataBindings.Add("Text", _bindingSource, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
    }
    
}
