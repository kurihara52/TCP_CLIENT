using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;

namespace TCPクライアント
{
    class LGN_Projector
    {

        /// <summary>
        /// 受容野に該当する画像のX座標
        /// </summary>
        List<int> CoordCoresRF_X = new List<int>();

        /// <summary>
        /// 受容野に該当する画像のY座標
        /// </summary>
        List<int> CoordCoresRF_Y = new List<int>();

        /// <summary>
        /// LGNに射影したX座標
        /// </summary>
        List<double> X_LGN = new List<double>();

        /// <summary>
        /// LGNに射影したY座標
        /// </summary>
        List<double> Y_LGN = new List<double>();

        /// <summary>
        /// LGNに投射した画像データ
        /// </summary>
        List<byte> LGN = new List<byte>();

        /// <summary>
        /// 受容野に含まれる細胞の数
        /// </summary>
        List<int> N_cell = new List<int>();

        /// <summary>
        /// 細胞の密度
        /// </summary>
        double cellDensity = 2;


        //細胞の三次元座標を計算
        private List<double> CalcCellCoord(int Ximg, int Yimg)
        {
            double Xcell;   //細胞のX座標
            double Ycell;   //細胞のY座標
            double Zcell;   //細胞のZ座標
            double RootElement;     //Z座標を計算するためのルートの中身
            double Yimg_bias = (Param.Wimg - Param.Himg) / 2;  //Y座標のバイアス値

            Xcell = (1 - 2 * Ximg / Param.Wimg) * Math.Sin((Param.HAOV * Math.PI / 180) / 2); ;
            //Ycell = (1 - 2 * Yimg / Param.Himg) * Math.Sin((Param.VAOV * Math.PI / 180) / 2);
            Ycell = (1 - 2 * (Yimg + Yimg_bias) / Param.Wimg) * Math.Sin((Param.HAOV * Math.PI / 180) / 2);
            RootElement = 1 - Math.Pow(Xcell, 2) - Math.Pow(Ycell, 2);
            if (RootElement > 0)
            {
                Zcell = Math.Sqrt(RootElement);
            }
            else
                Zcell = 0;
            var CoordCell = new List<double> { Xcell, Ycell, Zcell };
            return CoordCell;
        }

        //細胞が受容野に含まれるかを判定
        private bool IsIncludedRF(double RF_X, double RF_Y, double RF_Z, double Xcell, double Ycell, double Zcell, out double dot)
        {
            dot = (RF_X * Xcell + RF_Y * Ycell + RF_Z * Zcell);
            if (dot >= Math.Cos(Param.R))
                return true;
            else
                return false;
        }

        //単位ベクトルの計算(LocX)
        private List<double> CalcLocX(double RF_X, double RF_Y, double RF_Z)
        {
            var LocX = new List<double>();      //計算結果を保存するリスト
            //ベクトルの外積の計算
            double crossprod_x = RF_Y * Param.uz_z - RF_Z * Param.uz_y;
            double crossprod_y = RF_Z * Param.uz_x - RF_X * Param.uz_z;
            double crossprod_z = RF_X * Param.uz_y - RF_Y * Param.uz_x;
            //ベクトルの外積の絶対値の計算
            double absolute = Math.Sqrt(Math.Pow(crossprod_x, 2) + Math.Pow(crossprod_y, 2) + Math.Pow(crossprod_z, 2));
            //外積が0のときの例外処理(xの単位ベクトルに置き換える)
            if (absolute == 0)
            {
                crossprod_x = 1;
                crossprod_y = 0;
                crossprod_z = 0;
                absolute = 1;
            }
            //LocXの計算
            LocX.Add(crossprod_x / absolute + RF_X);
            LocX.Add(crossprod_y / absolute + RF_Y);
            LocX.Add(crossprod_z / absolute + RF_Z);
            return LocX;
        }

