
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
            ProjectListBox = new System.Windows.Forms.ListBox();
            VersionsListBox = new System.Windows.Forms.ListBox();
            PublishButton = new System.Windows.Forms.Button();
            OpenLocalFeedFolderButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // ProjectListBox
            // 
            ProjectListBox.FormattingEnabled = true;
            ProjectListBox.Location = new System.Drawing.Point(5, 11);
            ProjectListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            ProjectListBox.Name = "ProjectListBox";
            ProjectListBox.Size = new System.Drawing.Size(297, 184);
            ProjectListBox.TabIndex = 0;
            // 
            // VersionsListBox
            // 
            VersionsListBox.FormattingEnabled = true;
            VersionsListBox.Location = new System.Drawing.Point(309, 11);
            VersionsListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            VersionsListBox.Name = "VersionsListBox";
            VersionsListBox.Size = new System.Drawing.Size(140, 184);
            VersionsListBox.TabIndex = 1;
            // 
            // PublishButton
            // 
            PublishButton.Image = (System.Drawing.Image)resources.GetObject("PublishButton.Image");
            PublishButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            PublishButton.Location = new System.Drawing.Point(5, 210);
            PublishButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            PublishButton.Name = "PublishButton";
            PublishButton.Size = new System.Drawing.Size(445, 31);
            PublishButton.TabIndex = 2;
            PublishButton.Text = "Publish";
            PublishButton.UseVisualStyleBackColor = true;
            PublishButton.Click += PublishButton_Click;
            // 
            // OpenLocalFeedFolderButton
            // 
            OpenLocalFeedFolderButton.Image = (System.Drawing.Image)resources.GetObject("OpenLocalFeedFolderButton.Image");
            OpenLocalFeedFolderButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            OpenLocalFeedFolderButton.Location = new System.Drawing.Point(5, 252);
            OpenLocalFeedFolderButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            OpenLocalFeedFolderButton.Name = "OpenLocalFeedFolderButton";
            OpenLocalFeedFolderButton.Size = new System.Drawing.Size(445, 31);
            OpenLocalFeedFolderButton.TabIndex = 3;
            OpenLocalFeedFolderButton.Text = "Open local feed folder";
            OpenLocalFeedFolderButton.UseVisualStyleBackColor = true;
            OpenLocalFeedFolderButton.Click += OpenLocalFeedFolderButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(461, 299);
            Controls.Add(OpenLocalFeedFolderButton);
            Controls.Add(PublishButton);
            Controls.Add(VersionsListBox);
            Controls.Add(ProjectListBox);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Local feed publisher";
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ProjectListBox;
        private System.Windows.Forms.ListBox VersionsListBox;
        private System.Windows.Forms.Button PublishButton;
        private System.Windows.Forms.Button OpenLocalFeedFolderButton;
    }
}

