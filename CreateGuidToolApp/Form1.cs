using System.Diagnostics;
using System.Text;
using UUIDNext;
using static CreateGuidToolApp.Classes.Dialogs;

namespace CreateGuidToolApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                CreateButton.Enabled = false;

                try
                {
                    textBox1.Text = @"Please wait...";

                    await Task.Delay(500);

                    StringBuilder sb = new();

                    for (int index = 0; index < 10; index++)
                    {
                        Guid sequentialUuid = Uuid.NewDatabaseFriendly(Database.SqlServer);
                        sb.AppendLine(sequentialUuid.ToString());

                        await Task.Delay(1000);
                    }

                    textBox1.Text = sb.ToString();
                }
                finally
                {
                    CreateButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                DisplayError(this, ex);
            }
        }
    }
}
