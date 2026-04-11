using ProcessOrdersApp.Controls;

namespace ProcessOrdersApp;

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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        BindingNavigator1 = new CoreBindingNavigator();
        panel1 = new Panel();
        ExitAppButton = new Button();
        dataGridView1 = new DataGridView();
        ProcessButtom = new Button();
        (BindingNavigator1).BeginInit();
        panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // BindingNavigator1
        // 
        BindingNavigator1.ImageScalingSize = new Size(20, 20);
        BindingNavigator1.Location = new Point(0, 0);
        BindingNavigator1.Name = "BindingNavigator1";
        BindingNavigator1.Size = new Size(1717, 27);
        BindingNavigator1.TabIndex = 0;
        BindingNavigator1.Text = "coreBindingNavigator1";
        // 
        // panel1
        // 
        panel1.Controls.Add(ProcessButtom);
        panel1.Controls.Add(ExitAppButton);
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point(0, 396);
        panel1.Name = "panel1";
        panel1.Size = new Size(1717, 54);
        panel1.TabIndex = 1;
        // 
        // ExitAppButton
        // 
        ExitAppButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        ExitAppButton.Location = new Point(1611, 13);
        ExitAppButton.Name = "ExitAppButton";
        ExitAppButton.Size = new Size(94, 29);
        ExitAppButton.TabIndex = 0;
        ExitAppButton.Text = "Exit";
        ExitAppButton.UseVisualStyleBackColor = true;
        ExitAppButton.Click += ExitAppButton_Click;
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Dock = DockStyle.Fill;
        dataGridView1.Location = new Point(0, 27);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.Size = new Size(1717, 369);
        dataGridView1.TabIndex = 2;
        // 
        // ProcessButtom
        // 
        ProcessButtom.Location = new Point(12, 13);
        ProcessButtom.Name = "ProcessButtom";
        ProcessButtom.Size = new Size(94, 29);
        ProcessButtom.TabIndex = 3;
        ProcessButtom.Text = "Process";
        ProcessButtom.UseVisualStyleBackColor = true;
        ProcessButtom.Click += ProcessButton_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1717, 450);
        Controls.Add(dataGridView1);
        Controls.Add(panel1);
        Controls.Add(BindingNavigator1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Working with files and databases";
        (BindingNavigator1).EndInit();
        panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Controls.CoreBindingNavigator BindingNavigator1;
    private Panel panel1;
    private DataGridView dataGridView1;
    private Button ExitAppButton;
    private Button ProcessButtom;
}
