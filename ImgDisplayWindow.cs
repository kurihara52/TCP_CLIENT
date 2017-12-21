using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;

namespace TCPクライアント
{
    public partial class ImgDisplayWindow : Form
    {
      
        /// <summary>
        /// 画像処理クラスのインスタンス
        /// </summary>
        ImageProcessing improc = new ImageProcessing();


        /// <summary>
        /// ウィンドウのコンストラクタ
        /// </summary>
        public ImgDisplayWindow()
        {
            this.StartPosition = FormStartPosition.Manual;
            this.DoubleBuffered = false;
            this.Left = 50;
            this.Top = 150;
            InitializeComponent();
        }

        /// <summary>
        /// ロード時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgDisplayWindow_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        /// <summary>
        /// マスク画像の描画
        /// </summary>
        /// <param name="bmp"></param>
        public void DrawMask(Image bmp, Image Mask)
        {
            //Graphics g = Graphics.FromImage(bmp);
            //g.DrawImage(Mask, 0, 0, 1280, 960);
            pictureBox1.Image = bmp;
        }

        //非同期描画
        public async void AsyncDraw(int rowLength, int columnLength, byte [] receiveBuf, Image Mask)
        {
            byte [] drawBuf = receiveBuf;
            await Task.Run(() => {
                Image bmp = improc.ToImage(drawBuf, rowLength, columnLength);
                //Graphics g = Graphics.FromImage(bmp);
                try
                {
                    //g.DrawImage(Mask, 0, 0, rowLength, columnLength);
                    pictureBox1.Image = bmp;
                }
                catch(Exception ex)
                {
                   // MessageBox.Show(ex.Message);
                }
            });
        }

        /// <summary>
        /// 描画のリフレッシュ
        /// </summary>
        public void ImgRefresh()
        {
            pictureBox1.Update();
        }

        /// <summary>
        /// 描画のリセット
        /// </summary>
        public void ImgReset()
        {
            pictureBox1.Image = null;
        }

        /// <summary>
        /// 画像出力
        /// </summary>
        public void ExportBMP()
        {
            pictureBox1.Image.Save("..\\ImageData\\CameraData.png");
        }

        /// <summary>
        /// デバッグ用の描画処理
        /// </summary>
        /// <param name="bmp"></param>
        public void DebugDraw(Image bmp)
        {
            pictureBox1.Image = bmp;
        }



    }
}
