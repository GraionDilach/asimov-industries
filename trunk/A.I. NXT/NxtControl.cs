using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace A.I.NXT
{
    public class NxtControl 
    {
        bool NXTReady;
        bool NXT2Ready;

        Thread NXTListener;
        Thread NXT2Listener;
        SerialPort BluetoothConnection = new SerialPort();
        SerialPort BluetoothConnection2 = new SerialPort();

        public NxtControl(string NXTport, string NXT2port)
        {     
                          
                BluetoothConnection.PortName = NXTport;
                BluetoothConnection.Open();
                NXTReady = true;
               /* BluetoothConnection2.PortName = NXT2port;
                BluetoothConnection2.Open();
                NXT2Ready = true;*/
                NXTListener = new Thread(ReadMailbox);
                NXT2Listener = new Thread(ReadMailbox2);
                NXTListener.Start();
                NXT2Listener.Start();
          
               
        }

        void ReadMailbox()
        {

            while (true)
            {
                // ff üzenet megérkezik
                // fa process vége
                int length = BluetoothConnection.ReadByte() + 256 * BluetoothConnection.ReadByte();

               }
        }
        void ReadMailbox2()
        {
            while (true)
            {

            }

        }

        void GetMessageFromNXT(object s, SerialDataReceivedEventArgs e)
        {
            int length = BluetoothConnection2.ReadByte() + 256 * BluetoothConnection2.ReadByte();

            // write out only the ACKs
            for (int i = 0; i < length; i++)
            {
                if (i == 5)
                {
                   // ha megkapja az ack-t nem kéne neki egy bool ?
                }
                else
                {
                    BluetoothConnection2.ReadByte();
                }
            } 
            BluetoothConnection2.DiscardInBuffer();

        }

        public void StartOperation(int m1, int m2, int m3, int m4)
        {
            int[] NXT1Msgint = new int[] {m1,m2,0};
            int[] NXT2Msgint = new int[] {m3,m4,0};
            byte[] NXTMsg = new byte [3];
            byte[] NXT2Msg = new byte[3];
            for (int i = 0; i < 3; i++)
            {
                NXTMsg[i] = (byte)NXT1Msgint[i];
            }
            for (int i = 0; i < 3; i++)
            {
                // 1x17 elem

                NXT2Msg[i] = (byte)NXT2Msgint[i];
            }

            SendMessage(NXTMsg, NXT2Msg);

        }
        public void MagnetControl(bool TurnMagnetTo);
        void SendMessage(byte[] NXT1Msg, byte[] NXT2Msg)
        {
            NXT2Ready = false;
            NXTReady = false;
            BluetoothConnection.Write(NXT1Msg, 0, NXT1Msg.Length);
            BluetoothConnection2.Write(NXT2Msg, 0, NXT2Msg.Length);
            while ((NXTReady == false) && (NXT2Ready == false))
            {

                Thread.Sleep(250);
            }
        }


        
    }

}
