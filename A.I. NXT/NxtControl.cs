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

            while (true)
            {
                byte[] NXTMessage = { 0x00, 0x13, 0x0C, 0x00, 0x01 };
                Byte[] MessageLength = { 0x00, 0x00 };

                MessageLength[0] = (byte)NXTMessage.Length;
                BluetoothConnection.Write(MessageLength, 0, MessageLength.Length);
                BluetoothConnection.Write(NXTMessage, 0, NXTMessage.Length);
                Thread.Sleep(75);
               }
        }

        /// <summary>
        /// Reads the mailboxes from the second NXT.
        /// Also keeps communication alive.
        /// </summary>
        void ReadMailbox2()
        {
            while (true)
            {
                byte[] NXTMessage = { 0x00, 0x13, 0x0C, 0x00, 0x01 };
                Byte[] MessageLength = { 0x00, 0x00 };

                MessageLength[0] = (byte)NXTMessage.Length;
                BluetoothConnection2.Write(MessageLength, 0, MessageLength.Length);
                BluetoothConnection2.Write(NXTMessage, 0, NXTMessage.Length);
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
                        //UI.NXTTextbox = "Ready."
                        NXTReady = true;
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
                   if(CurrByte == 0xFF)
                   {
                      //UI.NXTTextbox2 = "Executing command..."
                   }
                   if (CurrByte == 0xFA)
                   {
                       //UI.NXTTextbox2 = "Ready."
                       NXT2Ready = true;
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
            bool negative= false;
            int residuary;
            byte[] NXTMsg = new byte[17];
            byte[] NXT2Msg = new byte[17];
            int i;
            // m2 motor értékeinek feldolgozása
            
            for (i = 16; i > 11; i--)
            {
                NXTMsg[i] = (byte)0;
            }
            NXTMsg[i] = (byte)0;
            i--;
            if (m2 < 0)
            {
                negative=true;
                m2=m2*-1;
            }
            {
                for (; i > 5; i--)
                {
                    if (m2 > 0)
                    {
                        residuary = m2 % 10;
                        NXTMsg[i] = (byte)residuary;

                    }
                    else
                                 
                        {
                            NXTMsg[i] = (byte)0;
                        }
                    
                    m2 = m2 / 10;
                }
            }
            if (negative == true)
            {
                NXTMsg[6] = (byte)'-';
                negative = false;
            }
            
            NXTMsg[i] = (byte)0;
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
                    NXTMsg[i] = (byte)residuary;
                }
                else
                {         
                   
                   NXTMsg[i] = (byte)0;
                    
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
                NXT2Msg[i] = (byte)0;
            }
            NXT2Msg[i] = (byte)0;
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
                    NXT2Msg[i] = (byte)residuary;
                }
                else
                {
                    if (negative == true)
                    {
                        NXT2Msg[i] = (byte)'-';
                        negative = false;
                    }
                    else
                    {
                        NXT2Msg[i] = (byte)0;
                    }
                }
                m4 = m4 / 10;
            }
            if (negative == true)
            {
                NXT2Msg[6] = (byte)'-';
                negative = false;
            }
         // az m3 motor feldolgozása
            if (m3 < 0)
            {
                negative = true;
                m3 = m3 * -1;
            }
            for (; i > 5; i--)
            {
                if (m3 > 0)
                {
                    residuary = m3 % 10;
                    NXT2Msg[i] = (byte)residuary;
                }
                else
                {
                    if (negative == true)
                    {
                        NXT2Msg[i] = (byte)'-';
                        negative = false;
                    }
                    else
                    {
                        NXT2Msg[i] = (byte)0;
                    }
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
            /// <summary>
            /// Sends two preformatted strings to the NXT devices
            /// </summary>
            /// <param name="NXT1Msg">Message aimed for NXT1</param>
            /// <param name="NXT2Msg">Message aimed for NXT2</param>

            byte[] NXT1MagnetMsg = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x07, 0x05, 0x00 };
            byte[] NXT2MagnetMsg = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            if (TurnMagnetTo == true)
            {
                SendMessage(NXT1MagnetMsg, NXT2MagnetMsg);
            }
        }
        void SendMessage(byte[] NXT1Msg, byte[] NXT2Msg)
        {
            NXT2Ready = false;
            NXTReady = false;
            Byte[] NXT1MsgLen = { 0x00, 0x00 };
            NXT1MsgLen[0] = (byte)NXT1Msg.Length;

            Byte[] NXT2MsgLen = { 0x00, 0x00 };
            NXT2MsgLen[0] = (byte)NXT2Msg.Length;

            BluetoothConnection.Write(NXT1MsgLen, 0, NXT1MsgLen.Length);
            BluetoothConnection.Write(NXT1Msg, 0, NXT1Msg.Length);

            BluetoothConnection2.Write(NXT2MsgLen, 0, NXT2MsgLen.Length);
            BluetoothConnection2.Write(NXT2Msg, 0, NXT2Msg.Length);
            while ((NXTReady == false) && (NXT2Ready == false))
            {

                Thread.Sleep(250);
            }
        }


        
    }

}
