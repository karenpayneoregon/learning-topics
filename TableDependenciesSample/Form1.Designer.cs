namespace TableDependenciesSample;

partial class Form1
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
        AboutButton = new ToolStripButton();
        LoadTreeViewButton = new Button();
        DependencyTreeView = new TreeView();
        button2 = new Button();
        SuspendLayout();
        // 
        // AboutButton
        // 
        AboutButton.Name = "AboutButton";
        AboutButton.Size = new Size(23, 23);
        // 
        // LoadTreeViewButton
        // 
        LoadTreeViewButton.Location = new Point(12, 12);
        LoadTreeViewButton.Name = "LoadTreeViewButton";
        LoadTreeViewButton.Size = new Size(227, 29);
        LoadTreeViewButton.TabIndex = 0;
        LoadTreeViewButton.Text = "Load databases";
        LoadTreeViewButton.UseVisualStyleBackColor = true;
        LoadTreeViewButton.Click += LoadTreeViewButton_Click;
        // 
        // DependencyTreeView
        // 
        DependencyTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        DependencyTreeView.Location = new Point(306, 0);
        DependencyTreeView.Name = "DependencyTreeView";
        DependencyTreeView.Size = new Size(332, 544);
        DependencyTreeView.TabIndex = 2;
        // 
        // button2
        // 
        button2.Location = new Point(12, 47);
        button2.Name = "button2";
        button2.Size = new Size(227, 29);
        button2.TabIndex = 3;
        button2.Text = "Clear";
        button2.UseVisualStyleBackColor = true;
        button2.Click += ClearTreeView_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(638, 544);
        Controls.Add(button2);
        Controls.Add(DependencyTreeView);
        Controls.Add(LoadTreeViewButton);
        MinimumSize = new Size(418, 0);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Code sample";
        ResumeLayout(false);
    }

    #endregion
    private ToolStripButton AboutButton;
    private Button LoadTreeViewButton;
    private TreeView DependencyTreeView;
    private Button button2;
}
