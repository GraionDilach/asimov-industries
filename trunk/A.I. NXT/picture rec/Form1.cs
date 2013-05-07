using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Reflection;

using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using System.Drawing.Imaging;

namespace FindOval
{
    
    public partial class Form1 : Form
    {
        string file;
        IntRange R = new IntRange(70, 255);
        IntRange G = new IntRange(70, 255);
        IntRange B = new IntRange(70, 255);
        List<string> trapez=new List<string>();
        List<string> ball = new List<string>();
        Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ChangeImageDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    file = ChangeImageDialog.FileName;
                   image=new Bitmap(file);
                   pictureBoxImage.Image = image;
                 //  MessageBox.Show("Image loading Done!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                   // MessageBox.Show("Failed loading selected image file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void showLoadedImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxImage.Image = image;
        }
        private void ProcessImage(Bitmap bitmap)
        {
            IntRange RBlack=new IntRange(0, 150);
            // lock image
            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);

            // step 1 - turn background to black
            ColorFiltering colorFilter = new ColorFiltering();

           
            //Black Backround
            colorFilter.Red = R;
            colorFilter.Green = R;
            colorFilter.Blue = R;
            colorFilter.FillOutsideRange = false;

            colorFilter.ApplyInPlace(bitmapData);

            // step 2 - locating objects
            BlobCounter blobCounter = new BlobCounter();

            blobCounter.FilterBlobs = true;
            blobCounter.MinHeight = 20;
            blobCounter.MinWidth = 20;

            blobCounter.ProcessImage(bitmapData);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            bitmap.UnlockBits(bitmapData);

            // step 3 - check objects' type and highlight
            SimpleShapeChecker shapeChecker = new SimpleShapeChecker();

            Graphics g = Graphics.FromImage(bitmap);
            Pen yellowPen = new Pen(Color.Green, 5); // circles
            Pen redPen = new Pen(Color.Red, 5);       // quadrilateral
            Pen brownPen = new Pen(Color.Brown, 5);   // quadrilateral with known sub-type
            Pen greenPen = new Pen(Color.Green, 5);   // known triangle
            Pen bluePen = new Pen(Color.Blue, 5);     // triangle

            List<IntPoint> edgePoints=new List<IntPoint>();
            for (int i = 0, n = blobs.Length; i < n; i++)
            {
                edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);

                DoublePoint center;
                double radius;

                // is circle ?
                if (shapeChecker.IsCircle(edgePoints, out center, out radius))
                {
                    ball.Add(center.X + "/" + center.Y);
                    listBox1.Items.Add("X: "+Convert.ToInt32(center.X)+", Y: "+Convert.ToInt32(center.Y));
                    g.DrawEllipse(yellowPen,
                        (float)(center.X - radius), (float)(center.Y - radius),
                        (float)(radius * 2), (float)(radius * 2));
                }
                List<IntPoint> corners;
                
