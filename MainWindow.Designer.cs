namespace TCPクライアント
{
    partial class MainWindow
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxAddr = new System.Windows.Forms.TextBox();
            this.labelAddr = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.richTextDisplay = new System.Windows.Forms.RichTextBox();
            this.textFlame = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonHalt = new System.Windows.Forms.Button();
            this.buttonContiniousCap = new System.Windows.Forms.Button();
            this.buttonSingleCap = new System.Windows.Forms.Button();
            this.buttonReceive = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonDebug = new System.Windows.Forms.Button();
            this.buttonReceiveCell = new System.Windows.Forms.Button();
            this.buttonReceiveProjected = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_FrameRate = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxAddr
            // 
            this.textBoxAddr.Location = new System.Drawing.Point(12, 38);
            this.textBoxAddr.Name = "textBoxAddr";
            this.textBoxAddr.Size = new System.Drawing.Size(299, 19);
            this.textBoxAddr.TabIndex = 0;
            this.textBoxAddr.Text = "172.20.2.55";
            this.textBoxAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelAddr
            // 
            this.labelAddr.AutoSize = true;
            this.labelAddr.Location = new System.Drawing.Point(21, 23);
            this.labelAddr.Name = "labelAddr";
            this.labelAddr.Size = new System.Drawing.Size(91, 12);
            this.labelAddr.TabIndex = 1;
            this.labelAddr.Text = "FPGAのIPアドレス";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(21, 74);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(57, 12);
            this.labelPort.TabIndex = 2;
            this.labelPort.Text = "ポート番号";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(12, 89);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(299, 19);
            this.textBoxPort.TabIndex = 3;
            this.textBoxPort.Text = "7000";
            this.textBoxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(12, 125);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(299, 23);
            this.buttonConnect.TabIndex = 6;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // richTextDisplay
            // 
            this.richTextDisplay.Location = new System.Drawing.Point(12, 154);
            this.richTextDisplay.Name = "richTextDisplay";
            this.richTextDisplay.ReadOnly = true;
            this.richTextDisplay.Size = new System.Drawing.Size(299, 61);
            this.richTextDisplay.TabIndex = 7;
            this.richTextDisplay.Text = "";
            // 
            // textFlame
            // 
            this.textFlame.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.textFlame.Location = new System.Drawing.Point(354, 38);
            this.textFlame.Name = "textFlame";
            this.textFlame.Size = new System.Drawing.Size(59, 19);
            this.textFlame.TabIndex = 18;
            this.textFlame.Text = "1";
            this.textFlame.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label1.Location = new System.Drawing.Point(419, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "フレーム目";
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(466, 225);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(119, 23);
            this.buttonExport.TabIndex = 23;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonHalt
            // 
            this.buttonHalt.Location = new System.Drawing.Point(341, 225);
            this.buttonHalt.Name = "buttonHalt";
            this.buttonHalt.Size = new System.Drawing.Size(119, 23);
            this.buttonHalt.TabIndex = 22;
            this.buttonHalt.Text = "Halt";
            this.buttonHalt.UseVisualStyleBackColor = true;
            this.buttonHalt.Click += new System.EventHandler(this.buttonHalt_Click);
            // 
            // buttonContiniousCap
            // 
            this.buttonContiniousCap.Location = new System.Drawing.Point(341, 125);
            this.buttonContiniousCap.Name = "buttonContiniousCap";
            this.buttonContiniousCap.Size = new System.Drawing.Size(244, 23);
            this.buttonContiniousCap.TabIndex = 21;
            this.buttonContiniousCap.Text = "Continous Capture";
            this.buttonContiniousCap.UseVisualStyleBackColor = true;
            this.buttonContiniousCap.Click += new System.EventHandler(this.buttonContiniousCap_Click);
            // 
            // buttonSingleCap
            // 
            this.buttonSingleCap.Location = new System.Drawing.Point(466, 89);
            this.buttonSingleCap.Name = "buttonSingleCap";
            this.buttonSingleCap.Size = new System.Drawing.Size(119, 23);
            this.buttonSingleCap.TabIndex = 20;
            this.buttonSingleCap.Text = "Single Capture";
            this.buttonSingleCap.UseVisualStyleBackColor = true;
            this.buttonSingleCap.Click += new System.EventHandler(this.buttonSingleCap_Click);
            // 
            // buttonReceive
            // 
            this.buttonReceive.Location = new System.Drawing.Point(341, 89);
            this.buttonReceive.Name = "buttonReceive";
            this.buttonReceive.Size = new System.Drawing.Size(119, 23);
            this.buttonReceive.TabIndex = 19;
            this.buttonReceive.Text = "Read Only";
            this.buttonReceive.UseVisualStyleBackColor = true;
            this.buttonReceive.Click += new System.EventHandler(this.buttonReceive_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(421, 264);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(84, 23);
            this.buttonReset.TabIndex = 24;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonDebug
            // 
            this.buttonDebug.Location = new System.Drawing.Point(510, 34);
            this.buttonDebug.Name = "buttonDebug";
            this.buttonDebug.Size = new System.Drawing.Size(75, 23);
            this.buttonDebug.TabIndex = 26;
            this.buttonDebug.Text = "Debug";
            this.buttonDebug.UseVisualStyleBackColor = true;
            this.buttonDebug.Click += new System.EventHandler(this.buttonDebug_Click);
            // 
            // buttonReceiveCell
            // 
            this.buttonReceiveCell.Location = new System.Drawing.Point(341, 170);
            this.buttonReceiveCell.Name = "buttonReceiveCell";
            this.buttonReceiveCell.Size = new System.Drawing.Size(119, 40);
            this.buttonReceiveCell.TabIndex = 27;
            this.buttonReceiveCell.Text = "Receive Cell Response";
            this.buttonReceiveCell.UseVisualStyleBackColor = true;
            this.buttonReceiveCell.Click += new System.EventHandler(this.buttonReceiveCell_Click);
            // 
            // buttonReceiveProjected
            // 
            this.buttonReceiveProjected.Location = new System.Drawing.Point(466, 170);
            this.buttonReceiveProjected.Name = "buttonReceiveProjected";
            this.buttonReceiveProjected.Size = new System.Drawing.Size(119, 40);
            this.buttonReceiveProjected.TabIndex = 28;
            this.buttonReceiveProjected.Text = "Receive Projected Data";
            this.buttonReceiveProjected.UseVisualStyleBackColor = true;
            this.buttonReceiveProjected.Click += new System.EventHandler(this.buttonReceiveProjected_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 16F);
            this.label2.Location = new System.Drawing.Point(164, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 22);
            this.label2.TabIndex = 29;
            this.label2.Text = "FPS";
            // 
            // textBox_FrameRate
            // 
            this.textBox_FrameRate.Font = new System.Drawing.Font("MS UI Gothic", 16F);
            this.textBox_FrameRate.Location = new System.Drawing.Point(104, 242);
            this.textBox_FrameRate.Name = "textBox_FrameRate";
            this.textBox_FrameRate.Size = new System.Drawing.Size(54, 29);
            this.textBox_FrameRate.TabIndex = 30;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(601, 297);
            this.Controls.Add(this.textBox_FrameRate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonReceiveProjected);
            this.Controls.Add(this.buttonReceiveCell);
            this.Controls.Add(this.buttonDebug);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonHalt);
            this.Controls.Add(this.buttonContiniousCap);
            this.Controls.Add(this.buttonSingleCap);
            this.Controls.Add(this.buttonReceive);
            this.Controls.Add(this.textFlame);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextDisplay);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.labelAddr);
            this.Controls.Add(this.textBoxAddr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "TCP_Client";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAddr;
        private System.Windows.Forms.Label labelAddr;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.RichTextBox richTextDisplay;
        private System.Windows.Forms.TextBox textFlame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonHalt;
        private System.Windows.Forms.Button buttonContiniousCap;
        private System.Windows.Forms.Button buttonSingleCap;
        private System.Windows.Forms.Button buttonReceive;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonDebug;
        private System.Windows.Forms.Button buttonReceiveCell;
        private System.Windows.Forms.Button buttonReceiveProjected;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_FrameRate;
    }
}

