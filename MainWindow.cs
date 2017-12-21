using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;

namespace TCPクライアント
{
    public partial class MainWindow : Form
    {

        /// <summary>
        /// tcp接続のタイムアウト処理用クラスのインスタンス
        /// </summary>
        ManualResetEvent timeout = new ManualResetEvent(false);

        /// <summary>
        /// TcpClientクラスのインスタンス
        /// </summary>
        TcpClient tcp;

        /// <summary>
        /// 画像表示ウィンドウのインスタンス
        /// </summary>
        ImgDisplayWindow ImgDisplay;

        /// <summary>
        /// RF0の写像を表示するウィンドウのインスタンス
        /// </summary>
        LGN_View LGN_RF0;

        /// <summary>
        /// RF1の写像を表示するウィンドウのインスタンス
        /// </summary>
        LGN_View LGN_RF1;

        /// <summary>
        /// RF2の写像を表示するウィンドウのインスタンス
        /// </summary>
        LGN_View LGN_RF2;

        /// <summary>
        /// RF3の写像を表示するウィンドウのインスタンス
        /// </summary>
        LGN_View LGN_RF3;

        /// <summary>
        /// RF4の写像を表示するウィンドウのインスタンス
        /// </summary>
        LGN_View LGN_RF4;

        /// <summary>
        /// RF5の写像を表示するウィンドウのインスタンス
        /// </summary>
        LGN_View LGN_RF5;

        /// <summary>
        /// RF6の写像を表示するウィンドウのインスタンス
        /// </summary>
        LGN_View LGN_RF6;

        /// <summary>
        /// RF0で切り出す画像のX座標
        /// </summary>
        List<int> Ximg_RF0 = new List<int>();

        /// <summary>
        /// RF1で切り出す画像のX座標
        /// </summary>
        List<int> Ximg_RF1 = new List<int>();

        /// <summary>
        /// RF2で切り出す画像のX座標
        /// </summary>
        List<int> Ximg_RF2 = new List<int>();

        /// <summary>
        /// RF3で切り出す画像のX座標
        /// </summary>
        List<int> Ximg_RF3 = new List<int>();

        /// <summary>
        /// RF4で切り出す画像のX座標
        /// </summary>
        List<int> Ximg_RF4 = new List<int>();

        /// <summary>
        /// RF5で切り出す画像のX座標
        /// </summary>
        List<int> Ximg_RF5 = new List<int>();

        /// <summary>
        /// RF6で切り出す画像のX座標
        /// </summary>
        List<int> Ximg_RF6 = new List<int>();

        /// <summary>
        /// すべての受容野で切り出す画像のX座標
        /// </summary>
        List<int> Ximg_All = new List<int>();

        /// <summary>
        /// RF0で切り出す画像のY座標
        /// </summary>
        List<int> Yimg_RF0 = new List<int>();

        /// <summary>
        /// RF1で切り出す画像のY座標
        /// </summary>
        List<int> Yimg_RF1 = new List<int>();

        /// <summary>
        /// RF2で切り出す画像のY座標
        /// </summary>
        List<int> Yimg_RF2 = new List<int>();

        /// <summary>
        /// RF3で切り出す画像のY座標
        /// </summary>
        List<int> Yimg_RF3 = new List<int>();

        /// <summary>
        /// RF4で切り出す画像のY座標
        /// </summary>
        List<int> Yimg_RF4 = new List<int>();

        /// <summary>
        /// RF5で切り出す画像のY座標
        /// </summary>
        List<int> Yimg_RF5 = new List<int>();

        /// <summary>
        /// RF6で切り出す画像のY座標
        /// </summary>
        List<int> Yimg_RF6 = new List<int>();

        /// <summary>
        /// すべての受容野で切り出す画像のY座標
        /// </summary>
        List<int> Yimg_All = new List<int>();

        /// <summary>
        /// RF0で切り出すLGNのX座標
        /// </summary>
        List<double> Xlgn_RF0 = new List<double>();

        /// <summary>
        /// RF1で切り出すLGNのX座標
        /// </summary>
        List<double> Xlgn_RF1 = new List<double>();

        /// <summary>
        /// RF2で切り出すLGNのX座標
        /// </summary>
        List<double> Xlgn_RF2 = new List<double>();

        /// <summary>
        /// RF3で切り出すLGNのX座標
        /// </summary>
        List<double> Xlgn_RF3 = new List<double>();

        /// <summary>
        /// RF4で切り出すLGNのX座標
        /// </summary>
        List<double> Xlgn_RF4 = new List<double>();

        /// <summary>
        /// RF5で切り出すLGNのX座標
        /// </summary>
        List<double> Xlgn_RF5 = new List<double>();

        /// <summary>
        /// RF6で切り出すLGNのX座標
        /// </summary>
        List<double> Xlgn_RF6 = new List<double>();

        /// <summary>
        /// RF0で切り出すLGNのY座標
        /// </summary>
        List<double> Ylgn_RF0 = new List<double>();

        /// <summary>
        /// RF1で切り出すLGNのY座標
        /// </summary>
        List<double> Ylgn_RF1 = new List<double>();

        /// <summary>
        /// RF2で切り出すLGNのY座標
        /// </summary>
        List<double> Ylgn_RF2 = new List<double>();

