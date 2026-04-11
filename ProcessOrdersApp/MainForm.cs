using System.ComponentModel;
using System.Diagnostics;
using ProcessOrdersApp.Classes;
using ProcessOrdersApp.Classes.Configuration;
using ProcessOrdersApp.Classes.Extensions;
using ProcessOrdersApp.Components;
using ProcessOrdersApp.Models;

namespace ProcessOrdersApp;

public partial class MainForm : Form
{
    private SortableBindingList<OrdersResults> _ordersBindingList;
    private BindingSource _ordersBindingSource = new();

    public MainForm()
    {
        InitializeComponent();
        Shown += MainForm_Shown;
    }

    private void MainForm_Shown(object? sender, EventArgs e)
    {

        dataGridView1.AllowUserToAddRows = false;

        BindingNavigator1.AboutItemButton.Click += AboutItemButton_Click;
        BindingNavigator1.CurrentItemButton.Click += CurrentItemButton_Click;

        //OrdersCsvExporter.ExportOrdersToCsv(FileSettings.Instance.FileName);

        if (!ImportOrders())
        {
            Dialogs.Information(this, "Cannot open the CSV file for reading.");
        }
        else
        {
            dataGridView1.DataError += DataGridView_DataError;
            dataGridView1.CurrentCellDirtyStateChanged += DataGridView_CurrentCellDirtyStateChanged;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
        }
    }

    private void DataGridView1_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (e is { RowIndex: >= 0, ColumnIndex: >= 0 })
        {
            var dgv = sender as DataGridView;
            if (dgv.Columns[e.ColumnIndex].Name != nameof(OrdersResults.Process)) return;
            DataGridViewCell changedCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

            /*
            * 
            */
            if (changedCell.Value is bool processValue && _ordersBindingSource.Current is OrdersResults current)
            {
                current.Process = processValue;
            }
        }
    }

    private void OrdersResults_ListChanged(object? sender, ListChangedEventArgs e)
    {
        if (e.ListChangedType == ListChangedType.ItemChanged)
        {
            var changedItem = _ordersBindingList[e.OldIndex];
        }
        else if (e.ListChangedType == ListChangedType.ItemAdded)
        {
            // do something
        }
        else if (e.ListChangedType == ListChangedType.ItemDeleted)
        {
            // do something
        }
    }

    /// <summary>
    /// Handles the <see cref="DataGridView.CurrentCellDirtyStateChanged"/> event for the <see cref="dataGridView1"/> control.
    /// </summary>
    /// <remarks>
    /// This method ensures that when the current cell in the first column of the <see cref="dataGridView1"/> becomes dirty,
    /// the edit operation is committed immediately. This is particularly useful for handling changes in checkbox cells.
    /// </remarks>
    /// <seealso cref="DataGridView.CommitEdit(DataGridViewDataErrorContexts)"/>
    private void DataGridView_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
    {
        if (dataGridView1.CurrentCell!.ColumnIndex == 0)
        {
            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            var current = _ordersBindingSource.GetCurrentOrder();
        }
    }

    private void DataGridView_DataError(object? sender, DataGridViewDataErrorEventArgs e)
    {
        e.Cancel = false;
    }

    /// <summary>
    /// Handles the click event for the "Current Item" button in the binding navigator.
    /// </summary>
    /// <remarks>
    /// This method retrieves the currently selected order from the binding source and displays its 
    /// <see cref="OrdersResults.OrderID"/> and <see cref="OrdersResults.CompanyName"/> in an informational dialog.
    /// </remarks>
    /// <seealso cref="Dialogs.Information(Control, string, string)"/>
    private void CurrentItemButton_Click(object? sender, EventArgs e)
    {
        var current = _ordersBindingSource.GetCurrentOrder();
        Dialogs.Information(this, $"{current!.OrderID} {current.CompanyName} {current.Process}");
    }

    private void AboutItemButton_Click(object? sender, EventArgs e)
    {
        Dialogs.Information(this, "Shows creating a CSV file and reading the orders from the file.");
    }

    /// <summary>
    /// Imports orders from a CSV file, updates the data bindings, and displays any errors encountered during the import process.
    /// </summary>
    /// <remarks>
    /// This method utilizes the <see cref="Importer.Execute"/> method to parse a CSV file and retrieve valid orders 
    /// along with the line numbers of invalid entries. It then updates the data bindings for the UI components 
    /// to reflect the imported data.
    /// </remarks>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the CSV file specified in the <see cref="Importer.Execute"/> method does not exist.
    /// </exception>
    /// <seealso cref="Importer.Execute"/>
    private bool ImportOrders()
    {
        if (!FileAccessUtil.CanOpenTextFile(FileSettings.Instance.FileName))
        {
            return false;
        }

        var (validOrders, badLineNumbers) = Importer.Execute(FileSettings.Instance.FileName);

        var badLines = badLineNumbers.Count;

        if (badLines > 0)
        {
            Dialogs.Information(this, $"Found {badLines} bad lines in the CSV file.");
        }

        _ordersBindingList = new SortableBindingList<OrdersResults>(validOrders);
        _ordersBindingSource.DataSource = _ordersBindingList;

        BindingNavigator1.BindingSource = _ordersBindingSource;
        dataGridView1.DataSource = _ordersBindingSource;

        DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
        dataGridView1.FixHeaders();
        dataGridView1.ExpandColumns();


        return true;

    }

    /// <summary>
    /// Processes the selected orders from the data source, filters them based on the 
    /// <see cref="OrdersResults.Process"/> property, and logs their details to the debug output.
    /// </summary>
    private void ProcessButton_Click(object sender, EventArgs e)
    {
        List<Order> selected = _ordersBindingList
            .Where(o => o.Process)
            .Select(o => new Order(
                o.OrderID, 
                o.OrderDate, 
                o.RequiredDate, 
                o.ShippedDate,
                o.ShipAddress, 
                o.ShipCity, 
                o.ShipPostalCode, 
                o.ShipCountry, 
                o.CompanyName))
            .ToList();

        /*
         * 
         */
        foreach (var (id, ordered, requiredBy, shipped, _, _, _, _, company) in selected)
        {
            Debug.WriteLine($"{id} {ordered} {requiredBy} {shipped} {company}");
        }
  
    }

    private void ExitAppButton_Click(object sender, EventArgs e)
    {
        Close();
    }
}
