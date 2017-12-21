namespace TCPクライアント
{
    partial class LGN_View
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxLGN = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLGN)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLGN
            // 
            this.pictureBoxLGN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLGN.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLGN.Name = "pictureBoxLGN";
            this.pictureBoxLGN.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxLGN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLGN.TabIndex = 0;
            this.pictureBoxLGN.TabStop = false;
            // 
            // LGN_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.pictureBoxLGN);
            this.MaximizeBox = false;
            this.Name = "LGN_View";
            this.Text = "LGN_View";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLGN)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLGN;
    }
}