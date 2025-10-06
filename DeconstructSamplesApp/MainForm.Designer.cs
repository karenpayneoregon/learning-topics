namespace DeconstructSamplesApp;

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
        PeopleDictionaryButton = new Button();
        SuspendLayout();
        // 
        // PeopleDictionaryButton
        // 
        PeopleDictionaryButton.Location = new Point(109, 98);
        PeopleDictionaryButton.Name = "PeopleDictionaryButton";
        PeopleDictionaryButton.Size = new Size(271, 29);
        PeopleDictionaryButton.TabIndex = 0;
        PeopleDictionaryButton.Text = "People dictionary sample";
        PeopleDictionaryButton.UseVisualStyleBackColor = true;
        PeopleDictionaryButton.Click += PeopleDictionaryButton_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(488, 225);
        Controls.Add(PeopleDictionaryButton);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Deconstruction: dictionary";
        ResumeLayout(false);
    }

    #endregion

    private Button PeopleDictionaryButton;
}
