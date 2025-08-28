using System.Diagnostics;
using ConsoleConfigurationLibrary.Classes;
using Microsoft.Data.SqlClient;
using SqlLibrary.Classes;
using SqlLibrary.Models;

#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace TableDependenciesSample;

public partial class Form1 : Form
{

    public Form1()
    {
        InitializeComponent();
        
    }

 
    private async void LoadTreeViewButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (DependencyTreeView.Nodes.Count > 0)
            {
                DependencyTreeView.Nodes.Clear();
                DependencyTreeView.NodeMouseClick -= DependencyTreeView_NodeMouseClick!;
            }

            await LoadDatabaseData(AppConnections.Instance.MainConnection);
            await LoadDatabaseData(AppConnections.Instance.SecondaryConnection);

            DependencyTreeView.NodeMouseClick += DependencyTreeView_NodeMouseClick!;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void DependencyTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Node.Tag is not null)
        {
            var details = e.Node.Tag as TreeDataItem;
            Debug.WriteLine(details);
        }
    }


    private async Task LoadDatabaseData(string connectionString)
    {
        Font boldFont = new(DependencyTreeView.Font, FontStyle.Bold);

        IReadOnlyList<DependencyGroupItem> result = await SqlServerHelpers
            .TableDependenciesAsync(connectionString);

        DependencyTreeView.BeginUpdate();
        var parentNode = DependencyTreeView.Nodes.Add(TableName(connectionString));

        try
        {
            foreach (var tableItem in result)
            {
                var node = parentNode.Nodes.Add(tableItem.TableName);

                foreach (var dependsItem in tableItem.List)
                {
                    var database = parentNode.Text;
                    var table = $"[{dependsItem.ParentTable}]";
                    var column = dependsItem.ReferenceColumn;

                    TreeNode childNode = node.Nodes.Add(dependsItem.ReferenceTable);
                    childNode.Tag = new TreeDataItem { Database = database, Catalog = table, ColumnName = column };
                    if (dependsItem.ReferenceTable != "Contacts") continue;

                    childNode.NodeFont = boldFont;
                    childNode.ForeColor = Color.Crimson;
                }
            }

            DependencyTreeView.ExpandAll();
        }
        finally
        {
            DependencyTreeView.EndUpdate();
        }

        DependencyTreeView.SelectedNode = DependencyTreeView.Nodes[0];
        ActiveControl = DependencyTreeView;

        return;

        string TableName(string connection)
        {
            SqlConnectionStringBuilder builder = new(connection);
            return builder.InitialCatalog;
        }
    }

    private void ClearTreeView_Click(object sender, EventArgs e)
    {
        DependencyTreeView.Nodes.Clear();
    }
}
