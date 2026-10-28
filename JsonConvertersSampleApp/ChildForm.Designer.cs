namespace JsonConvertersSampleApp;

partial class ChildForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        dataGridView1 = new DataGridView();
        FirstNameColumn = new DataGridViewTextBoxColumn();
        LastNameColumn = new DataGridViewTextBoxColumn();
        GenderColumn = new DataGridViewTextBoxColumn();
        SocialColumn = new DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Columns.AddRange(new DataGridViewColumn[] { FirstNameColumn, LastNameColumn, GenderColumn, SocialColumn });
        dataGridView1.Location = new Point(25, 38);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.Size = new Size(734, 188);
        dataGridView1.TabIndex = 0;
        // 
        // FirstNameColumn
        // 
        FirstNameColumn.DataPropertyName = "FirstName";
        FirstNameColumn.HeaderText = "First";
        FirstNameColumn.MinimumWidth = 6;
        FirstNameColumn.Name = "FirstNameColumn";
        FirstNameColumn.Width = 125;
        // 
        // LastNameColumn
        // 
        LastNameColumn.DataPropertyName = "LastName";
        LastNameColumn.HeaderText = "Last";
        LastNameColumn.MinimumWidth = 6;
        LastNameColumn.Name = "LastNameColumn";
        LastNameColumn.Width = 125;
        // 
        // GenderColumn
        // 
        GenderColumn.DataPropertyName = "Gender";
        GenderColumn.HeaderText = "Gender";
        GenderColumn.MinimumWidth = 6;
        GenderColumn.Name = "GenderColumn";
        GenderColumn.Width = 125;
        // 
        // SocialColumn
        // 
        SocialColumn.DataPropertyName = "SSN";
        SocialColumn.HeaderText = "SSN";
        SocialColumn.MinimumWidth = 6;
        SocialColumn.Name = "SocialColumn";
        SocialColumn.Width = 125;
        // 
        // ChildForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 282);
        Controls.Add(dataGridView1);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "ChildForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Deserialize taxpayers";
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DataGridView dataGridView1;
    private DataGridViewTextBoxColumn FirstNameColumn;
    private DataGridViewTextBoxColumn LastNameColumn;
    private DataGridViewTextBoxColumn GenderColumn;
    private DataGridViewTextBoxColumn SocialColumn;
}