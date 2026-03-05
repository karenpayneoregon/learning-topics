namespace ExportedVisualStudioExtensionsApp;

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
        CoreNavigator = new ExportedVisualStudioExtensionsApp.Controls.CoreBindingNavigator();
        ExtensionNameTextBox = new TextBox();
        LinkLabel1 = new LinkLabel();
        (CoreNavigator).BeginInit();
        SuspendLayout();
        // 
        // CoreNavigator
        // 
        CoreNavigator.ImageScalingSize = new Size(20, 20);
        CoreNavigator.Location = new Point(0, 0);
        CoreNavigator.Name = "CoreNavigator";
        CoreNavigator.Size = new Size(849, 27);
        CoreNavigator.TabIndex = 0;
        CoreNavigator.Text = "coreBindingNavigator1";
        // 
        // ExtensionNameTextBox
        // 
        ExtensionNameTextBox.Location = new Point(12, 97);
        ExtensionNameTextBox.Name = "ExtensionNameTextBox";
        ExtensionNameTextBox.Size = new Size(554, 27);
        ExtensionNameTextBox.TabIndex = 1;
        // 
        // LinkLabel1
        // 
        LinkLabel1.AutoSize = true;
        LinkLabel1.Location = new Point(12, 140);
        LinkLabel1.Name = "LinkLabel1";
        LinkLabel1.Size = new Size(126, 20);
        LinkLabel1.TabIndex = 2;
        LinkLabel1.TabStop = true;
        LinkLabel1.Text = "more information";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(849, 243);
        Controls.Add(LinkLabel1);
        Controls.Add(ExtensionNameTextBox);
        Controls.Add(CoreNavigator);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Visual Studio extensions";
        (CoreNavigator).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Controls.CoreBindingNavigator CoreNavigator;
    private TextBox ExtensionNameTextBox;
    private LinkLabel LinkLabel1;
}
