using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace A.I.NXT
{
    /// <summary>
    /// Class for handling communication between the PC and NXT.
    /// It should maintain the syncs between the three
    /// devices, as in the 2 NXT bricks and the PC.
    /// </summary>
    public class NxtControl
    {
        bool NXTReady;  ///<boolean for NXT status, true, if ready, false, if busy
        bool NXT2Ready; ///<boolean for NXT2 status, true, if ready, false, if busy
        bool NXTConnection;                ///<boolean for NXT connection, false if disconnected
        bool NXT2Connection;                ///<boolean for NXT2 connection, false if disconnected

        byte[] NXTMessage;      ///<buffer where the next message for the NXT to be sent will be placed
        byte[] NXT2Message;     ///<buffer where the next message for the NXT2 to be sent will be placed

        Thread NXTListener;     ///<background thread for pinging and mailbox reading, NXT1
        Thread NXT2Listener;    ///<background thread for pinging and mailbox reading, NXT2
        SerialPort BluetoothConnection = new SerialPort();  ///<Connection for NXT1
        SerialPort BluetoothConnection2 = new SerialPort(); ///<Connection for NXT2

        /// <summary>
        /// Constructor for the NXTControl class
        /// </summary>
        /// <param name="NXTport">COM port of the first NXT</param>
        /// <param name="NXT2port">COM port of the second NXT</param>
        public NxtControl(string NXTport, string NXT2port)
        {
            //to-do: add handling for UI
            BluetoothConnection.PortName = NXTport;
            BluetoothConnection.Open();
            NXTReady = true;
            BluetoothConnection2.PortName = NXT2port;
            BluetoothConnection2.Open();
            NXT2Ready = true;
            NXTConnection = true;
            NXT2Connection = true;
            NXTMessage = new byte[] { };
            NXT2Message = new byte[] { };
            BluetoothConnection.DataReceived += GetMessageFromNXT;
            BluetoothConnection2.DataReceived += GetMessageFromNXT2;
            NXTListener = new Thread(ReadMailbox);
            NXT2Listener = new Thread(ReadMailbox2);
            NXTListener.Start();
            NXT2Listener.Start();



        }

        /// <summary>
        /// Reads the mailboxes from the first NXT.
        /// Also keeps communication alive.
        /// </summary>
        void ReadMailbox()
        {

            while (NXTConnection)
            {
                byte[] _NXTMessage;
                Byte[] MessageLength = { 0x00, 0x00 };
                lock (this)
                {
                    if (NXTMessage.Length != 0)
                    {
                        _NXTMessage = new byte[NXTMessage.Length];
                        NXTMessage.CopyTo(_NXTMessage, 0);
                        NXTMessage = new byte[] { };
                    }
                    else
                    {
                        _NXTMessage = new byte[] { 0x00, 0x13, 0x0B, 0x00, 0x01 };
                    }

                }

                MessageLength[0] = (byte)_NXTMessage.Length;
                BluetoothConnection.Write(MessageLength, 0, MessageLength.Length);
                BluetoothConnection.Write(_NXTMessage, 0, _NXTMessage.Length);
                Thread.Sleep(75);
            }
        }

        /// <summary>
        /// Reads the mailboxes from the second NXT.
        /// Also keeps communication alive.
        /// </summary>
        void ReadMailbox2()
        {
            while (NXT2Connection)
            {
                byte[] _NXTMessage;
                Byte[] MessageLength = { 0x00, 0x00 };
                lock (this)
                {
                    if (NXT2Message.Length != 0)
                    {
                        _NXTMessage = new byte[NXT2Message.Length];
                        NXT2Message.CopyTo(_NXTMessage, 0);
                        NXT2Message = new byte[] { };
                    }
                    else
                    {
                        _NXTMessage = new byte[] { 0x00, 0x13, 0x0B, 0x00, 0x01 };
                    }

                }

                MessageLength[0] = (byte)_NXTMessage.Length;
                BluetoothConnection2.Write(MessageLength, 0, MessageLength.Length);
                BluetoothConnection2.Write(_NXTMessage, 0, _NXTMessage.Length);
                Thread.Sleep(75);
            }

        }

        /// <summary>
        /// Processes the data sent from the first NXT.
        /// </summary>
        void GetMessageFromNXT(object s, SerialDataReceivedEventArgs e)
        {
            int length = BluetoothConnection.ReadByte() + 256 * BluetoothConnection.ReadByte();

            // write out only the ACKs
            // i=2 is the mailbox index
            for (int i = 0; i < length; i++)
            {
                byte CurrByte = (byte)BluetoothConnection.ReadByte();
                if (i == 5)
                {
                    if (CurrByte == 0xFF)
                    {
                        //UI.NXTTextbox = "Executing command..."
                    }

                    if (CurrByte == 0xFA)
                    {
                        lock (this)
                        {
                            //UI.NXTTextbox = "Ready."
                            NXTReady = true;
                        }
                    }

                }
            }
            BluetoothConnection.DiscardInBuffer();

        }

        /// <summary>
        /// Processes the data sent from the second NXT.
        /// </summary>
        void GetMessageFromNXT2(object s, SerialDataReceivedEventArgs e)
        {
            int length = BluetoothConnection2.ReadByte() + 256 * BluetoothConnection2.ReadByte();

            // write out only the ACKs
            for (int i = 0; i < length; i++)
            {
                byte CurrByte = (byte)BluetoothConnection2.ReadByte();
                if (i == 5)
                {
                    if (CurrByte == 0xFF)
                    {
                        //UI.NXTTextbox2 = "Executing command..."
                    }
                    if (CurrByte == 0xFA)
                    {
                        lock (this)
                        {
                            //UI.NXT2Textbox = "Ready."
                            NXT2Ready = true;
                        }
                    }

                }
            }
            BluetoothConnection2.DiscardInBuffer();

        }

        /// <summary>
        /// Sends a message to the robot to turn the 4 motors with a specified amount
        /// </summary>
        /// <param name="m1">Amount of turns Motor 1 shall do (in degree)</param>
        /// <param name="m2">Amount of turns Motor 2 shall do (in degree)</param>
        /// <param name="m3">Amount of turns Motor 3 shall do (in degree)</param>
        /// <param name="m4">Amount of turns Motor 4 shall do (in degree)</param>
        public void StartOperation(int m1, int m2, int m3, int m4)
        {
            bool negative = false;
            int residuary;
            byte[] NXTMsg = new byte[17];
            byte[] NXT2Msg = new byte[17];
            int i;
            // m2 motor értékeinek feldolgozása

            for (i = 16; i > 11; i--)
            {
                NXTMsg[i] = (byte)0x30;
            }
            NXTMsg[i] = (byte)0x30;
            i--;
            if (m2 < 0)
            {
                negative = true;
                m2 = m2 * -1;
            }
            {
                for (; i > 5; i--)
                {
                    if (m2 > 0)
                    {
                        residuary = m2 % 10;
                        NXTMsg[i] = (byte)(residuary + 0x30);

                    }
                    else
                    {
                        NXTMsg[i] = (byte)0x30;
                    }

                    m2 = m2 / 10;
                }
            }
            if (negative == true)
            {
                NXTMsg[6] = (byte)'-';
                negative = false;
            }

            NXTMsg[i] = (byte)0x30;
            i--;
            // az m1 motor feldolgozása
            if (m1 < 0)
            {
                negative = true;
                m1 = m1 * -1;
            }
            for (; i >= 0; i--)
            {
                if (m1 > 0)
                {
                    residuary = m1 % 10;
                    NXTMsg[i] = (byte)(residuary + 0x30);
                }
                else
                {

                    NXTMsg[i] = (byte)0x30;

                }
                m1 = m1 / 10;
            }
            if (negative == true)
            {
                NXTMsg[0] = (byte)'-';
                negative = false;
            }


            // m4 motor értékeinek feldolgozása
            for (i = 16; i > 11; i--)
            {
                NXT2Msg[i] = (byte)0x30;
            }
            NXT2Msg[i] = (byte)0x30;
            i--;
            if (m4 < 0)
            {
                negative = true;
                m4 = m4 * -1;
            }
            for (; i > 5; i--)
            {
                if (m4 > 0)
                {
                    residuary = m4 % 10;
                    NXT2Msg[i] = (byte)(residuary + 0x30);
                }
                else
                {
          
                        NXT2Msg[i] = (byte)0x30;
               
                }
                m4 = m4 / 10;
            }
            if (negative == true)
            {
                NXT2Msg[6] = (byte)'-';
                negative = false;
            }
            NXTMsg[i] = (byte)0x30;
            i--;
            // az m3 motor feldolgozása
            if (m3 < 0)
            {
                negative = true;
                m3 = m3 * -1;
            }
            for (; i >= 0; i--)
            {
                if (m3 > 0)
                {
                    residuary = m3 % 10;
                    NXT2Msg[i] = (byte)(residuary + 0x30);
                }
                else
                {
         
                        NXT2Msg[i] = (byte)0x30;
         
                }
                m3 = m3 / 10;
            }
            if (negative == true)
            {
                NXT2Msg[0] = (byte)'-';
                negative = false;
            }
            SendMessage(NXTMsg, NXT2Msg);

        }

        /// <summary>
        /// Public function to control the magnet
        /// 
        /// If the motor function is turned in then
        /// the magnet will be lowered, if it'll be called
        /// with the parameter set to false,
        /// it'll lift the magnet back to the tube
        /// </summary>
        /// <param name="TurnMagnetTo">The parameter for the magnet</param>
        public void MagnetControl(bool TurnMagnetTo)
        {

            byte[] NXT1MagnetMsg = new byte[] { 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x37, 0x35, 0x30 };
            byte[] NXT2MagnetMsg = new byte[] { 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30 };
            if (TurnMagnetTo == true)
            {
                SendMessage(NXT1MagnetMsg, NXT2MagnetMsg);
            }
        }

        /// <summary>
        /// Sends two preformatted strings to the NXT devices
        /// </summary>
        /// <param name="NXT1Msg">Message aimed for NXT1</param>
        /// <param name="NXT2Msg">Message aimed for NXT2</param>
        void SendMessage(byte[] NXT1Msg, byte[] NXT2Msg)
        {
            NXT2Ready = false;
            NXTReady = false;

            lock (this)
            {

                byte datalength = Convert.ToByte(NXT1Msg.Length + 1);
                byte[] NxtHeader = { 0x00, 0x09, 0x00, datalength };
                NXTMessage = new byte[NxtHeader.Length + NXT1Msg.Length + 1];
                NxtHeader.CopyTo(NXTMessage, 0);
                NXT1Msg.CopyTo(NXTMessage, NxtHeader.Length);

                Byte[] NXT1MsgLen = { 0x00, 0x00 };
                NXT1MsgLen[0] = (byte)NXTMessage.Length;

                byte data2length = Convert.ToByte(NXT2Msg.Length + 1);
                byte[] Nxt2Header = { 0x00, 0x09, 0x00, data2length };

                NXT2Message = new byte[Nxt2Header.Length + NXT2Msg.Length + 1];
                Nxt2Header.CopyTo(NXT2Message, 0);
                NXT2Msg.CopyTo(NXT2Message, Nxt2Header.Length);
            }

            while ((NXTReady == false) && (NXT2Ready == false))
            {

                Thread.Sleep(250);
            }
        }

        /// <summary>
        /// This function closes down the Bluetooth connections of the NXTs.
        /// </summary>
        public void DisConnect()
        {
            lock (this)
            {
                NXTConnection = false;
                NXT2Connection = false;
            }
            BluetoothConnection.DataReceived += null;
            BluetoothConnection2.DataReceived += null;
            BluetoothConnection.Close();
            BluetoothConnection2.Close();
        }

    }

}
