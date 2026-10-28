namespace JsonConvertersSampleApp;

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
        listBox1 = new ListBox();
        textBox1 = new TextBox();
        DeserializeButton = new Button();
        SuspendLayout();
        // 
        // listBox1
        // 
        listBox1.FormattingEnabled = true;
        listBox1.Location = new Point(12, 12);
        listBox1.Name = "listBox1";
        listBox1.Size = new Size(150, 124);
        listBox1.TabIndex = 0;
        // 
        // textBox1
        // 
        textBox1.Location = new Point(168, 12);
        textBox1.Multiline = true;
        textBox1.Name = "textBox1";
        textBox1.ScrollBars = ScrollBars.Vertical;
        textBox1.Size = new Size(620, 426);
        textBox1.TabIndex = 1;
        // 
        // DeserializeButton
        // 
        DeserializeButton.Location = new Point(12, 142);
        DeserializeButton.Name = "DeserializeButton";
        DeserializeButton.Size = new Size(150, 29);
        DeserializeButton.TabIndex = 2;
        DeserializeButton.Text = "Deserialize";
        DeserializeButton.UseVisualStyleBackColor = true;
        DeserializeButton.Click += DeserializeButton_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(DeserializeButton);
        Controls.Add(textBox1);
        Controls.Add(listBox1);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Mask SSN for json";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private ListBox listBox1;
    private TextBox textBox1;
    private Button DeserializeButton;
}