                if (shapeChecker.IsConvexPolygon(edgePoints, out corners))
                {
                    // get sub-type
                    PolygonSubType subType = shapeChecker.CheckPolygonSubType(corners);

                    Pen pen;

                    if (subType == PolygonSubType.Rectangle)
                    {
                        pen = (corners.Count == 4) ? greenPen : bluePen;
                        listBox1.Items.Add("Trapéz");
                        for (int j = 0; j < corners.Count; j++)
                        {
                            listBox1.Items.Add(corners[j]);
                            trapez.Add(corners[j].X+"/"+corners[j].Y);
                        }
                        
                        g.DrawPolygon(pen, ToPointsArray(corners));
                    }
                    

                    
                }
        
            }

            yellowPen.Dispose();
            redPen.Dispose();
            greenPen.Dispose();
            bluePen.Dispose();
            brownPen.Dispose();
            g.Dispose();

            // put new image to clipboard
            Clipboard.SetDataObject(bitmap);
            // and to picture box
            pictureBoxImage.Image = bitmap;
            
            //UpdatePictureBoxPosition();
        }


        // Update size and position of picture box control


        // Conver list of AForge.NET's points to array of .NET points
        private Point[] ToPointsArray(List<IntPoint> points)
        {
            Point[] array = new Point[points.Count];

            for (int i = 0, n = points.Count; i < n; i++)
            {
                array[i] = new Point(points[i].X, points[i].Y);
            }

            return array;
        }

        private void findBallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            ProcessImage(image);
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedIndex == 1)
            {
                R = new IntRange(200, 255);
                G = new IntRange(200, 255);
                B = new IntRange(200, 255);
            }
            else
            {
                R = new IntRange(0, 180);
                G = new IntRange(0, 180);
                B = new IntRange(0, 180);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            R = new IntRange(trackBar1.Value, trackBar2.Value);
            toolStripStatusLabel1.Text = Convert.ToString("Min: " + trackBar1.Value);
            toolStripStatusLabel2.Text = Convert.ToString("Max: " + trackBar2.Value);
        }

        private void reLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            image = new Bitmap(file);
            pictureBoxImage.Image = image;
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.Visible == false)
            {
                listBox1.Visible = true;
            }
            else
            {
                listBox1.Visible = false;
            }
        }

        private void kiszámítToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Muvelet muv = new Muvelet();
            int x1, x2, x3, x4, x5, x6, x7, x8;
            x1 = Convert.ToInt32(toolStripTextBox1.Text);
            x2 = Convert.ToInt32(toolStripTextBox2.Text);
            x3 = Convert.ToInt32(toolStripTextBox3.Text);
            x4 = Convert.ToInt32(toolStripTextBox4.Text);
            x5 = Convert.ToInt32(toolStripTextBox5.Text);
            x6 = Convert.ToInt32(toolStripTextBox6.Text);
            x7 = Convert.ToInt32(toolStripTextBox7.Text);
            x8 = Convert.ToInt32(toolStripTextBox8.Text);
            muv.aEgyenes(x1, x2, x3, x4);
            muv.bEgyenes(x5, x6, x7, x8);
            int[] koordinata = muv.Metszespont();
            MessageBox.Show("x= " + koordinata[0] + " y= " + koordinata[1]);
        }

        private void számítToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Muvelet muv = new Muvelet();
            string[] Ball = ball[0].Split('/');
            string[] A = trapez[0].Split('/');

        }

        private void pictureBoxImage_Click(object sender, EventArgs e)
        {

        }

        private void adatokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] a = new string[4];
            int db = 0;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i] == "Trapéz")
                {
                    i++;
                    a[db] = Convert.ToString(listBox1.Items[i]);
                    db++;

                }
            }

            String[] ass = a[0].Split(',');
            String[] ass1 = a[1].Split(',');
            String[] ass2 = a[2].Split(',');
            String[] ass3 = a[3].Split(',');

          double m1=Convert.ToInt32( ass1[0]) - Convert.ToInt32( ass[0]);
          double m2 = Convert.ToInt32(ass3[0]) - Convert.ToInt32(ass2[0]);
          
     
          string[] labda = ball[0].Split('X');
          string [] valoslabda = ball[0].Split('/');
          string[] valoslabda1 = ball[1].Split('/');
          string[] valoslabda2 = ball[2].Split('/');
          string[] valoslabda3= ball[3].Split('/');
          string[] valoslabda4 = ball[4].Split('/');
            //1labda
          double magassag = (Convert.ToDouble(valoslabda[0]) - Convert.ToDouble(ass[0])) / m1;
          

          double oldal = Convert.ToInt32(ass2[1]) - Convert.ToInt32(ass[1]); ;
          double o =( Convert.ToDouble(valoslabda[1]) - Convert.ToDouble(ass1[1]))/oldal;
          double vm = 236 * magassag;
          double c = o * 120;
          string l1 = vm + ", " + c;
            //2labda
          
           magassag = (Convert.ToDouble(valoslabda1[0]) - Convert.ToDouble(ass[0])) / m1;
           vm = 236 * magassag;

           oldal = Convert.ToInt32(ass2[1]) - Convert.ToInt32(ass[1]); ;
           o = (Convert.ToDouble(valoslabda1[1]) - Convert.ToDouble(ass1[1])) / oldal;
           c = o * 120;
          string l2 = vm + ", " + c;
            
          //3labda

          magassag = (Convert.ToDouble(valoslabda2[0]) - Convert.ToDouble(ass[0])) / m1;
          

          oldal = Convert.ToInt32(ass2[1]) - Convert.ToInt32(ass[1]); ;
          o = (Convert.ToDouble(valoslabda2[1]) - Convert.ToDouble(ass1[1])) / oldal;
          c = o * 120;
          vm = 236 * magassag;
          string l3 = vm + ", " + c;
          //4labda

          magassag = (Convert.ToDouble(valoslabda3[0]) - Convert.ToDouble(ass[0])) / m1;
          vm = 236 * magassag;

          oldal = Convert.ToInt32(ass2[1]) - Convert.ToInt32(ass[1]); ;
          o = (Convert.ToDouble(valoslabda3[1]) - Convert.ToDouble(ass1[1])) / oldal;
          c = o * 120;
          string l4 = vm + ", " + c;
          //5labda

          magassag = (Convert.ToDouble(valoslabda4[0]) - Convert.ToDouble(ass[0])) / m1;
          vm = 236 * magassag;

          oldal = Convert.ToInt32(ass2[1]) - Convert.ToInt32(ass[1]); ;
          o = (Convert.ToDouble(valoslabda4[1]) - Convert.ToDouble(ass1[1])) / oldal;
          c = o * 120;
          string l5 = vm + ",  " + c;

          MessageBox.Show(l1 + " \n" + l2 + " \n" + l3 + " \n" + l4 + " \n" + l5);
         

        }

    }
}
