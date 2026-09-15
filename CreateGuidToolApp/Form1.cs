using System.Diagnostics;
using System.Text;
using UUIDNext;

namespace CreateGuidToolApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private  void CreateButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = @"Please wait...";
            textBox1.Refresh();

            Thread.Sleep(500);

            StringBuilder sb = new();

            for (int index = 0; index < 10; index++)
            {
                Guid sequentialUuid = Uuid.NewDatabaseFriendly(Database.SqlServer);
                sb.AppendLine(sequentialUuid.ToString());
                Thread.Sleep(1000);
            }

            textBox1.Text = sb.ToString();
            textBox1.Refresh();
        }
    }
}
