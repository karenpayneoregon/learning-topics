using JsonConvertersSampleApp.Models;

namespace JsonConvertersSampleApp;
public partial class ChildForm : Form
{
    public ChildForm()
    {
        InitializeComponent();

    }
    public ChildForm(List<Taxpayer> taxpayers)
    {
        InitializeComponent();
        dataGridView1.AutoGenerateColumns = false;
        dataGridView1.DataSource = taxpayers;

    }
}
