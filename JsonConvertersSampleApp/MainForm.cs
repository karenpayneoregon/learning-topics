
using JsonConvertersSampleApp.Classes;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using JsonConvertersSampleApp.Models;
using JsonConvertersSampleLibrary.Extensions;
using JsonConvertersSampleLibrary.Classes;
using static System.Net.Mime.MediaTypeNames;

namespace JsonConvertersSampleApp;

public partial class MainForm : Form
{
    BindingList<Taxpayer> _taxpayersBindingList;
    BindingSource BindingSource = new();
    public MainForm()
    {
        InitializeComponent();

        _taxpayersBindingList = new BindingList<Taxpayer>(BogusOperations.TaxpayerList());
        BindingSource.DataSource = _taxpayersBindingList;
        listBox1.DataSource = BindingSource;
        var json = JsonSerializer.Serialize(_taxpayersBindingList, Options);

        textBox1.Text = json;

        List<Taxpayer> taxpayers = JsonSerializer.Deserialize<List<Taxpayer>>(json);

        CreditCardExample.ForCustomers();

        //Debug.WriteLine("12345678901234".MaskCreditCard()); 
        //Debug.WriteLine("123456789012345".MaskCreditCard());
        //Debug.WriteLine("123456789012".MaskCreditCard());

        CreditCardExample.FromExternal();
    }

    private static JsonSerializerOptions Options =>
        new()
        {
            WriteIndented = true
        };

    private void DeserializeButton_Click(object sender, EventArgs e)
    {
        using ChildForm f = new(_taxpayersBindingList.ToList());
        f.ShowDialog();
    }
}
