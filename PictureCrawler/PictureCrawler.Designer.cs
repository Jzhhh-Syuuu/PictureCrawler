namespace PictureCrawler
{
    partial class PictureCrawler
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Crawl = new System.Windows.Forms.Button();
            this.URL = new System.Windows.Forms.TextBox();
            this.outputText = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SavePath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SavePathLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Crawl
            // 
            this.Crawl.Location = new System.Drawing.Point(77, 78);
            this.Crawl.Name = "Crawl";
            this.Crawl.Size = new System.Drawing.Size(75, 23);
            this.Crawl.TabIndex = 0;
            this.Crawl.Text = "Crawl";
            this.Crawl.UseVisualStyleBackColor = true;
            this.Crawl.Click += new System.EventHandler(this.Crawl_Click);
            // 
            // URL
            // 
            this.URL.Location = new System.Drawing.Point(77, 51);
            this.URL.Name = "URL";
            this.URL.Size = new System.Drawing.Size(431, 21);
            this.URL.TabIndex = 1;
            // 
            // outputText
            // 
            this.outputText.Location = new System.Drawing.Point(29, 117);
            this.outputText.Multiline = true;
            this.outputText.Name = "outputText";
            this.outputText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputText.Size = new System.Drawing.Size(479, 283);
            this.outputText.TabIndex = 2;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // SavePath
            // 
            this.SavePath.Location = new System.Drawing.Point(77, 12);
            this.SavePath.Name = "SavePath";
            this.SavePath.Size = new System.Drawing.Size(431, 21);
            this.SavePath.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(470, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "···";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OpenFilePath_Click);
            // 
            // SavePathLabel
            // 
            this.SavePathLabel.AutoSize = true;
            this.SavePathLabel.Location = new System.Drawing.Point(9, 15);
            this.SavePathLabel.Name = "SavePathLabel";
            this.SavePathLabel.Size = new System.Drawing.Size(53, 12);
            this.SavePathLabel.TabIndex = 5;
            this.SavePathLabel.Text = "保存位置";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "目标地址";
            // 
            // PictureCrawler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 449);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SavePathLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SavePath);
            this.Controls.Add(this.outputText);
            this.Controls.Add(this.URL);
            this.Controls.Add(this.Crawl);
            this.Name = "PictureCrawler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PictureCrawler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Crawl;
        private System.Windows.Forms.TextBox URL;
        private System.Windows.Forms.TextBox outputText;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox SavePath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label SavePathLabel;
        private System.Windows.Forms.Label label1;
    }
}

