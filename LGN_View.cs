using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TCPクライアント
{
    public partial class LGN_View : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LGN_View(int RF_index)
        {
            //初期位置の設定
            this.StartPosition = FormStartPosition.Manual;
            if (RF_index % 2 == 0)
                this.Left = 700;
            else
                this.Left = 1600;
            this.Top = (RF_index / 2) * 350;
            this.Text = "LGN_View_RF" + RF_index;
            this.BackColor = Color.Black;
            InitializeComponent();
        }


        //非同期描画
        public void DrawLGN(Image bmp)
        {
            try
            {
                pictureBoxLGN.Image = bmp;
            }
             catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 描画のリフレッシュ
        /// </summary>
        public void DrawRefresh(Image bmp)
        {
            pictureBoxLGN.Image = bmp;
            pictureBoxLGN.Refresh();
        }

        public void ExportBMP(int RFindex)
        {
            pictureBoxLGN.Image.Save("..\\ImageData\\LGN_RF" + RFindex + ".png");
        }


    }
}