        //単位ベクトルの計算(LocY)
        private List<double> CalcLocY(double RF_X, double RF_Y, double RF_Z, double LocX_x, double LocX_y, double LocX_z)
        {
            var LocY = new List<double>();      //計算結果を保存するリスト
            //ベクトルの外積の計算
            double crossprod_x = RF_Y * LocX_z - RF_Z * LocX_y;
            double crossprod_y = RF_Z * LocX_x - RF_X * LocX_z;
            double crossprod_z = RF_X * LocX_y - RF_Y * LocX_x;
            //ベクトルの外積の絶対値の計算
            double absolute = Math.Sqrt(Math.Pow(crossprod_x, 2) + Math.Pow(crossprod_y, 2) + Math.Pow(crossprod_z, 2));
            //外積が0のときの例外処理(yの単位ベクトルに置き換える)
            if (absolute == 0)
            {
                crossprod_x = 0;
                crossprod_y = 1;
                crossprod_z = 0;
                absolute = 1;
            }

            LocY.Add(crossprod_x / absolute + RF_X);
            LocY.Add(crossprod_y / absolute + RF_Y);
            LocY.Add(crossprod_z / absolute + RF_Z);
            return LocY;
        }


        //LGN細胞のX座標計算
        private double CalcXlgn(double RF_X, double RF_Y, double RF_Z, double LocX_x, double LocX_y, double LocX_z, double Cell_x, double Cell_y, double Cell_z, double dot)
        {
            double Xlgn;
            //ベクトルの内積の計算
            double dotprod_x = LocX_x * (Cell_x / dot - RF_X);
            double dotprod_y = LocX_y * (Cell_y / dot - RF_Y);
            double dotprod_z = LocX_z * (Cell_z / dot - RF_Z);
            Xlgn = (dotprod_x + dotprod_y + dotprod_z) / Param.Delta_Beta;
            return Xlgn;
        }


        //LGN細胞のY座標計算
        private double CalcYlgn(double RF_X, double RF_Y, double RF_Z, double LocY_x, double LocY_y, double LocY_z, double Cell_x, double Cell_y, double Cell_z, double dot)
        {
            double Ylgn;
            //ベクトルの内積の計算
            double dotprod_x = LocY_x * (Cell_x / dot - RF_X);
            double dotprod_y = LocY_y * (Cell_y / dot - RF_Y);
            double dotprod_z = LocY_z * (Cell_z / dot - RF_Z);
            Ylgn = (dotprod_x + dotprod_y + dotprod_z) / Param.Delta_Beta;
            return Ylgn;
        }


        //受容野に該当する画像のXY座標の計算
        private void CalcCoordCoresRF(int RF_index, double density)
        {
            var CoordCell = new List<double>();
            var RF_value = new List<double>();
            double dot;
            var LocX = new List<double>();
            var LocY = new List<double>();
            double Xlgn = 0;
            double Ylgn = 0;

            for (double Yimg = 0; Yimg <= 960; Yimg += density)
            {
                for (double Ximg = 0; Ximg <= 1280; Ximg += density)
                {
                    CoordCell.AddRange(CalcCellCoord((int)Ximg, (int)Yimg));    //細胞の座標計算
                    RF_value.AddRange(GetRFvalue(RF_index));       //RFベクトル取得
                    //細胞が受容野に含まれるか判定
                    if (IsIncludedRF(RF_value[0], RF_value[1], RF_value[2], CoordCell[0], CoordCell[1], CoordCell[2], out dot))
                    {
                        LocX.AddRange(CalcLocX(RF_value[0], RF_value[1], RF_value[2]));   //LocXの計算
                        LocY.AddRange(CalcLocY(RF_value[0], RF_value[1], RF_value[2], LocX[0], LocX[1], LocX[2]));  //LocYの計算
                        Xlgn = CalcXlgn(RF_value[0], RF_value[1], RF_value[2], LocX[0], LocX[1], LocX[2], CoordCell[0], CoordCell[1], CoordCell[2], dot);  //LGNのX座標計算
                        Ylgn = CalcYlgn(RF_value[0], RF_value[1], RF_value[2], LocY[0], LocY[1], LocY[2], CoordCell[0], CoordCell[1], CoordCell[2], dot);  //LGNのY座標計算
                        X_LGN.Add(Xlgn);    //LGNのX座標を保存
                        Y_LGN.Add(Ylgn);    //LGNのY座標を保存
                        CoordCoresRF_X.Add((int)Ximg);   //受容野に対応する画像のX座標を保存
                        CoordCoresRF_Y.Add((int)Yimg);   //受容野に対応する画像のY座標を保存
                    }
                    //リストの初期化
                    CoordCell.Clear();
                    RF_value.Clear();
                }
            }
        }



