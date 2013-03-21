using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace nxtBlueTooth
{
    public partial class Form1 : Form
    {
        private
        SerialPort BluetoothConnection= new SerialPort();
        SerialPort BluetoothConnection2 = new SerialPort();
        public Form1()
        {
            InitializeComponent();
        }



        private void GetVersion_Click(object sender, EventArgs e)
        {
            byte[] NxtMessage = {0x01, 0x88 };
            NXTSendCommandAndGetReply(NxtMessage);
        }

        private void GetInfo_Click(object sender, EventArgs e)
        {
            byte[] NxtMessage = {0x01, 0x9B };
            NXTSendCommandAndGetReply(NxtMessage);
        }
        private void ReadMailBox_Click(object sender, EventArgs e)
        {
            byte[] NxtMessage = {0x00, 0x13, 0x00, 0x00,0x01 };
            NxtMessage[2] = (byte)(this.numericUpDownMailBoxNbr.Value-1+10);
            NXTSendCommandAndGetReply(NxtMessage);
        }
        private void WriteMailBoxBool_Click(object sender, EventArgs e)
        {
            byte[] NxtMessage = {0x00, 0x09, 0x00, 0x02, 0x00, 0x00 };
            NxtMessage[2] = (byte)(this.numericUpDownMailBoxNbr.Value-1);
            NxtMessage[4] = (byte)this.numericUpDownBool.Value;
            NXTSendCommandAndGetReply(NxtMessage);
        }
        private void WriteMailBoxOurMessage_Click(object sender, EventArgs e)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(textBox3.Text);
            byte datalength = Convert.ToByte(bytes.Length + 1);
            byte[] NxtHeader = { 0x00, 0x09, 0x00, datalength};
            byte[] endMessage = { 0x00 };
            var NxtMessage = new byte[NxtHeader.Length + bytes.Length + 1];
                NxtHeader.CopyTo(NxtMessage, 0);
                bytes.CopyTo(NxtMessage, NxtHeader.Length);
            NxtMessage[2] = (byte)(this.numericUpDownMailBoxNbr.Value - 1);

            Byte[] bytes2 = encoding.GetBytes(textBox5.Text);
            byte datalength2 = Convert.ToByte(bytes2.Length + 1);
            byte[] NxtHeader2 = { 0x00, 0x09, 0x00, datalength2 };
            byte[] endMessage2 = { 0x00 };
            var NxtMessage2 = new byte[NxtHeader2.Length + bytes2.Length + 1];
            NxtHeader2.CopyTo(NxtMessage2, 0);
            bytes2.CopyTo(NxtMessage2, NxtHeader2.Length);
            NxtMessage2[2] = (byte)(this.numericUpDownMailBoxNbr.Value - 1);

            NXTSendCommandAndGetReply(NxtMessage);
            //NXT2SendCommandAndGetReply(NxtMessage2);
        }
        
        private void WriteMailBoxInt_Click(object sender, EventArgs e)
        {
            byte[] NxtMessage = { 0x00, 0x09, 0x00, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00 };
            NxtMessage[2] = (byte)(this.numericUpDownMailBoxNbr.Value - 1);
            int tmp = (int)this.numericUpDownInt.Value;
            for (int ByteCtr = 0; ByteCtr <= 3; ByteCtr++)
            {
                NxtMessage[4 + ByteCtr] = (byte)tmp;
                tmp >>= 8;
            }
            NXTSendCommandAndGetReply(NxtMessage);
        }

        private void NXTSendCommandAndGetReply(byte[] Command)
        {
            
            Byte[] MessageLength= {0x00, 0x00};
            
            MessageLength[0]=(byte)Command.Length;
            this.textBox2.Text += "TX:";
            for (int i = 0; i < Command.Length; i++)
                this.textBox2.Text += Command[i].ToString("X2") + " ";
            this.textBox2.Text += Environment.NewLine;
            this.textBox2.Select(this.textBox2.Text.Length, 0);
            this.textBox2.ScrollToCaret();
            
            BluetoothConnection.Write(MessageLength, 0, MessageLength.Length);
            BluetoothConnection.Write(Command, 0, Command.Length);
            }

        private void NXT2SendCommandAndGetReply(byte[] Command)
        {

            Byte[] MessageLength = { 0x00, 0x00 };

            MessageLength[0] = (byte)Command.Length;
            this.textBox6.Text += "TX:";
            for (int i = 0; i < Command.Length; i++)
                this.textBox6.Text += Command[i].ToString("X2") + " ";
            this.textBox6.Text += Environment.NewLine;
            this.textBox6.Select(this.textBox2.Text.Length, 0);
            this.textBox6.ScrollToCaret();

            BluetoothConnection2.Write(MessageLength, 0, MessageLength.Length);
            BluetoothConnection2.Write(Command, 0, Command.Length);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            this.buttonConnect.Enabled = false;
            if (BluetoothConnection.IsOpen)
            {
                this.buttonGetInfo.Enabled = false;
                this.buttonGetVersion.Enabled = false;
                this.buttonReadMailbox.Enabled = false;
                this.buttonWriteMailboxBool.Enabled = false;
                this.buttonWriteMailBoxInt.Enabled = false;
                BluetoothConnection.Close();
                this.buttonConnect.Text = "Connect";
            }
            else
            {
                this.buttonConnect.Text = "Disconnect";
                this.BluetoothConnection.PortName = this.textBox1.Text.Trim();
                BluetoothConnection.Open();
                //this.BluetoothConnection2.PortName = this.textBox4.Text.Trim();
                //BluetoothConnection2.Open();
                BluetoothConnection.DataReceived += serialPort_DataReceived;
                BluetoothConnection2.DataReceived += serialPort2_DataReceived;
                this.buttonGetInfo.Enabled = true;
                this.buttonGetVersion.Enabled = true;
                this.buttonReadMailbox.Enabled = true;
                this.buttonWriteMailboxBool.Enabled = true;
                this.buttonWriteMailBoxInt.Enabled = true;
            }
            this.buttonConnect.Enabled = true;

        }

        void serialPort_DataReceived(object s, SerialDataReceivedEventArgs e)
        {
            int length = BluetoothConnection.ReadByte() + 256 * BluetoothConnection.ReadByte();

            // write out only the ACKs 
            for (int i = 0; i < length; i++)
            {
                if (i == 5)
                {
                    textBox2.Text += BluetoothConnection.ReadByte().ToString("X2") + " ";
                }
                else
                {
                    BluetoothConnection.ReadByte();
                }
            } 
            BluetoothConnection.DiscardInBuffer();
            this.textBox2.Text += Environment.NewLine;
            this.textBox2.Select(this.textBox2.Text.Length, 0);
            this.textBox2.ScrollToCaret();

        }

        void serialPort2_DataReceived(object s, SerialDataReceivedEventArgs e)
        {
            int length = BluetoothConnection2.ReadByte() + 256 * BluetoothConnection2.ReadByte();

            // write out only the ACKs
            for (int i = 0; i < length; i++)
            {
                if (i == 5)
                {
                    textBox6.Text += BluetoothConnection2.ReadByte().ToString("X2") + " ";
                }
                else
                {
                    BluetoothConnection2.ReadByte();
                }
            } 
            BluetoothConnection2.DiscardInBuffer();
            this.textBox6.Text += Environment.NewLine;
            this.textBox6.Select(this.textBox6.Text.Length, 0);
            this.textBox6.ScrollToCaret();

        }
    }
}