using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace A.I.NXT
{


    public partial class UserInterface : Form
    {
        Thread szal;


        double [] drawArray=new double [18];
        PictureBox[] picturbox = new PictureBox[5]; //pictureBox for balls
        double[,] cordinatsArray = new double[5, 2];
        FunctionControl functionControl = new FunctionControl();


        public UserInterface()
        {
            InitializeComponent();
        }
        private void Start_Button_Click(object sender, EventArgs e)
        {


            szal = new Thread(functionControl.Start);
            szal.Start();


          

            int corX = 0;
            int corY = 0;
            for (int i = 3; i < drawArray.Length; i++)
            {
                int k = 0;
                for (int j = 0; j < 2; j++)
                {
                    cordinatsArray[k, j] = drawArray[i];
                    i++;

                }
                i++;
                k++;
            }




            //pictureboxes for a balls in the map
            for (int i = 0; i < 5; i++)
            {



                picturbox[i] = new PictureBox();
                picturbox[i].Width = 10;
                picturbox[i].Height = 10;
                picturbox[i].Visible = true;
                picturbox[i].Image = new Bitmap("balll.png");
                picturbox[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.panelMap.Controls.Add(picturbox[i]);

                //array 
                for (int j = 0; j < 2; j++)
                {
                    if (j % 2 == 0)
                    {
                        corX = Convert.ToInt32(cordinatsArray[i, j]);
                    }
                    else { corY = Convert.ToInt32(cordinatsArray[i, j]); }
                }
                picturbox[i].Location = new Point(corX, corY);

            }

            //draw the baskett on a map
            PictureBox basket = new PictureBox();
            basket.Image = new Bitmap("basket.png");
            basket.Width = 20;
            basket.Height = 20;
            basket.Visible = true;
            basket.SizeMode = PictureBoxSizeMode.StretchImage;
            this.panelMap.Controls.Add(basket);
            


        }

      

        private void Abort_Button_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //functionControl = null;

            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }

        private void buttonConnection_Click_1(object sender, EventArgs e)
        {

            if (functionControl.connection() == true)
            {
                Start_Button.Enabled = true;
                picConRed.Visible = false;
                picConGreen.Visible = true;
                this.gyuszkoBox2.Text = "connected";
            }
            else { this.gyuszkoBox2.Text = "can't connect"; }
        }
    }
}