using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TCPクライアント
{
    class ImageProcessing
    {
        /// <summary>
        /// イメージコンバータのインスタンス生成
        /// </summary>
        static ImageConverter imgconv = new ImageConverter();


        /// <summary>
        /// 画像をグレースケールに変換
        /// </summary>
        /// <returns>グレースケールデータ</returns>
        public Image TransGray()
        {
            Bitmap bmp = Properties.Resources.Figure1;

            //ビットマップデータをアンマネージ配列にコピーしてから処理
            BitmapData data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);
            byte[] buf = new byte[bmp.Width * bmp.Height * 4];
            Marshal.Copy(data.Scan0, buf, 0, buf.Length);
            for (int i = 0; i < buf.Length;)
            {
                byte grey = (byte)(0.299 * buf[i] + 0.587 * buf[i + 1] + 0.114 * buf[i + 2]);
                buf[i++] = grey;
                buf[i++] = grey;
                buf[i++] = grey;
                i++;
            }
            Marshal.Copy(buf, 0, data.Scan0, buf.Length);
            bmp.UnlockBits(data);

            return (Image)bmp;
        }


        /// <summary>
        /// 画像をbyte配列に変換
        /// </summary>
        /// <returns></returns>
        public byte[] ToByte(int figNum)
        {
            Bitmap bmp;
            switch (figNum)
            {
                case 0:
                    bmp = Properties.Resources.Figure1;
                    break;
                case 1:
                    bmp = Properties.Resources.Figure2;
                    break;
                case 2:
                    bmp = Properties.Resources.Figure3;
                    break;
                case 3:
                    bmp = Properties.Resources.Figure4;
                    break;
                case 4:
                    bmp = Properties.Resources.Figure5;
                    break;
                case 5:
                    bmp = Properties.Resources.Figure6;
                    break;
                case 6:
                    bmp = Properties.Resources.Figure7;
                    break;
                case 7:
                    bmp = Properties.Resources.Figure8;
                    break;
                case 8:
                    bmp = Properties.Resources.Figure9;
                    break;
                case 9:
                    bmp = Properties.Resources.Figure10;
                    break;
                case 10:
                    bmp = Properties.Resources.Figure11;
                    break;
                case 11:
                    bmp = Properties.Resources.Figure12;
                    break;
                case 12:
                    bmp = Properties.Resources.Figure13;
                    break;
                case 13:
                    bmp = Properties.Resources.IMG0;
                    break;
                case 14:
                    bmp = Properties.Resources.IMG1;
                    break;
                case 15:
                    bmp = Properties.Resources.IMG2;
                    break;
                case 16:
                    bmp = Properties.Resources.IMG3;
                    break;
                case 17:
                    bmp = Properties.Resources.IMG4;
                    break;
                case 18:
                    bmp = Properties.Resources.IMG5;
                    break;
                default:
                    bmp = Properties.Resources.Figure1;
                    break;
            }
            //ビットマップデータをアンマネージ配列にコピーしてから処理
            BitmapData data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);
            byte[] buf = new byte[bmp.Width * bmp.Height * 4];
            Marshal.Copy(data.Scan0, buf, 0, buf.Length);
            byte[] imgData = new byte[bmp.Width * bmp.Height];
            for (int i = 0; i < imgData.Length;)
            {
                byte grey = (byte)(0.299 * buf[i*4] + 0.587 * buf[i*4 + 1] + 0.114 * buf[i*4 + 2]);
                imgData[i] = grey;
                //buf[i++] = grey;
                //buf[i++] = grey;
                i++;
            }
            Marshal.Copy(buf, 0, data.Scan0, buf.Length);
            bmp.UnlockBits(data);
            return imgData;
        }



        /// <summary>
        /// バイト配列をビットマップに変換
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Image ToImage(byte[] b, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            //ビットマップデータをアンマネージ配列にコピーしてから処理
            BitmapData data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);
            byte[] buf = new byte[bmp.Width * bmp.Height * 4];
            Marshal.Copy(data.Scan0, buf, 0, buf.Length);

            int count = 0;
            for (int i = 0; i < buf.Length;)
            {
                buf[i++] = b[count];
                buf[i++] = b[count];
                buf[i++] = b[count];
                buf[i++] = 255;
                count += 1;
            }
            Marshal.Copy(buf, 0, data.Scan0, buf.Length);
            bmp.UnlockBits(data);
            return bmp;
        }

        /// <summary>
        /// 受容野で切り出した領域でマスク
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="X_RF"></param>
        /// <param name="Y_RF"></param>
        /// <returns></returns>
        public Image MakeMask(int width, int height, List<int> X_RF, List<int> Y_RF)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            //ビットマップデータをアンマネージ配列にコピーしてから処理
            BitmapData data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);
            byte[] buf = new byte[bmp.Width * bmp.Height * 4];
            Marshal.Copy(data.Scan0, buf, 0, buf.Length);
            int X = 0;  //現在のX座標
            int Y = 0;  //現在のY座標
            int Coord1D = 0; //画像の１次元座標
            int RF_count = X_RF.Count;   //リストRFのサイズ
            for (int i = 0; i < buf.Length;)
            {
                buf[i++] = 0;
                buf[i++] = 0;
                buf[i++] = 0;
                buf[i++] = 200;
            }
            for (int i = 0; i < RF_count; i++)
            {
                X = X_RF[i];
                Y = Y_RF[i];
                Coord1D = (X + Y * 1280) * 4;
                buf[Coord1D + 3] = 0;
            }
            Marshal.Copy(buf, 0, data.Scan0, buf.Length);
            bmp.UnlockBits(data);
            return bmp;
        }

    }
}
