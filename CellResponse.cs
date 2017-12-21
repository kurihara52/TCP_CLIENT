using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TCPクライアント
{
    public partial class CellResponse : Form
    {
        public CellResponse(int RF_index)
        {
            InitializeComponent();
            //初期位置の設定
            this.StartPosition = FormStartPosition.Manual;
            if (RF_index % 2 == 0)
                this.Left = 1000;
            else
                this.Left = 1900;
            this.Top = (RF_index / 2) * 350;
        }


        /// <summary>
        /// 配列の値をバインド
        /// </summary>
        /// <param name="array"></param>
        /// <param name="N_bind"></param>
        /// <returns></returns>
        public List<int> Bind(byte[] array, int N_bind)
        {
            int arraySize = array.Count();
            int transientValue;
            if (N_bind == 2)
                transientValue = (int)(Math.Pow(2, 16) / 2);
            else if (N_bind == 3)
                transientValue = (int)(Math.Pow(2, 24) / 2);
            else if (N_bind == 4)
                transientValue = (int)(Math.Pow(2, 32) / 2);
            else
                transientValue = 0;
            List<int> bind_array = new List<int>();
            int x = 0;
            for (int i = 0; i < arraySize;)
            {
                for (int j = 0; j < N_bind; j++)
                {
                    x += array[i++] * (int)(Math.Pow(256, j));
                }
                bind_array.Add(CalcComplement(x, transientValue));
                x = 0;
            }
            return bind_array;
        }

        /// <summary>
        /// 2の補数の計算
        /// </summary>
        /// <param name="x"></param>
        /// <param name="transientValue"></param>
        /// <returns></returns>
        private int CalcComplement(int x, int transientValue)
        {
            int y;
            if (x < transientValue)
                y = x;
            else
                y = -2 * transientValue + x;
            return y;
        }

        /// <summary>
        /// 描画のリフレッシュ
        /// </summary>
        /// <param name="NDS_NL_BMP"></param>
        /// <param name="NDS_L_BMP"></param>
        /// <param name="DSC_BMP"></param>
        public void DrawRefresh(Image NDS_NL_BMP, Image NDS_L_BMP, Image DSC_BMP, Image MDC_BMP)
        {
            Application.DoEvents();
            picturebox_NDS_NL.Image = NDS_NL_BMP;
            pictureBox_NDS_L.Image = NDS_L_BMP;
            pictureBox_DSC.Image = DSC_BMP;
            pictureBox_MDC.Image = MDC_BMP;
            //picturebox_NDS_NL.Refresh();
            //pictureBox_NDS_L.Refresh();
            //pictureBox_DSC.Refresh();
        }



        /// <summary>
        /// 単純細胞/複雑細胞/運動検出細胞の出力をビットマップデータに変換
        /// </summary>
        /// <param name="NDS"></param>
        /// <param name="max_value"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Image Convert_BMP_Cell(List<int> Cell, double max_value, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            //ビットマップデータをアンマネージ配列にコピーしてから処理
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte[] buf = new byte[bmp.Width * bmp.Height * 4];
            Marshal.Copy(data.Scan0, buf, 0, buf.Length);
            int count = 0;
            double normalized_NDS;
            byte R = 0;
            byte B = 0;
            byte G = 0;
            for (int i = 0; i < buf.Length;)
            {
                normalized_NDS = Cell[count] / max_value;
                if (normalized_NDS >= 0)
                {
                    R = 255;
                    G = (byte)(255 - normalized_NDS * normalized_NDS * normalized_NDS * 255);
                    B = (byte)(255 - normalized_NDS * normalized_NDS * normalized_NDS * 255);
                }
                else
                {
                    R = (byte)(255 + normalized_NDS * normalized_NDS * normalized_NDS * 255);
                    G = (byte)(255 + normalized_NDS * normalized_NDS * normalized_NDS * 255);
                    B = 255;
                }
                buf[i++] = B;   //Blue
                buf[i++] = G;   //Green
                buf[i++] = R;   //Red
                buf[i++] = 255;

                count++;
            }
            Marshal.Copy(buf, 0, data.Scan0, buf.Length);
            bmp.UnlockBits(data);
            return bmp;
        }



        /// <summary>
        /// 全受容野の単純細胞をビットマップのリストに変換
        /// </summary>
        /// <param name="NDS_NL"></param>
        /// <param name="N_Cell"></param>
        /// <param name="N_RF"></param>
        /// <returns></returns>
        public List<Image> Convert_BMP_NDS_ALL(List<int> NDS, int N_Cell, int N_RF)
        {
            List<int> NDS_precut = new List<int>();
            List<int> NDS_postcut = new List<int>();
            List<Image> NDS_BMP = new List<Image>();
            int abs_MAX;

            for (int i = 0; i < N_RF; i++)
            {
                //1受容野分のデータを取り出す
                for (int j = 0; j < N_Cell; j++)
                {
                    NDS_precut.Add(NDS[0]);
                    NDS.RemoveAt(0);
                }
                //左右の両端をカット
                NDS_postcut = (CutExtraElement(NDS_precut, 4, 39));
                //絶対値の最大値計算
                abs_MAX = SelectAbsMax(NDS_postcut);
                //ビットマップに変換
                NDS_BMP.Add(Convert_BMP_Cell(NDS_postcut, abs_MAX, 31, 23));
                //リスト初期化
                NDS_precut.Clear();
                NDS_postcut.Clear();
            }
            return NDS_BMP;
        }

        /// <summary>
        /// 全受容野の複雑細胞をビットマップのリストに変換
        /// </summary>
        /// <param name="DSC"></param>
        /// <param name="N_Cell"></param>
        /// <param name="N_RF"></param>
        /// <returns></returns>
        public List<Image> Convert_BMP_DSC_ALL(List<int> DSC, int N_Cell, int N_RF)
        {
            List<int> DSC_temp = new List<int>();
            List<Image> DSC_BMP = new List<Image>();
            int abs_MAX;

            for (int i = 0; i < N_RF; i++)
            {
                //1受容野分のデータを取り出す
                for (int j = 0; j < N_Cell; j++)
                {
                    DSC_temp.Add(DSC[0]);
                    DSC.RemoveAt(0);
                }
                //絶対値の最大値計算
                abs_MAX = SelectAbsMax(DSC_temp);
                //ビットマップに変換
                DSC_BMP.Add(Convert_BMP_Cell(DSC_temp, abs_MAX, 31, 23));
                //リストの初期化
                DSC_temp.Clear();
            }
            return DSC_BMP;
        }


        /// <summary>
        /// 全受容野の運動検出細胞をビットマップのリストに変換
        /// </summary>
        /// <param name="MDC"></param>
        /// <param name="N_Cell"></param>
        /// <param name="N_RF"></param>
        /// <returns></returns>
        public List<Image> Convert_BMP_MDC_ALL(List<int> MDC, int N_Cell, int N_RF)
        {
            List<int> MDC_temp = new List<int>();
            List<Image> MDC_BMP = new List<Image>();
            int abs_MAX;

            for (int i = 0; i < N_RF; i++)
            {
                //1受容野分のデータを取り出す
                for (int j = 0; j < N_Cell; j++)
                {
                    MDC_temp.Add(MDC[0]);
                    MDC.RemoveAt(0);
                }
                //絶対値の最大値計算
                abs_MAX = SelectAbsMax(MDC_temp);
                //ビットマップに変換
                MDC_BMP.Add(Convert_BMP_Cell(MDC_temp, abs_MAX, 31, 31));
                //リストの初期化
                MDC_temp.Clear();
            }
            return MDC_BMP;
        }


        /// <summary>
        /// NDS細胞の両端をカット
        /// </summary>
        /// <param name="preCutData"></param>
        /// <param name="NumOfCut"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private List<int> CutExtraElement(List<int> preCutData, int NumOfCut, int width)
        {
            List<int> postCutData = new List<int>();
            for (int i = 0; i < preCutData.Count; i++)
            {
                if (NumOfCut <= (i % width) && (i % width) < width - NumOfCut)
                {
                    postCutData.Add(preCutData[i]);
                }
            }
            return postCutData;
        }

        /// <summary>
        /// 絶対値の最大値を求める
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private int SelectAbsMax(List<int> d)
        {
            int abs_MAX = 0;
            int max = d.Max();
            int min = d.Min();
            if (Math.Abs(max) < Math.Abs(min))
                abs_MAX = Math.Abs(min);
            else
                abs_MAX = Math.Abs(max);
            return abs_MAX;
        }

        /// <summary>
        /// ロード時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CellResponse_Load(object sender, EventArgs e)
        {
            picturebox_NDS_NL.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_NDS_L.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_DSC.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_MDC.SizeMode = PictureBoxSizeMode.Zoom;
        }

    }
}
