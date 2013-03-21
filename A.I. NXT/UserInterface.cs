﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace A.I.NXT
{
    public partial class UserInterface : Form
    {
        Button start;
        GameStrategy gamestrategy;
        NxtControl control;
        //PictureRecognition picturerecognition;
        public UserInterface()
        {
            InitializeComponent();
        }
        private void UserInterface_Load(object sender, EventArgs e)
        {
            gamestrategy = new GameStrategy();
            control = new NxtControl();
            //picturerecognition = new PictureRecognition();
            start = new Button();
            start.Text = "Connect to NXT devices";
            start.Size = new Size(200,30);
            start.Location = new Point(this.Width/2-105,20);
            start.Click += new System.EventHandler(start_Click);
            this.Controls.Add(start);
        }
        private void start_Click(Object sender, System.EventArgs e)
        {
            start.Visible = false;
            Label l = new Label();
            l.Text = "Connecting";
            l.Location = new Point(this.Width / 2 - l.Width / 2, 30);
            control = new NxtControl();
            if (control.activeConnectionOne() && control.activeConnectionTwo())
            {
                l.Text = "Tesztkoordináták elküldése a GameStrategy-nek";
                int x = Convert.ToInt32(toBasketX.Text);
                int y = Convert.ToInt32(toBasketY.Text);
                int z = Convert.ToInt32(toBasketZ.Text);
                int x2 = Convert.ToInt32(toBallX.Text);
                int y2 = Convert.ToInt32(toBallY.Text);
                int z2 = Convert.ToInt32(toBallZ.Text);

                int[] dataForGS = new int[18] { x, y, z, x2, y2, z2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                gamestrategy.setCoordinates(dataForGS);
            }
            else
            {
                MessageBox.Show("Hiba történt a kapcsolódásnál");
                start.Visible = true;
                l.Visible = false;
            }
        }

        private void toBasket_Click(object sender, EventArgs e)
        {
            int[] dataForControl = gamestrategy.goToBasket();
            bool movement = control.startOperation(dataForControl[0], dataForControl[1], dataForControl[2], dataForControl[3]);
            if (!movement) MessageBox.Show("Hiba történt a mozgatás során");
        }

        private void toBall_Click(object sender, EventArgs e)
        {
            int[] dataForControl = gamestrategy.goToBasket();
            bool movement = control.startOperation(dataForControl[0], dataForControl[1], dataForControl[2], dataForControl[3]);
            if (!movement) MessageBox.Show("Hiba történt a mozgatás során");
        }
    }
}