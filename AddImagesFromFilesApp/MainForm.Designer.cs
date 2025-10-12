namespace AddImagesFromFilesApp;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        ExecuteButton = new Button();
        dataGridView1 = new DataGridView();
        panel1 = new Panel();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // ExecuteButton
        // 
        ExecuteButton.Image = Properties.Resources.Execute_16x;
        ExecuteButton.ImageAlign = ContentAlignment.MiddleLeft;
        ExecuteButton.Location = new Point(24, 12);
        ExecuteButton.Name = "ExecuteButton";
        ExecuteButton.Size = new Size(151, 29);
        ExecuteButton.TabIndex = 0;
        ExecuteButton.Text = "Import";
        ExecuteButton.UseVisualStyleBackColor = true;
        ExecuteButton.Click += ExecuteButton_Click;
        // 
        // dataGridView1
        // 
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.TopLeft;
        dataGridViewCellStyle3.BackColor = SystemColors.Window;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
        dataGridView1.Dock = DockStyle.Fill;
        dataGridView1.Location = new Point(0, 55);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.ReadOnly = true;
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.RowTemplate.Height = 100;
        dataGridView1.Size = new Size(800, 416);
        dataGridView1.TabIndex = 1;
        // 
        // panel1
        // 
        panel1.Controls.Add(ExecuteButton);
        panel1.Dock = DockStyle.Top;
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(800, 55);
        panel1.TabIndex = 2;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 471);
        Controls.Add(dataGridView1);
        Controls.Add(panel1);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Code Sample";
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        panel1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Button ExecuteButton;
    private DataGridView dataGridView1;
    private Panel panel1;
}
