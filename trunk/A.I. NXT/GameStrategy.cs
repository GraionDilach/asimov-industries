using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A.I.NXT
{
    class GameStrategy
    {
        double xk = 112.9;//pálya közepe x koordináta szerint
        double zk = 50.25;//pálya hossza z koordináta szerint
        double ym = 80;//robot magassága a tetejétől számítva
        double ykm = 40;//80 cm feletti rész
        double atlo, kh, fordulatszög;
        double menet = 2.5 * Math.PI; double rész;
        double[] labdakoo;
        double[,] adatok = new double[8, 4]; int k = 0; int i = 0; int j = 0;
        int[] tömb = new int[4];
        double[] különb = new double[4];
        public GameStrategy();
        public void setCoordinates()
        {
            atlo = (xk * xk) + (zk * zk);
            kh = atlo + (107 * 107);
            kh = Math.Sqrt(kh);
            for (int j = 0; j < 4; j++)
            {
                adatok[i, j] = kh;
            }
            i++;
            kezdoallapot();
        }
        public void kezdoallapot()
        {
            atlo = (xk * xk) + (zk * zk);
            kh = atlo + (ykm * ykm);
            kh = Math.Sqrt(kh);
            for (int j = 0; j < 4; j++)
            {
                adatok[i, j] = kh;
            }
            i++;
            különbség(i);

        }
        public void goToBasket()
        {
            double x, y, z;
            x = labdakoo[k];
            k++;
            y = labdakoo[k];
            k++;
            z = labdakoo[k];
            k++;

            adatok[i, 0] = M1(x, y, z);
            adatok[i, 1] = M2(x, y, z);
            adatok[i, 2] = M3(x, y, z);
            adatok[i, 3] = M4(x, y, z);
            i++;
            különbség(i);
        }
        public void goToBall();
        public void különbség(int i)
        {
            int a = 2;
            int b = 1;
            if (i == 5)
            {
                a = 3; b = 2;
            }
            if (i == 6)
            {
                a = 4; b = 3;
            }
            if (i == 7)
            {
                a = 5; b = 4;
            }
            for (int j = 0; j < különb.Length; j++)
            {
                különb[j] = adatok[i - a, j] - adatok[i - b, j];
            }
            tömb = Menet(különb);
        }
        public int[] Tömb()
        {
            return tömb;
        }
        public double M1(double x, double y, double z)
        {
            x -= 9.6;
            z -= 7.75;
            atlo = (x * x) + (z * z);
            kh = atlo + (ykm * ykm);
            kh = Math.Sqrt(kh);
            return kh;
        }
        public double M2(double x, double y, double z)
        {
            x += 9.6;
            z -= 7.75;
            atlo = (x * x) + (z * z);
            kh = atlo + (ykm * ykm);
            kh = Math.Sqrt(kh);
            return kh;
        }
        public double M3(double x, double y, double z)
        {
            x -= 9.6;
            z += 7.75;
            atlo = (x * x) + (z * z);
            kh = atlo + (ykm * ykm);
            kh = Math.Sqrt(kh);
            return kh;
        }
        public double M4(double x, double y, double z)
        {
            x += 9.6;
            z += 7.75;
            atlo = (x * x) + (z * z);
            kh = atlo + (ykm * ykm);
            kh = Math.Sqrt(kh);
            return kh;
        }
        public int[] Menet(double[] különb)//fordulatszám kiszámítása
        {
            int[] tömb = new int[4];
            for (int i = 0; i < különb.Length; i++)
            {
                rész = különb[i] / menet;
                fordulatszög = 360 * rész;
                tömb[i] = Convert.ToInt32(fordulatszög);
            }
            return tömb;
        }
    }
}
