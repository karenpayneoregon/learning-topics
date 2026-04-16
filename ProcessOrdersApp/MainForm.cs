using System.ComponentModel;
using ProcessOrdersApp.Classes;
using ProcessOrdersApp.Classes.Configuration;
using ProcessOrdersApp.Classes.Extensions;
using ProcessOrdersApp.Components;
using ProcessOrdersApp.Models;
using Serilog;

namespace ProcessOrdersApp;

public partial class MainForm : Form
{
    private SortableBindingList<OrdersResults> _ordersBindingList = null!;
    private readonly BindingSource _ordersBindingSource = new();

    public MainForm()
    {
        InitializeComponent();
        Shown += MainForm_Shown;
    }

    private void MainForm_Shown(object? sender, EventArgs e)
    {

        dataGridView1.AllowUserToAddRows = false;

        // Set the standard row color (even rows)
        dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
        // Set the alternating row color (odd rows)
        dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;


        BindingNavigator1.AboutItemButton.Click += AboutItemButton_Click;
        BindingNavigator1.CurrentItemButton.Click += CurrentItemButton_Click;


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
            Log.Warning("Cannot open file: {FileName}", FileSettings.Instance.FileName);
            return false;
        }

        var (validOrders, badLineNumbers) = Importer.Execute(FileSettings.Instance.FileName);

        if (validOrders.Count == 0)
        {
            DisableButtons();
        }
        
        var badLines = badLineNumbers.Count;

        if (badLines > 0)
        {
            // logged in the Importer.Execute method
            Dialogs.Information(this, $"Found {badLines} bad lines in the CSV file.");
        }

        _ordersBindingList = new SortableBindingList<OrdersResults>(validOrders);
        _ordersBindingSource.DataSource = _ordersBindingList;

        BindingNavigator1.BindingSource = _ordersBindingSource;
        dataGridView1.DataSource = _ordersBindingSource;

        dataGridView1.FixHeaders();
        dataGridView1.ExpandColumns();

        return true;

    }

    private void DataGridView1_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (e is { RowIndex: >= 0, ColumnIndex: >= 0 })
        {
            var dgv = sender as DataGridView;
            if (dgv.Columns[e.ColumnIndex].Name != nameof(OrdersResults.Process)) return;
            DataGridViewCell changedCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

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

    /// <summary>
    /// Handles the <see cref="DataGridView.DataError"/> event for the <see cref="dataGridView1"/> control.
    /// </summary>
    /// <remarks>
    /// This method is invoked when an error occurs during data operations in the <see cref="dataGridView1"/> control.
    /// It ensures that the error is handled gracefully by setting the <see cref="DataGridViewDataErrorEventArgs.Cancel"/> 
    /// property to <c>false</c>, allowing the application to continue running without interruption.
    /// </remarks>
    private void DataGridView_DataError(object? sender, DataGridViewDataErrorEventArgs e)
    {
        e.Cancel = false;
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


        if (DataOperations.ProcessOrders(selected))
        {
            if (OrdersCsvExporter.RemoveRowsByOrderId(FileSettings.Instance.FileName, selected.Select(o => o.OrderID).ToList()))
            {
                
                foreach (var existingOrder in selected
                             .Select(order => _ordersBindingList.FirstOrDefault(o => o.OrderID == order.OrderID)))
                    _ordersBindingList.Remove(existingOrder);

                if (_ordersBindingList.Count == 0)
                {
                    DisableButtons();
                }
            }
        }

    }

    /// <summary>
    /// Disables all buttons within the current form, including BindingNavigator1.CurrentItemButton.
    /// </summary>
    /// <remarks>
    /// This method iterates through all button controls in the form and disables them. 
    /// Additionally, it disables the <see cref="Controls.CoreBindingNavigator.CurrentItemButton"/> 
    /// in the associated binding navigator.
    /// </remarks>
    private void DisableButtons()
    {
        foreach (var button in this.ButtonList())
        {
            button.Enabled = false;
            BindingNavigator1.CurrentItemButton.Enabled = false;
        }
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

    private void ExitAppButton_Click(object sender, EventArgs e)
    {
        Close();
    }
}