        /// <summary>
        /// RF3で切り出すLGNのY座標
        /// </summary>
        List<double> Ylgn_RF3 = new List<double>();

        /// <summary>
        /// RF4で切り出すLGNのY座標
        /// </summary>
        List<double> Ylgn_RF4 = new List<double>();

        /// <summary>
        /// RF5で切り出すLGNのY座標
        /// </summary>
        List<double> Ylgn_RF5 = new List<double>();

        /// <summary>
        /// RF6で切り出すLGNのY座標
        /// </summary>
        List<double> Ylgn_RF6 = new List<double>();

        /// <summary>
        /// 網膜細胞からLGN細胞への射影を計算するクラスのインスタンス
        /// </summary>
        LGN_Projector LGN_Projector = new LGN_Projector();

        /// <summary>
        /// 送信用画像データのバッファリスト
        /// </summary>
        List<byte> sendDataBuf = new List<byte>();

        /// <summary>
        /// sendDataBufを作成するためのバッファ配列
        /// </summary>
        byte[] tempData;

        /// <summary>
        /// 送信用データのリスト
        /// </summary>
        List<byte> sendData = new List<byte>();

        /// <summary>
        /// 受信データを保存する配列
        /// </summary>
        byte[] receiveData;

        /// <summary>
        /// 受信データのサイズ
        /// </summary>
        int receiveSize = 0;

        /// <summary>
        /// 連続キャプチャを停止するフラグ
        /// </summary>
        bool haltFlag = false;

        /// <summary>
        /// 送信用のパケット生成クラスのインスタンス
        /// </summary>
        PacketConverter pacCon = new PacketConverter();

        /// <summary>
        /// 画像処理クラスのインスタンス
        /// </summary>
        ImageProcessing improc = new ImageProcessing();

        /// <summary>
        /// Tcp通信のネットワークストリームクラス
        /// </summary>
        NetworkStream ns;

        /// <summary>
        /// ストップウォッチのインスタンス
        /// </summary>
        Stopwatch sw = new Stopwatch();

        /// <summary>
        /// マスク画像のビットマップ
        /// </summary>
        Image mask;

        /// <summary>
        /// カメラ画像の描画をリフレッシュしてウィンドウを更新するフラグ
        /// </summary>
        bool DrawFlag_IMG = false;

        /// <summary>
        /// 細胞応答の描画をリフレッシュしてウィンドウを更新するフラグ
        /// </summary>
        bool DrawFlag_Cell = false;

        /// <summary>
        /// 非同期メソッドを抜けるフラグ
        /// </summary>
        bool ReturnFlag = false;

        /// <summary>
        /// LGN細胞のRF0のビットマップ
        /// </summary>
        Image bmp_RF0;

        /// <summary>
        /// LGN細胞のRF1のビットマップ
        /// </summary>
        Image bmp_RF1;

        /// <summary>
        /// LGN細胞のRF2のビットマップ
        /// </summary>
        Image bmp_RF2;

        /// <summary>
        /// LGN細胞のRF3のビットマップ
        /// </summary>
        Image bmp_RF3;

        /// <summary>
        /// LGN細胞のRF4のビットマップ
        /// </summary>
        Image bmp_RF4;

        /// <summary>
        /// LGN細胞のRF5のビットマップ
        /// </summary>
        Image bmp_RF5;

        /// <summary>
        /// LGN細胞のRF6のビットマップ
        /// </summary>
        Image bmp_RF6;

        /// <summary>
        /// 細胞の応答強度を表示するクラスのインスタンス
        /// </summary>
        CellResponse CellResponse0;

        /// <summary>
        /// 細胞の応答強度を表示するクラスのインスタンス
        /// </summary>
        CellResponse CellResponse1;

        /// <summary>
        /// 細胞の応答強度を表示するクラスのインスタンス
        /// </summary>
        CellResponse CellResponse2;

        /// <summary>
        /// 細胞の応答強度を表示するクラスのインスタンス
        /// </summary>
        CellResponse CellResponse3;

        /// <summary>
        /// 細胞の応答強度を表示するクラスのインスタンス
        /// </summary>
        CellResponse CellResponse4;

        /// <summary>
        /// 細胞の応答強度を表示するクラスのインスタンス
        /// </summary>
        CellResponse CellResponse5;

        /// <summary>
        /// 細胞の応答強度を表示するクラスのインスタンス
        /// </summary>
        CellResponse CellResponse6;

        /// <summary>
        /// 遅延なし単純型細胞の応答強度を表示するビットマップ配列
        /// </summary>
        List<Image> NDS_NL_BMP = new List<Image>();

        /// <summary>
        /// 遅延あり単純型細胞の応答強度を表示するビットマップ配列
        /// </summary>
        List<Image> NDS_L_BMP = new List<Image>();

        /// <summary>
        /// 複雑型細胞の応答強度を表示するビットマップ配列
        /// </summary>
        List<Image> DSC_BMP = new List<Image>();

        /// <summary>
        /// 運動検出細胞の応答強度を表示するビットマップ配列
        /// </summary>
        List<Image> MDC_BMP = new List<Image>();

