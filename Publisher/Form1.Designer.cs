
namespace Publisher
{
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ProjectListBox = new System.Windows.Forms.ListBox();
            this.VersionsListBox = new System.Windows.Forms.ListBox();
            this.PublishButton = new System.Windows.Forms.Button();
            this.OpenLocalFeedFolderButton = new System.Windows.Forms.Button();
            this.CopyCommandCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ProjectListBox
            // 
            this.ProjectListBox.FormattingEnabled = true;
            this.ProjectListBox.ItemHeight = 15;
            this.ProjectListBox.Location = new System.Drawing.Point(4, 8);
            this.ProjectListBox.Name = "ProjectListBox";
            this.ProjectListBox.Size = new System.Drawing.Size(260, 124);
            this.ProjectListBox.TabIndex = 0;
            // 
            // VersionsListBox
            // 
            this.VersionsListBox.FormattingEnabled = true;
            this.VersionsListBox.ItemHeight = 15;
            this.VersionsListBox.Location = new System.Drawing.Point(270, 8);
            this.VersionsListBox.Name = "VersionsListBox";
            this.VersionsListBox.Size = new System.Drawing.Size(123, 124);
            this.VersionsListBox.TabIndex = 1;
            // 
            // PublishButton
            // 
            this.PublishButton.Image = ((System.Drawing.Image)(resources.GetObject("PublishButton.Image")));
            this.PublishButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PublishButton.Location = new System.Drawing.Point(4, 138);
            this.PublishButton.Name = "PublishButton";
            this.PublishButton.Size = new System.Drawing.Size(389, 23);
            this.PublishButton.TabIndex = 2;
            this.PublishButton.Text = "Publish";
            this.PublishButton.UseVisualStyleBackColor = true;
            this.PublishButton.Click += new System.EventHandler(this.PublishButton_Click);
            // 
            // OpenLocalFeedFolderButton
            // 
            this.OpenLocalFeedFolderButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenLocalFeedFolderButton.Image")));
            this.OpenLocalFeedFolderButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OpenLocalFeedFolderButton.Location = new System.Drawing.Point(4, 189);
            this.OpenLocalFeedFolderButton.Name = "OpenLocalFeedFolderButton";
            this.OpenLocalFeedFolderButton.Size = new System.Drawing.Size(389, 23);
            this.OpenLocalFeedFolderButton.TabIndex = 3;
            this.OpenLocalFeedFolderButton.Text = "Open local feed folder";
            this.OpenLocalFeedFolderButton.UseVisualStyleBackColor = true;
            this.OpenLocalFeedFolderButton.Click += new System.EventHandler(this.OpenLocalFeedFolderButton_Click);
            // 
            // CopyCommandCheckBox
            // 
            this.CopyCommandCheckBox.AutoSize = true;
            this.CopyCommandCheckBox.Location = new System.Drawing.Point(4, 164);
            this.CopyCommandCheckBox.Name = "CopyCommandCheckBox";
            this.CopyCommandCheckBox.Size = new System.Drawing.Size(233, 19);
            this.CopyCommandCheckBox.TabIndex = 4;
            this.CopyCommandCheckBox.Text = "Copy command to Windows Clipboard";
            this.CopyCommandCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 224);
            this.Controls.Add(this.CopyCommandCheckBox);
            this.Controls.Add(this.OpenLocalFeedFolderButton);
            this.Controls.Add(this.PublishButton);
            this.Controls.Add(this.VersionsListBox);
            this.Controls.Add(this.ProjectListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Publisher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ProjectListBox;
        private System.Windows.Forms.ListBox VersionsListBox;
        private System.Windows.Forms.Button PublishButton;
        private System.Windows.Forms.Button OpenLocalFeedFolderButton;
        private System.Windows.Forms.CheckBox CopyCommandCheckBox;
    }
}