        //入力されたインデックスに応じてRFの値を取得する
        private List<double> GetRFvalue(int index)
        {
            List<double> RF_value;      //RFベクトルを保存するリスト

            switch (index)
            {
                case 0:
                    RF_value = new List<double> { 0, 0, 1 };
                    break;
                case 1:
                    RF_value = new List<double> { 0.0506492, 0, 0.998717 };
                    break;
                case 2:
                    RF_value = new List<double> { 0.0253246, 0.0438635, 0.998717 };
                    break;
                case 3:
                    RF_value = new List<double> { -0.0253246, 0.0438635, 0.998717 };
                    break;
                case 4:
                    RF_value = new List<double> { -0.0506492, 0, 0.998717 };
                    break;
                case 5:
                    RF_value = new List<double> { -0.0253246, -0.0438635, 0.998717 };
                    break;
                case 6:
                    RF_value = new List<double> { 0.0253246, -0.0438635, 0.998717 };
                    break;
                default:
                    RF_value = new List<double> { 0, 0, 0 };
                    break;
            }
            return RF_value;
        }



        /// <summary>
        /// 受容野で切り出す画像データをバイト配列で出力
        /// </summary>
        /// <param name="b"></param>
        /// <param name="X_RF"></param>
        /// <param name="Y_RF"></param>
        /// <returns></returns>
        public List<byte> CalcLGN(byte[] b, List<int> X_RF, List<int> Y_RF)
        {
            List<byte> LGN = new List<byte>();
            int X = 0;  //現在のX座標
            int Y = 0;  //現在のY座標
            int Coord1D = 0; //画像の１次元座標
            int RF_count = X_RF.Count;   //リストRFのサイズ
            for (int i = 0; i < RF_count; i++)
            {
                X = X_RF[i];
                Y = Y_RF[i];
                Coord1D = (X+1 + Y * 1280);
                LGN.Add(b[Coord1D]);
            }
            return LGN;
        }

        /// <summary>
        /// LGNの細胞の出力をビットマップに変換
        /// </summary>
        /// <param name="X_LGN"></param>
        /// <param name="Y_LGN"></param>
        /// <param name="LGN"></param>
        /// <param name="FormWidth"></param>
        /// <param name="FormHeight"></param>
        /// <returns></returns>
        public Bitmap LGNCell2Bitmap(List<double> X_LGN, List<double> Y_LGN, List<byte> LGN, int FormWidth, int FormHeight)
        {
            Bitmap bmp = new Bitmap(FormWidth, FormHeight, PixelFormat.Format32bppArgb);
            //ビットマップデータをアンマネージ配列にコピーしてから処理
            BitmapData data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);
            byte[] buf = new byte[bmp.Width * bmp.Height * 4];
            Marshal.Copy(data.Scan0, buf, 0, buf.Length);
            int count = X_LGN.Count;    //リストのサイズ
            int Coord1D = 0;        //画像の一次元座標
            int Coord1D_buf = 0;
            //受容野に対応する座標を細胞の出力で上書き
            int CellSize = 7;
           /* for(int i = 0; i < buf.Length;)
            {
                buf[i + 3] = 255;
                i += 4;
            }*/
            for (int i = 0; i < count; i++)
            {
                Coord1D = (int)(-X_LGN[i]*10) + FormWidth / 2 + ((int)(-Y_LGN[i]*10) + FormHeight / 2) * FormWidth;
                Coord1D = Coord1D * 4;
                Coord1D_buf = Coord1D;
                for (int j = 0; j < CellSize; j++)
                {
                    for (int k = 0; k < CellSize; k++)
                    {
                        buf[Coord1D++] = LGN[i];
                        buf[Coord1D++] = LGN[i];
                        buf[Coord1D++] = LGN[i];
                        buf[Coord1D++] = 255;
                    }
                    Coord1D = Coord1D_buf + FormWidth * 4;
                    Coord1D_buf += FormWidth * 4;
                }
            }
            //ビットマップデータをリターン
            Marshal.Copy(buf, 0, data.Scan0, buf.Length);
            bmp.UnlockBits(data);
            return bmp;
        }


