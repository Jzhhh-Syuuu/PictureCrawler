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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PictureCrawler));
            this.Crawl = new System.Windows.Forms.Button();
            this.URL = new System.Windows.Forms.TextBox();
            this.outputText = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SavePath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SavePathLabel = new System.Windows.Forms.Label();
            this.URLLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TotalLabel = new System.Windows.Forms.Label();
            this.CloseImg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CloseImg)).BeginInit();
            this.SuspendLayout();
            // 
            // Crawl
            // 
            this.Crawl.BackColor = System.Drawing.Color.Transparent;
            this.Crawl.Location = new System.Drawing.Point(325, 161);
            this.Crawl.Name = "Crawl";
            this.Crawl.Size = new System.Drawing.Size(75, 23);
            this.Crawl.TabIndex = 0;
            this.Crawl.Text = "Crawl";
            this.Crawl.UseVisualStyleBackColor = false;
            this.Crawl.Click += new System.EventHandler(this.Crawl_Click);
            // 
            // URL
            // 
            this.URL.BackColor = System.Drawing.Color.AliceBlue;
            this.URL.Location = new System.Drawing.Point(325, 134);
            this.URL.Name = "URL";
            this.URL.Size = new System.Drawing.Size(431, 21);
            this.URL.TabIndex = 1;
            // 
            // outputText
            // 
            this.outputText.BackColor = System.Drawing.Color.AliceBlue;
            this.outputText.Location = new System.Drawing.Point(382, 208);
            this.outputText.Multiline = true;
            this.outputText.Name = "outputText";
            this.outputText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputText.Size = new System.Drawing.Size(573, 364);
            this.outputText.TabIndex = 2;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // SavePath
            // 
            this.SavePath.BackColor = System.Drawing.Color.AliceBlue;
            this.SavePath.Location = new System.Drawing.Point(325, 95);
            this.SavePath.Name = "SavePath";
            this.SavePath.ReadOnly = true;
            this.SavePath.Size = new System.Drawing.Size(431, 21);
            this.SavePath.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Font = new System.Drawing.Font("宋体", 3.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(718, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "●●●";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.OpenFilePath_Click);
            // 
            // SavePathLabel
            // 
            this.SavePathLabel.AutoSize = true;
            this.SavePathLabel.BackColor = System.Drawing.Color.Transparent;
            this.SavePathLabel.Location = new System.Drawing.Point(257, 98);
            this.SavePathLabel.Name = "SavePathLabel";
            this.SavePathLabel.Size = new System.Drawing.Size(53, 12);
            this.SavePathLabel.TabIndex = 5;
            this.SavePathLabel.Text = "保存位置";
            // 
            // URLLabel
            // 
            this.URLLabel.AutoSize = true;
            this.URLLabel.BackColor = System.Drawing.Color.Transparent;
            this.URLLabel.Location = new System.Drawing.Point(257, 137);
            this.URLLabel.Name = "URLLabel";
            this.URLLabel.Size = new System.Drawing.Size(53, 12);
            this.URLLabel.TabIndex = 6;
            this.URLLabel.Text = "目标地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(441, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 7;
            // 
            // TotalLabel
            // 
            this.TotalLabel.AutoSize = true;
            this.TotalLabel.BackColor = System.Drawing.Color.Transparent;
            this.TotalLabel.Location = new System.Drawing.Point(435, 166);
            this.TotalLabel.Name = "TotalLabel";
            this.TotalLabel.Size = new System.Drawing.Size(203, 12);
            this.TotalLabel.TabIndex = 8;
            this.TotalLabel.Text = "已下载数/总下载数（成功：失败：）";
            // 
            // CloseImg
            // 
            this.CloseImg.BackColor = System.Drawing.Color.Transparent;
            this.CloseImg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseImg.BackgroundImage")));
            this.CloseImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseImg.Location = new System.Drawing.Point(937, 103);
            this.CloseImg.Name = "CloseImg";
            this.CloseImg.Size = new System.Drawing.Size(33, 31);
            this.CloseImg.TabIndex = 10;
            this.CloseImg.TabStop = false;
            this.CloseImg.Click += new System.EventHandler(this.CloseImg_Click);
            this.CloseImg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CloseImg_MouseDown);
            this.CloseImg.MouseLeave += new System.EventHandler(this.CloseImg_MouseLeave);
            this.CloseImg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CloseImg_MouseUp);
            // 
            // PictureCrawler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1066, 655);
            this.Controls.Add(this.CloseImg);
            this.Controls.Add(this.outputText);
            this.Controls.Add(this.TotalLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.URLLabel);
            this.Controls.Add(this.SavePathLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SavePath);
            this.Controls.Add(this.URL);
            this.Controls.Add(this.Crawl);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PictureCrawler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PictureCrawler";
            this.Load += new System.EventHandler(this.PictureCrawler_Load);
            this.ResizeBegin += new System.EventHandler(this.PictureCrawler_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.PictureCrawler_ResizeEnd);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureCrawler_MouseDown);
            this.Move += new System.EventHandler(this.PictureCrawler_Move);
            this.Resize += new System.EventHandler(this.PictureCrawler_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.CloseImg)).EndInit();
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
        private System.Windows.Forms.Label URLLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TotalLabel;
        private System.Windows.Forms.PictureBox CloseImg;
    }
}