        /// <summary>
        /// 受容野の数
        /// </summary>
        int Num_RF = 7;

        /// <summary>
        /// メインウィンドウのコンストラクタ
        /// </summary>
        public MainWindow()
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Left = 50;
            this.Top = 700;
            InitializeComponent();
        }

        /// <summary>
        /// ロード時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            //各種座標の計算
            LGN_Projector.CalcLGN_Coord(ref Ximg_RF0, ref Yimg_RF0, ref Xlgn_RF0, ref Ylgn_RF0, 0);
            LGN_Projector.CalcLGN_Coord(ref Ximg_RF1, ref Yimg_RF1, ref Xlgn_RF1, ref Ylgn_RF1, 1);
            LGN_Projector.CalcLGN_Coord(ref Ximg_RF2, ref Yimg_RF2, ref Xlgn_RF2, ref Ylgn_RF2, 2);
            LGN_Projector.CalcLGN_Coord(ref Ximg_RF3, ref Yimg_RF3, ref Xlgn_RF3, ref Ylgn_RF3, 3);
            LGN_Projector.CalcLGN_Coord(ref Ximg_RF4, ref Yimg_RF4, ref Xlgn_RF4, ref Ylgn_RF4, 4);
            LGN_Projector.CalcLGN_Coord(ref Ximg_RF5, ref Yimg_RF5, ref Xlgn_RF5, ref Ylgn_RF5, 5);
            LGN_Projector.CalcLGN_Coord(ref Ximg_RF6, ref Yimg_RF6, ref Xlgn_RF6, ref Ylgn_RF6, 6);
            Ximg_All.AddRange(Ximg_RF0); Yimg_All.AddRange(Yimg_RF0);
            Ximg_All.AddRange(Ximg_RF1); Yimg_All.AddRange(Yimg_RF1);
            Ximg_All.AddRange(Ximg_RF2); Yimg_All.AddRange(Yimg_RF2);
            Ximg_All.AddRange(Ximg_RF3); Yimg_All.AddRange(Yimg_RF3);
            Ximg_All.AddRange(Ximg_RF4); Yimg_All.AddRange(Yimg_RF4);
            Ximg_All.AddRange(Ximg_RF5); Yimg_All.AddRange(Yimg_RF5);
            Ximg_All.AddRange(Ximg_RF6); Yimg_All.AddRange(Yimg_RF6);
            mask = improc.MakeMask(1280, 960, Ximg_All, Yimg_All);
            Ximg_All.Clear();
            Yimg_All.Clear();
        }


        /// <summary>
        /// FPGAとのコネクションを開始するボタンのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            tcp  = new TcpClient();
            string ipAddr = textBoxAddr.Text;        //IPアドレス
            int port;
            string portString = textBoxPort.Text;       //ポート番号
            try
            {
                port = int.Parse(portString);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return;
            }
            timeout.Reset();
            //サーバーと接続開始
            tcp.BeginConnect(ipAddr, port, new AsyncCallback(ConnectCallback), tcp);
            //接続のタイムアウト処理           
            if (!timeout.WaitOne(10000))
            {
                tcp.Close();
                MessageBox.Show("接続がタイムアウトしました");
                return;
            }
            richTextDisplay.AppendText("サーバー(");
            richTextDisplay.AppendText(((System.Net.IPEndPoint)tcp.Client.RemoteEndPoint).Address.ToString() + " : ");
            richTextDisplay.AppendText(((System.Net.IPEndPoint)tcp.Client.RemoteEndPoint).Port.ToString() + ")と接続しました(");
            richTextDisplay.AppendText(((System.Net.IPEndPoint)tcp.Client.LocalEndPoint).Address.ToString() + " : ");
            richTextDisplay.AppendText(((System.Net.IPEndPoint)tcp.Client.LocalEndPoint).Port.ToString() + ")\n");
            ns = tcp.GetStream();
        }


        /// <summary>
        /// タイムアウト判定
        /// </summary>
        /// <param name="result"></param>
        private void ConnectCallback(IAsyncResult result)
        {
            timeout.Set();
        }


        /// <summary>
        /// ReadOnlyボタンのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReceive_Click(object sender, EventArgs e)
        {
            //入力されたデータが有効かチェック
            if (!IsDataValid())
            {
                return;
            }
            //接続状況のチェック
            if (!tcp.Connected)
            {
                MessageBox.Show("サーバーと未接続です");
                return;
            }
            int columnLength = 960;      //行サイズの読み取り
            int rowLength = 1280;                    //列サイズの読み取り
            receiveData = new byte[columnLength * rowLength];       //受信したデータを格納する配列\
            //読み取り命令のパケット送信
            sendData.Add(0x01);
            sendData.Add((byte)(byte.Parse(textFlame.Text) - 1));
            ns.Write(sendData.ToArray(), 0, sendData.Count);
            sendData.Clear();
            //計測開始
            sw.Start();
            //データ受信
            ReceivePixelData(rowLength, columnLength);
            //計測終了
            sw.Stop();
            textBox_FrameRate.Text = sw.ElapsedMilliseconds.ToString();
            sw.Reset();
            //画像とLGN細胞の描画
            LGN_Draw(receiveData);
        }


        /// <summary>
        /// SingleCaptureボタンのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSingleCap_Click(object sender, EventArgs e)
        {
            //入力されたデータが有効かチェック
            if (!IsDataValid())
            {
                return;
            }
            //接続状況のチェック
            if (!tcp.Connected)
            {
                MessageBox.Show("サーバーと未接続です");
                return;
            }
            int columnLength = 960;      //行サイズの読み取り
            int rowLength = 1280;                    //列サイズの読み取り
            receiveData = new byte[columnLength * rowLength];       //受信したデータを格納する配列
            //読み取り命令のパケット送信
            sendData.Add(0x02);
            sendData.Add((byte)(byte.Parse(textFlame.Text) - 1));
            ns.Write(sendData.ToArray(), 0, sendData.Count);
            sendData.Clear();
            //計測開始
            sw.Start();
            //データ受信
            ReceivePixelData(rowLength, columnLength);
            //計測終了
            sw.Stop();
            textBox_FrameRate.Text = sw.ElapsedMilliseconds.ToString();
            sw.Reset();
            //画像とLGN細胞の描画
            LGN_Draw(receiveData);
        }


        /// <summary>
        /// 連続モードのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonContiniousCap_Click(object sender, EventArgs e)
        {
            //入力されたデータが有効かチェック
            if (!IsDataValid())
            {
                return;
            }
            //接続状況のチェック
            if (!tcp.Connected)
            {
                MessageBox.Show("サーバーと未接続です");
                return;
            }
            int columnLength = 960;   //列サイズ
            int rowLength = 1280;      //行サイズ
            int measure_cnt = 0;
            receiveData = new byte[columnLength * rowLength];       //受信したデータを格納する配列
            bool isLagged = false;      //受信したデータが遅延あり/なしを判定するフラグ
            //連続読み取り命令のパケット送信
            sendData.Add(0x03);
            sendData.Add((byte)(byte.Parse(textFlame.Text) - 1));
            ns.Write(sendData.ToArray(), 0, sendData.Count);
            sendData.Clear();
            //データの受信と描画の実行
            AsyncRefresh1();
            AsyncRefresh2();
            while(true)
            {
                //待機中の処理を実行
                Application.DoEvents();
                
                //データ受信
                ReceivePixelData(rowLength, columnLength);
                //非同期に受信した画像を描画
                ImgDisplay.AsyncDraw(rowLength, columnLength, receiveData, mask);
                //非同期にLGN細胞を描画
                AsyncMakeBMP(receiveData);
                //単純細胞/複雑細胞の応答強度受信
                if (isLagged)
                {
                    //計測終了
                    measure_cnt++;
                    if(measure_cnt == 3)
                    {
                        sw.Stop();
                        textBox_FrameRate.Text = (1 / (double.Parse(sw.ElapsedMilliseconds.ToString()) * 0.001 / (2 * measure_cnt))).ToString();
                        sw.Reset();
                        measure_cnt = 0;
                    }
                    byte[] NDS_NL_temp = Receive_NDS(39, 23, Num_RF);
                    byte[] NDS_L_temp = Receive_NDS(39, 23, Num_RF);
                    byte[] DSC_temp = Receive_DSC(31, 23, Num_RF);
                    byte[] MDC_temp = Receive_MDC(31, 31, Num_RF);
                    //非同期にNDS/DSCの応答を描画
                    AsyncMakeBMP_Cell(NDS_NL_temp, NDS_L_temp, DSC_temp, MDC_temp);
                    //計測開始
                    sw.Start();
                }
                //遅延あり/なしの反転
                isLagged = !isLagged;
                //停止フラグの確認
                if (haltFlag)
                {
                    ReturnFlag = true;
                    haltFlag = false;
                    tcp.Close();
                    return;
                }
                
            }
        }


        //画像データの受信
        private void ReceivePixelData(int rowLength, int columnLength)
        {
            int dataSize = rowLength * columnLength;    //受信するデータサイズ
            int offset = 0;     //受信したデータをファイルに書き込む際のオフセット
            //データ受信
            while (true)
            {
                dataSize -= receiveSize;
                offset += receiveSize;
                receiveSize = ns.Read(receiveData, offset, dataSize);
                //受信したデータサイズが指定したサイズと一致したらループを抜ける
                if (receiveSize == dataSize)
                {
                    receiveSize = 0;
                    return;
                }
                if (haltFlag)
                {
                    return;
                }
                Application.DoEvents();
            }
        }


        /// <summary>
        /// Receive Cell Response ボタンのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReceiveCell_Click(object sender, EventArgs e)
        {
            CellResponse0 = new CellResponse(0);
            CellResponse0.Show();
            //接続状況のチェック
            if (!tcp.Connected)
            {
                MessageBox.Show("サーバーと未接続です");
                return;
            }
            //FPGAへトリガー送信
            sendData.Add(0x05);
            ns.Write(sendData.ToArray(), 0, sendData.Count);
            sendData.Clear();
            //計測開始
            sw.Start();
            //データ受信
            byte[] NDS_NL_temp = Receive_NDS(39, 23, Num_RF);
            byte[] NDS_L_temp = Receive_NDS(39, 23, Num_RF);
            byte[] DSC_temp = Receive_DSC(31, 23, Num_RF);
            byte[] MDC_temp = Receive_MDC(31, 31, Num_RF);
           
            //配列を結合
            List<int> NDS_NL = CellResponse0.Bind(NDS_NL_temp, 2);
            List<int> NDS_L = CellResponse0.Bind(NDS_L_temp, 2);
            List<int> DSC = CellResponse0.Bind(DSC_temp, 3);
            List<int> MDC = CellResponse0.Bind(MDC_temp, 4);
            //ビットマップ変換
             List<Image> NDS_NL_BMP = CellResponse0.Convert_BMP_NDS_ALL(NDS_NL, 897, Num_RF);
             List<Image> NDS_L_BMP = CellResponse0.Convert_BMP_NDS_ALL(NDS_L, 897, Num_RF);
             List<Image> DSC_BMP = CellResponse0.Convert_BMP_DSC_ALL(DSC, 713, Num_RF);
             List<Image> MDC_BMP = CellResponse0.Convert_BMP_MDC_ALL(MDC, 961, Num_RF);
            //描画処理
            CellResponse0.DrawRefresh(NDS_NL_BMP[0], NDS_L_BMP[0], DSC_BMP[0], MDC_BMP[0]);
            //計測終了
            sw.Stop();
            textBox_FrameRate.Text = (sw.ElapsedMilliseconds).ToString();
            sw.Reset();
        }


        /// <summary>
        /// 非同期にNDS/DSCの応答強度を表示するビットマップ配列作成
        /// </summary>
        /// <param name="NDS_NL_temp"></param>
        /// <param name="NDS_L_temp"></param>
        /// <param name="DSC_temp"></param>
        private async void AsyncMakeBMP_Cell(byte[] NDS_NL_temp, byte[] NDS_L_temp, byte[] DSC_temp, byte[] MDC_temp)
        {
            await Task.Run(() =>
            {
                List<int> NDS_NL = CellResponse0.Bind(NDS_NL_temp, 2);
                List<int> NDS_L = CellResponse0.Bind(NDS_L_temp, 2);
                List<int> DSC = CellResponse0.Bind(DSC_temp, 3);
                List<int> MDC = CellResponse0.Bind(MDC_temp, 4);
                NDS_NL_BMP = CellResponse0.Convert_BMP_NDS_ALL(NDS_NL, 897, Num_RF);
                NDS_L_BMP = CellResponse0.Convert_BMP_NDS_ALL(NDS_L, 897, Num_RF);
                DSC_BMP = CellResponse0.Convert_BMP_DSC_ALL(DSC, 713, Num_RF);
                MDC_BMP = CellResponse0.Convert_BMP_MDC_ALL(MDC, 961, Num_RF);
                DrawFlag_Cell = true;
            });
        }

         /// <summary>
         /// 単純細胞データ受信
         /// </summary>
         /// <param name="width"></param>
         /// <param name="height"></param>
         /// <returns></returns>
         private byte[] Receive_NDS(int width, int height, int N_RF)
         {
             int dataSize = width * height * 2 * N_RF;    //受信するデータサイズ
             int receiveSize = 0;
             byte[] receiveData = new byte[dataSize];       //受信したデータを格納する配列
             int offset = 0;     //受信したデータをファイルに書き込む際のオフセット
             //データ受信
             while (true)
             {
                 dataSize -= receiveSize;
                 offset += receiveSize;
                 receiveSize = ns.Read(receiveData, offset, dataSize);
                 //受信したデータサイズが指定したサイズと一致したらループを抜ける
                 if (receiveSize == dataSize)
                 {
                     return receiveData;
                 }
             }
         }

         /// <summary>
         /// 複雑細胞データ受信
         /// </summary>
         /// <param name="width"></param>
         /// <param name="height"></param>
         /// <returns></returns>
         private byte[] Receive_DSC(int width, int height, int N_RF)
         {
             int dataSize = width * height * 3 * N_RF;    //受信するデータサイズ
             int receiveSize = 0;
             byte[] receiveData = new byte[dataSize];       //受信したデータを格納する配列
             int offset = 0;     //受信したデータをファイルに書き込む際のオフセット
             //データ受信
             while (true)
             {
                 dataSize -= receiveSize;
                 offset += receiveSize;
                 receiveSize = ns.Read(receiveData, offset, dataSize);
                 //受信したデータサイズが指定したサイズと一致したらループを抜ける
                 if (receiveSize == dataSize)
                 {
                     return receiveData;
                 }
             }
         }


        /// <summary>
        /// 運動検出細胞データ受信
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private byte[] Receive_MDC(int width, int height, int N_RF)
        {
            int dataSize = width * height * 4 * N_RF;    //受信するデータサイズ
            int receiveSize = 0;
            byte[] receiveData = new byte[dataSize];       //受信したデータを格納する配列
            int offset = 0;     //受信したデータをファイルに書き込む際のオフセット
            //データ受信
            while (true)
            {
                dataSize -= receiveSize;
                offset += receiveSize;
                receiveSize = ns.Read(receiveData, offset, dataSize);
                //受信したデータサイズが指定したサイズと一致したらループを抜ける
                if (receiveSize == dataSize)
                {
                    return receiveData;
                }
            }
        }


        /// <summary>
        /// Receive Projected Dataボタンのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReceiveProjected_Click(object sender, EventArgs e)
        {
            //接続状況のチェック
            if (!tcp.Connected)
            {
                MessageBox.Show("サーバーと未接続です");
                return;
            }
            //FPGAへトリガー送信
            sendData.Add(0x06);
            ns.Write(sendData.ToArray(), 0, sendData.Count);
            sendData.Clear();
            //データ受信
            byte[] ReceiveData_temp = Receive(761);
            //配列を分配
            byte[] Xlgn_temp = new byte[761 * 2];
            byte[] Ylgn_temp = new byte[761 * 2];
            byte[] LGN = new byte[761];
            Decompose(ReceiveData_temp, ref Xlgn_temp, ref Ylgn_temp, ref LGN);
            //値の変換
            List<string> Xlgn = Bind(Xlgn_temp, 2);
            List<string> Ylgn = Bind(Ylgn_temp, 2);
            //ファイル出力
            ExportFile(Xlgn, Ylgn, LGN);
        }

        /// <summary>
        /// 単純細胞データ受信
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private byte[] Receive(int N_Cell)
        {
            int dataSize = (N_Cell * 2) * 2 + N_Cell;    //受信するデータのバイト長
            int receiveSize = 0;
            byte[] receiveData = new byte[dataSize];       //受信したデータを格納する配列
            int offset = 0;     //受信したデータをファイルに書き込む際のオフセット
            //データ受信
            while (true)
            {
                dataSize -= receiveSize;
                offset += receiveSize;
                receiveSize = ns.Read(receiveData, offset, dataSize);
                //受信したデータサイズが指定したサイズと一致したらループを抜ける
                if (receiveSize == dataSize)
                {
                    return receiveData;
                }
            }
        }

        /// <summary>
        /// 受信した配列を分解
        /// </summary>
        /// <param name="ReceiveData"></param>
        /// <param name="Xlgn_temp"></param>
        /// <param name="Ylgn_temp"></param>
        /// <param name="LGN"></param>
        private void Decompose(byte[] ReceiveData, ref byte[] Xlgn_temp, ref byte[] Ylgn_temp, ref byte[] LGN)
        {
            int count_receive = 0;
            int count_x = 0;
            int count_y = 0;
            int count_lgn = 0;
            for (int i = 0; i < 761; i++)
            {
                for (int j = 0; j < 2; j++)
                    Xlgn_temp[count_x++] = ReceiveData[count_receive++];
                for (int j = 0; j < 2; j++)
                    Ylgn_temp[count_y++] = ReceiveData[count_receive++];
                LGN[count_lgn++] = ReceiveData[count_receive++];
            }
        }

        /// <summary>
        /// 配列の値を2進数に変換してバインド
        /// </summary>
        /// <param name="array"></param>
        /// <param name="N_bind"></param>
        /// <returns></returns>
        private List<string> Bind(byte[] array, int N_bind)
        {
            int arraySize = array.Count();
            string pre_bind;
            string post_bind = "";
            List<string> bind_array = new List<string>();
            int x = 0;
            for (int i = 0; i < arraySize;)
            {
                post_bind = "";
                for (int j = 0; j < N_bind; j++)
                {
                    pre_bind = Convert.ToString(array[i], 2);
                    post_bind = pre_bind.PadLeft(8, '0') + post_bind;
                    i++;
                }
                bind_array.Add(post_bind);
            }
            return bind_array;
        }

        /// <summary>
        /// 切り出し結果をファイル出力
        /// </summary>
        /// <param name="Xlgn"></param>
        /// <param name="Ylgn"></param>
        /// <param name="LGN"></param>
        private void ExportFile(List<string> Xlgn, List<string> Ylgn, byte[] LGN)
        {
            StreamWriter sw1 = new StreamWriter("Xlgn.dat");
            StreamWriter sw2 = new StreamWriter("Ylgn.dat");
            StreamWriter sw3 = new StreamWriter("LGN.dat");
            for (int i = 0; i < Xlgn.Count; i++)
            {
                sw1.WriteLine(Xlgn[i]);
                sw2.WriteLine(Ylgn[i]);
                sw3.WriteLine(LGN[i]);
            }
            sw1.Close();
            sw2.Close();
            sw3.Close();
            MessageBox.Show("Complete Export File!");
        }


        /// <summary>
        /// Haltボタンのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHalt_Click(object sender, EventArgs e)
         {
             sendData.Add(0x04);
             ns.Write(sendData.ToArray(), 0, sendData.Count);
             sendData.Clear();
             haltFlag = true;
         }


         /// <summary>
         /// Resetボタンのイベントハンドラ
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         private void buttonReset_Click(object sender, EventArgs e)
         {
             ImgDisplay.Close();
             LGN_RF0.Close();
             LGN_RF1.Close();
             LGN_RF2.Close();
             LGN_RF3.Close();
             LGN_RF4.Close();
             LGN_RF5.Close();
             LGN_RF6.Close();
         }


         /// <summary>
         /// Exportボタンのイベントハンドラ
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         private void buttonExport_Click(object sender, EventArgs e)
         {
             ImgDisplay.ExportBMP();
             LGN_RF0.ExportBMP(0);
             LGN_RF1.ExportBMP(1);
             LGN_RF2.ExportBMP(2);
             LGN_RF3.ExportBMP(3);
             LGN_RF4.ExportBMP(4);
             LGN_RF5.ExportBMP(5);
             LGN_RF6.ExportBMP(6);
             MessageBox.Show("Complete Export File!");
         }



         /// <summary>
         /// 入力された値が有効かの検証
         /// </summary>
         /// <returns></returns>
         private bool IsDataValid()
         {
             //フレーム入力の検証
             try
             {
                 int flame = int.Parse(textFlame.Text);
                 if (flame <= 0 || flame > 64)
                 {
                     MessageBox.Show("フレーム入力が正しくありません");
                     return false;
                 }
             }
             catch (Exception exc)
             {
                 MessageBox.Show(exc.Message);
                 return false;
             }
             return true;
         }



         byte[] DATA_img;
         /// <summary>
         /// デバッグ用ボタンのイベントハンドラ
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         private void buttonDebug_Click(object sender, EventArgs e)
         {
            int x = 10;
            DATA_img = improc.ToByte(0);
             LGN_Draw(DATA_img);

           /*  ReturnFlag = false;
             AsyncRefresh();
             sw.Start();
             for (int i = 0; i < 100; i++)
             {
                 DATA_img = improc.ToByte((x % 2) + 13);
                 AsyncMakeBMP(DATA_img);
                 ImgDisplay.AsyncDraw(1280, 960, DATA_img, mask);
                 x++;
             }
             sw.Stop();
             MessageBox.Show(sw.ElapsedMilliseconds.ToString());
             sw.Reset();
             Thread.Sleep(100);
             ReturnFlag = true;*/
        }



        /// <summary>
        /// 非同期にBMPデータを生成
        /// </summary>
        /// <param name="RetinalCell"></param>
        private async void AsyncMakeBMP(byte[] RetinalCell)
        {
            LGN_Projector LGN_Projector = new LGN_Projector();
            await Task.Run(() =>
            {
                bmp_RF0 = LGN_Projector.Retina2LGN(0, Ximg_RF0, Yimg_RF0, Xlgn_RF0, Ylgn_RF0, RetinalCell);
                bmp_RF1 = LGN_Projector.Retina2LGN(1, Ximg_RF1, Yimg_RF1, Xlgn_RF1, Ylgn_RF1, RetinalCell);
                bmp_RF2 = LGN_Projector.Retina2LGN(2, Ximg_RF2, Yimg_RF2, Xlgn_RF2, Ylgn_RF2, RetinalCell);
                bmp_RF3 = LGN_Projector.Retina2LGN(3, Ximg_RF3, Yimg_RF3, Xlgn_RF3, Ylgn_RF3, RetinalCell);
                bmp_RF4 = LGN_Projector.Retina2LGN(4, Ximg_RF4, Yimg_RF4, Xlgn_RF4, Ylgn_RF4, RetinalCell);
                bmp_RF5 = LGN_Projector.Retina2LGN(5, Ximg_RF5, Yimg_RF5, Xlgn_RF5, Ylgn_RF5, RetinalCell);
                bmp_RF6 = LGN_Projector.Retina2LGN(6, Ximg_RF6, Yimg_RF6, Xlgn_RF6, Ylgn_RF6, RetinalCell);
                DrawFlag_IMG = true;
            });
        }



        /// <summary>
        /// 非同期に描画のリフレッシュを実行
        /// </summary>
        private async void AsyncRefresh1()
        {
            await Task.Run(() =>
            {
                ImgDisplay = new ImgDisplayWindow();
                LGN_RF0 = new LGN_View(0);
                LGN_RF1 = new LGN_View(1);
                LGN_RF2 = new LGN_View(2);
                LGN_RF3 = new LGN_View(3);
                LGN_RF4 = new LGN_View(4);
                LGN_RF5 = new LGN_View(5);
                LGN_RF6 = new LGN_View(6);
                ImgDisplay.Show();
                LGN_RF0.Show();
                LGN_RF1.Show();
                LGN_RF2.Show();
                LGN_RF3.Show();
                LGN_RF4.Show();
                LGN_RF5.Show();
                LGN_RF6.Show();
                while (true)
                {
                    if (DrawFlag_IMG)
                    {
                        ImgDisplay.ImgRefresh();
                        LGN_RF0.DrawRefresh(bmp_RF0);
                        LGN_RF1.DrawRefresh(bmp_RF1);
                        LGN_RF2.DrawRefresh(bmp_RF2);
                        LGN_RF3.DrawRefresh(bmp_RF3);
                        LGN_RF4.DrawRefresh(bmp_RF4);
                        LGN_RF5.DrawRefresh(bmp_RF5);
                        LGN_RF6.DrawRefresh(bmp_RF6);
                        DrawFlag_IMG = false;
                    }
                    if (ReturnFlag)
                    {
                        ImgDisplay.Close();
                        LGN_RF0.Close();
                        LGN_RF1.Close();
                        LGN_RF2.Close();
                        LGN_RF3.Close();
                        LGN_RF4.Close();
                        LGN_RF5.Close();
                        LGN_RF6.Close();
                        ReturnFlag = false;
                        return;
                    }
                }
            });
        }

        /// <summary>
        /// 非同期に細胞応答の描画を実行
        /// </summary>
        private async void AsyncRefresh2()
        {
            await Task.Run(() =>
            {
                CellResponse0 = new CellResponse(0);
                CellResponse1 = new CellResponse(1);
                CellResponse2 = new CellResponse(2);
                CellResponse3 = new CellResponse(3);
                CellResponse4 = new CellResponse(4);
                CellResponse5 = new CellResponse(5);
                CellResponse6 = new CellResponse(6);
                CellResponse0.Show();
                CellResponse1.Show();
                CellResponse2.Show();
                CellResponse3.Show();
                CellResponse4.Show();
                CellResponse5.Show();
                CellResponse6.Show();
                while (true)
                {
                    if (ReturnFlag)
                    {
                        CellResponse0.Close();
                        CellResponse1.Close();
                        CellResponse2.Close();
                        CellResponse3.Close();
                        return;
                    }
                    else if (DrawFlag_Cell)
                    {
                        CellResponse0.DrawRefresh(NDS_NL_BMP[0], NDS_L_BMP[0], DSC_BMP[0], MDC_BMP[0]);
                        CellResponse1.DrawRefresh(NDS_NL_BMP[1], NDS_L_BMP[1], DSC_BMP[1], MDC_BMP[1]);
                        CellResponse2.DrawRefresh(NDS_NL_BMP[2], NDS_L_BMP[2], DSC_BMP[2], MDC_BMP[2]);
                        CellResponse3.DrawRefresh(NDS_NL_BMP[3], NDS_L_BMP[3], DSC_BMP[3], MDC_BMP[3]);
                        CellResponse4.DrawRefresh(NDS_NL_BMP[4], NDS_L_BMP[4], DSC_BMP[4], MDC_BMP[4]);
                        CellResponse5.DrawRefresh(NDS_NL_BMP[5], NDS_L_BMP[5], DSC_BMP[5], MDC_BMP[5]);
                        CellResponse6.DrawRefresh(NDS_NL_BMP[6], NDS_L_BMP[6], DSC_BMP[6], MDC_BMP[6]);
                        DrawFlag_Cell = false;
                    }
                }
            });
        }


        /// <summary>
        /// すべてのLGN細胞とカメラ画像を描画
        /// </summary>
        /// <param name="RetinalCell"></param>
        private void LGN_Draw(byte[] RetinalCell)
        {
            //LGN細胞への写像を計算するインスタンスを受容野の数だけ作成
            ImgDisplay = new ImgDisplayWindow();
            LGN_RF0 = new LGN_View(0);
            LGN_RF1 = new LGN_View(1);
            LGN_RF2 = new LGN_View(2);
            LGN_RF3 = new LGN_View(3);
            LGN_RF4 = new LGN_View(4);
            LGN_RF5 = new LGN_View(5);
            LGN_RF6 = new LGN_View(6);
            LGN_Projector LGN_Projector = new LGN_Projector();
            ImgDisplay.Show();
            LGN_RF0.Show();
            LGN_RF1.Show();
            LGN_RF2.Show();
            LGN_RF3.Show();
            LGN_RF4.Show();
            LGN_RF5.Show();
            LGN_RF6.Show();
            //受容野ごとにLGNへの射影結果を描画
            LGN_Projector.Dummy(0, RetinalCell, Ximg_RF0, Yimg_RF0, Xlgn_RF0, Ylgn_RF0, LGN_RF0);
            LGN_Projector.Dummy(1, RetinalCell, Ximg_RF1, Yimg_RF1, Xlgn_RF1, Ylgn_RF1, LGN_RF1);
            LGN_Projector.Dummy(2, RetinalCell, Ximg_RF2, Yimg_RF2, Xlgn_RF2, Ylgn_RF2, LGN_RF2);
            LGN_Projector.Dummy(3, RetinalCell, Ximg_RF3, Yimg_RF3, Xlgn_RF3, Ylgn_RF3, LGN_RF3);
            LGN_Projector.Dummy(4, RetinalCell, Ximg_RF4, Yimg_RF4, Xlgn_RF4, Ylgn_RF4, LGN_RF4);
            LGN_Projector.Dummy(5, RetinalCell, Ximg_RF5, Yimg_RF5, Xlgn_RF5, Ylgn_RF5, LGN_RF5);
            LGN_Projector.Dummy(6, RetinalCell, Ximg_RF6, Yimg_RF6, Xlgn_RF6, Ylgn_RF6, LGN_RF6);
            ImgDisplay.DrawMask(improc.ToImage(RetinalCell, 1280, 960), mask);
        }



        /// <summary>
        /// 各種描画をリフレッシュ
        /// </summary>
        private void DrawRefresh()
        {
           // ImgDisplay.ImgRefresh();
            LGN_RF0.Refresh();
            LGN_RF1.Refresh();
            LGN_RF2.Refresh();
            LGN_RF3.Refresh();
            LGN_RF4.Refresh();
            LGN_RF5.Refresh();
            LGN_RF6.Refresh();
        }


    }
}
