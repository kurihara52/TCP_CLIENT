using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPクライアント
{
    class PacketConverter
    {
        public List<byte> To2Byte(int data)
        {
            List<byte> packet = new List<byte>();

            if(0 <= data && data < 65535 * 100)
            {
                string xdata = data.ToString("x4");
                packet.Add(Convert.ToByte(xdata.Substring(0,2), 16));
                packet.Add(Convert.ToByte(xdata.Substring(2, 2), 16));
            }
            else
            {
                MessageBox.Show("範囲外の値が入力されました");
            }

            return packet;
        }

        public byte  FrameAddressIncrementer(byte frameAddr)
        {
            byte AddedFrameAddr = 0;    //加算後のフレームアドレス
            if(frameAddr < 64)
            {
                AddedFrameAddr = frameAddr++;
            }
            else if(frameAddr == 64)
            {
                AddedFrameAddr = 1;
            }
            return AddedFrameAddr;
        }

    }
}
