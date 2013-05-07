using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindOval
{
    class Muvelet
    {
        double xi1, xi2, x0, x1, yi1, yi2, y0, y1;

        public Muvelet()
        { }
        public void aEgyenes(int xi, int yi, int x0, int y0)
        {
            xi1 = xi;
            yi1 = yi;
            this.x0 = x0;
            this.y0 = y0;
        }
        public void bEgyenes(int xi, int yi, int x0, int y0)
        {
            xi2 = xi;
            yi2 = yi;
            this.x1 = x0;
            this.y1 = y0;
        }
        public int[] Metszespont()
        {
            double b1, b2, a1, a2, m1, m2;
            m1 = yi1 / xi1;
            m2 = yi2 / xi2;
            b1 = y0 - m1 * x0;
            b2 = y1 - m2 * x1;
            a1 = Math.Abs(b1) + Math.Abs(b2);
            a2 = m2 - m1;
            double X = a1 / a2;
            double Y = m1 * X + b1;
            int[] eredmeny = new int[2];
            eredmeny[0] = Convert.ToInt32(X);
            eredmeny[1] = Convert.ToInt32(Y);
            return eredmeny;
        }
       
    }
}
