using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A.I.NXT
{
    class GameStrategy
    {
        double coordinate_x = 116.9;///<coordinat_x is distance between robot and side of the field,robot is int the midst of the area
        double coordinate_y = 53.25;///<coodinate_y is distance between front of the robot and front of the field
        //double coordinate_z = 40;///<coordinate_z is height from top of the robot to base
        double coordinate_zz = 60;///<coordinate_zz distance between top of  the robot and field(120cm)
        double diagonal_square;///<diagonal_square is sum square of coordinates_x and coordinates_y
        double length_of_the_cord;///<length_of_the_cord is required distance to coordinates
        double rev_as_angle;///<this variable is rev as angle of the motor
        double length_per_rotation = (2.39/2) * Math.PI;///<the amount of cord changing within a full rotation
        double rev_amount;///<this variable is rev as number of the motor
        double[] coordinates_balls_basket;///<coordinates_ball_basket contain coordinates from PictureRecognition
        double[,] buffer_calculated_cordlengths = new double[8, 4];///<buffered previously calculated cordlengths for comparison
        int k = 0; int i = 0;///<pointers
        int[] output_to_nxtcontrol = new int[4];///<amount of rotations for the next move
        double[] difference = new double[4];///<difference between current and previous status of cords
        double[] M_count = new double[] { 52, 52, 52, 52 };///<actual count of rotation
        bool init_rev;

        public GameStrategy();
        /// <summary>
        /// This function gets the coordinates of the balls and the basket.(Ez a függvény megkapja a labdák és a kosár koordinátáit.)
        /// </summary>
        /// <param name="coordinates_balls_basket"></param>
        public void Coordinates(double [] coordinates_balls_basket)
        {
            this.coordinates_balls_basket = coordinates_balls_basket;
        }
        /// <summary>
        /// Centers the robot and calls the initial stage function.(alap)
        /// </summary>
		
        public void setCoordinates()
        {
            diagonal_square = (coordinate_x * coordinate_x) + (coordinate_y * coordinate_y);
            length_of_the_cord = diagonal_square + (104 * 104);
            length_of_the_cord = Math.Sqrt(length_of_the_cord);            
            for (int j = 0; j < 4; j++)
            {
                buffer_calculated_cordlengths[i, j] = length_of_the_cord;
            }
            i++;
           initial_state();
        }

        /// <summary>
        /// This function sets the initital state of the robot by positioning it to 40 cm high.()
        /// </summary>
   
        public void initial_state()
        {
            diagonal_square = (coordinate_x * coordinate_x) + (coordinate_y * coordinate_y);            
            length_of_the_cord = diagonal_square + (coordinate_zz * coordinate_zz);
            length_of_the_cord = Math.Sqrt(length_of_the_cord);           
            for (int j = 0; j < 4; j++)
            {
                buffer_calculated_cordlengths[i, j] = length_of_the_cord;
            }
            i++;
            Difference(i);

        }
        /// <summary>
        /// Calculates distance between the basket and initial state.(goToBasket)
        /// </summary>
        
        public void goToBasket()
        {
            double x, y, z;
            x = coordinates_balls_basket[k];
            k++;
            y = coordinates_balls_basket[k];
            k++;
            z = coordinates_balls_basket[k];
            k++;

            buffer_calculated_cordlengths[i, 0] = M1(x, y, z);
            buffer_calculated_cordlengths[i, 1] = M2(x, y, z);
            buffer_calculated_cordlengths[i, 2] = M3(x, y, z);
            buffer_calculated_cordlengths[i, 3] = M4(x, y, z);
            i++;
            Difference(i);
        }

        /// <summary>
        /// This function calculates the distance between the ball and the basket.(gotoball)
        /// </summary>

        public void goToBall()
        {
            double x, y, z;
            x = coordinates_balls_basket[k];
            k++;
            y = coordinates_balls_basket[k];
            k++;
            z = coordinates_balls_basket[k];
            k++;
            buffer_calculated_cordlengths[i, 0] = M1(x, y, z);
            buffer_calculated_cordlengths[i, 1] = M2(x, y, z);
            buffer_calculated_cordlengths[i, 2] = M3(x, y, z);
            buffer_calculated_cordlengths[i, 3] = M4(x, y, z);           
            i++;
            Difference(i);
        }
        public int[] returnToBasket()
        {
            for (int j = 0; j < output_to_nxtcontrol.Length; j++)
            {
                output_to_nxtcontrol[j] = output_to_nxtcontrol[j] * (-1);
            }
            return output_to_nxtcontrol;
        }
        /// <summary>
        /// This function calculates difference between the previous and the current status.
        /// </summary>
        /// <param name="i"></param>
        public void Difference(int i)
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
            for (int j = 0; j < difference.Length; j++)
            {
                difference[j] = buffer_calculated_cordlengths[i - a, j] - buffer_calculated_cordlengths[i - b, j];
            }
            Revolutions_as_angle(difference);
        }
        /// <summary>
        /// This returns the values for the NXT controls within an array.
        /// </summary>
        /// <returns></returns>
        public int[] Output()
        {
            return output_to_nxtcontrol;
        }
        /// <summary>
        /// Calculating M1 motor cord length.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>cord length </returns>
        public double M1(double x, double y, double z)
        {
            x -= 9.6;
            y -= 7.75;
            diagonal_square = (x * x) + (y * y);
            length_of_the_cord = diagonal_square + (coordinate_zz * coordinate_zz);
            length_of_the_cord = Math.Sqrt(length_of_the_cord);
            return length_of_the_cord;
        }
        /// <summary>
        /// Calculating M2 motor cord length.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>cord length </returns>
        public double M2(double x, double y, double z)
        {
            x += 9.6;
            y -= 7.75;
            diagonal_square = (x * x) + (y * y);
            length_of_the_cord = diagonal_square + (coordinate_zz * coordinate_zz);
            length_of_the_cord = Math.Sqrt(length_of_the_cord);
            return length_of_the_cord;
        }
        /// <summary>
        /// Calculating M3 motor cord length.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>cord length </returns>
		
        public double M3(double x, double y, double z)
        {
            x -= 9.6;
            y += 7.75;
            diagonal_square = (x * x) + (y * y);
            length_of_the_cord = diagonal_square + (coordinate_zz * coordinate_zz);
            length_of_the_cord = Math.Sqrt(length_of_the_cord);
            return length_of_the_cord;
        }
        /// <summary>
        /// Calculating M4 motor cord length.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>cord length </returns>
        public double M4(double x, double y, double z)
        {
            x += 9.6;
            y += 7.75;
            diagonal_square = (x * x) + (y * y);
            length_of_the_cord = diagonal_square + (coordinate_zz * coordinate_zz);
            length_of_the_cord = Math.Sqrt(length_of_the_cord);
            return length_of_the_cord;
        }

        /// <summary>
        ///This function converts revolutions into angle.(Revolutions_as_angle)
        /// </summary>
        /// <param name="difference"></param>


        public void Revolutions_as_angle(double[] difference)
        {
            for (int i = 0; i < difference.Length; i++)
            { 
           
                //rev_amount = difference[i] / length_per_rotation;
                //rev_as_angle = 360 * rev_amount;
                //output_to_nxtcontrol[i] = Convert.ToInt32(rev_as_angle);
                if (init_rev)
                {
                    length_per_rotation = 2.5 * Math.PI;
                }
                if (M_count[i] < 75 && M_count[i] > 60)
                {
                    length_per_rotation = (2.38 - 0.08) * Math.PI;
                }
                if (M_count[i] < 60 && M_count[i] > 45)
                {
                    length_per_rotation = (2.38 - 0.16) * Math.PI;
                }
                if (M_count[i] < 45 && M_count[i] > 30)
                {
                    length_per_rotation = (2.38 - 0.24) * Math.PI;
                }
                if (M_count[i] < 30 && M_count[i] > 15)
                {
                    length_per_rotation = (2.38 - 0.32) * Math.PI;
                }
                if (M_count[i] < 15 && M_count[i] > 0)
                {
                    length_per_rotation = (2.38 - 0.40) * Math.PI;
                }
                rev_amount = difference[i] / length_per_rotation;
                if (rev_amount < 0)
                {
                    M_count[i] = M_count[i] - rev_amount;
                }
                if (rev_amount >= 0)
                {
                    M_count[i] = M_count[i] + rev_amount;
                }
                rev_as_angle = 360 * rev_amount;
                output_to_nxtcontrol[i] = Convert.ToInt32(rev_as_angle);
            }
            init_rev = false;
           
        }
    }
}
