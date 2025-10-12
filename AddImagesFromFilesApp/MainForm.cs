using AddImagesFromFilesApp.Classes;

namespace AddImagesFromFilesApp;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        Size = new Size(818, 1015);
    }
    private async void ExecuteButton_Click(object sender, EventArgs e)
    {
        try
        {
            var list = CategoryHelper.LoadCategoriesFromImages("Images");
            var repository = new CategoryRepository();
            repository.ResetTable();
            var (_, success) = await repository.InsertManyAsync(list);
            if (success)
            {
                var table = await repository.GetAllDataTableAsync();
                Dialogs.Information(this, "Information", "Data loaded successfully.");
                dataGridView1.DataSource = table;
                dataGridView1.ExpandColumns();
            }
            else
            {
                Dialogs.Information(this, "Error", "See log for details.");
            }
        }
        catch (Exception exception)
        {
            Dialogs.Information(this, "Error", $"{exception.Message}");
        }
    }
}
