namespace TextEnrichment
{
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
            this.enrichBtn = new System.Windows.Forms.Button();
            this.selectFileBtn = new System.Windows.Forms.Button();
            this.filePathBeforeTextBox = new System.Windows.Forms.TextBox();
            this.filePathAfterTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // enrichBtn
            // 
            this.enrichBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.enrichBtn.Location = new System.Drawing.Point(430, 70);
            this.enrichBtn.Name = "enrichBtn";
            this.enrichBtn.Size = new System.Drawing.Size(75, 23);
            this.enrichBtn.TabIndex = 0;
            this.enrichBtn.Text = "Enrich";
            this.enrichBtn.UseVisualStyleBackColor = true;
            this.enrichBtn.Click += new System.EventHandler(this.enrichBtn_Click);
            // 
            // selectFileBtn
            // 
            this.selectFileBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectFileBtn.Location = new System.Drawing.Point(397, 6);
            this.selectFileBtn.Name = "selectFileBtn";
            this.selectFileBtn.Size = new System.Drawing.Size(75, 23);
            this.selectFileBtn.TabIndex = 1;
            this.selectFileBtn.Text = "Select File";
            this.selectFileBtn.UseVisualStyleBackColor = true;
            this.selectFileBtn.Click += new System.EventHandler(this.selectFileBtn_Click);
            // 
            // filePathBeforeTextBox
            // 
            this.filePathBeforeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filePathBeforeTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.filePathBeforeTextBox.Location = new System.Drawing.Point(93, 6);
            this.filePathBeforeTextBox.Name = "filePathBeforeTextBox";
            this.filePathBeforeTextBox.Size = new System.Drawing.Size(298, 23);
            this.filePathBeforeTextBox.TabIndex = 4;
            // 
            // filePathAfterTextBox
            // 
            this.filePathAfterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filePathAfterTextBox.Location = new System.Drawing.Point(93, 35);
            this.filePathAfterTextBox.Name = "filePathAfterTextBox";
            this.filePathAfterTextBox.Size = new System.Drawing.Size(298, 23);
            this.filePathAfterTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "File to Enrich";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Enriched File";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 105);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filePathAfterTextBox);
            this.Controls.Add(this.selectFileBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filePathBeforeTextBox);
            this.Controls.Add(this.enrichBtn);
            this.Name = "MainForm";
            this.Text = "TextEnricher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enrichBtn;
        private System.Windows.Forms.Button selectFileBtn;
        private System.Windows.Forms.TextBox filePathBeforeTextBox;
        private System.Windows.Forms.TextBox filePathAfterTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