        /// <summary>
        /// 網膜細胞からLGN細胞への射影結果をビットマップに変換
        /// </summary>
        /// <param name="RF_index"></param>
        /// <param name="RetinalCell"></param>
        /// <param name="Ximg"></param>
        /// <param name="Yimg"></param>
        /// <param name="Xlgn"></param>
        /// <param name="Ylgn"></param>
        /// <returns></returns>
        public void Dummy(int RF_index, byte[] RetinalCell, List<int> Ximg, List<int> Yimg, List<double> Xlgn, List<double> Ylgn, LGN_View LGN_View)
        {
            Bitmap bmp = new Bitmap(600, 600, PixelFormat.Format32bppArgb);
            //受容野領域の画像データの切り出し
            LGN.AddRange(CalcLGN(RetinalCell, Ximg, Yimg));
            //切り出したLGN細胞をファイル出力
            StreamWriter sw = new StreamWriter("LGN_RF0.dat");
            foreach (var x in LGN)
            {
                //sw.WriteLine(Convert.ToString(x, 2).PadLeft(8, '0'));
                sw.WriteLine(x);
            }
            sw.Close();
            //LGNへの射影結果をビットマップに変換
            bmp = LGNCell2Bitmap(Xlgn, Ylgn, LGN, 600, 600);
            //描画処理
            LGN_View.DrawLGN(bmp);
            //リストの初期化
            LGN.Clear();
        }


        public Image Retina2LGN(int RF_index, List<int> Ximg, List<int> Yimg, List<double> Xlgn, List<double> Ylgn, byte[] RetinalCell)
        {
            Bitmap bmp = new Bitmap(600, 600, PixelFormat.Format32bppArgb);
            //受容野領域の画像データの切り出し
            LGN.AddRange(CalcLGN(RetinalCell, Ximg, Yimg));
            //LGNへの射影結果をビットマップに変換
            bmp = LGNCell2Bitmap(Xlgn, Ylgn, LGN, 600, 600);
            //リストの初期化
            LGN.Clear();
            return bmp;
        }


        /// <summary>
        /// 受容野で切り出す画像のXY座標とLGN細胞のXY座標を計算
        /// </summary>
        /// <param name="Ximg"></param>
        /// <param name="Yimg"></param>
        /// <param name="Xlgn"></param>
        /// <param name="Ylgn"></param>
        /// <param name="RF_index"></param>
        public void CalcLGN_Coord(ref List<int> Ximg, ref List<int> Yimg, ref List<double> Xlgn, ref List<double> Ylgn, int RF_index)
        {
            //LGNのXY座標を計算
            CalcCoordCoresRF(RF_index, cellDensity);
            Ximg.AddRange(CoordCoresRF_X);
            Yimg.AddRange(CoordCoresRF_Y);
            Xlgn.AddRange(X_LGN);
            Ylgn.AddRange(Y_LGN);
            //リストの初期化
            CoordCoresRF_X.Clear();
            CoordCoresRF_Y.Clear();
            X_LGN.Clear();
            Y_LGN.Clear();
        }


        //パラメータを保存するクラス
        static class Param
        {
            public const double Inv_Wimg = 0.00078125;
            public const double Inv_Himg = 0.001041667;
            public const double CosR = 0.99920743186;
            public const double Inv_Delta_Beta = 640.4385723;
            public const int uz_x = 0;
            public const int uz_y = 0;
            public const int uz_z = 1;
            public const double SinHAOV = 0.81915204428;    //sin(55deg)
            public const double SinVAOV = 0.6593458151;     //sin(41.25deg)

            public const double Wimg = 1280;
            public const double Himg = 960;
            public const double HAOV = 110;
            public const double VAOV = 82.5;
            //public const double R = 0.0398164;
            public const double R = 0.03985;
            public const double Delta_Beta = 0.00156143;
        }


    }
}
