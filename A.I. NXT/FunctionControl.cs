using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A.I.NXT
{
    class FunctionControl
    {
        GameStrategy gamestrategy;
        NxtControl control;
        PictureRecognition picturerecognition;
        double[] dataForGS = new double[18];
        public bool con = true;
        private int[] dataForControl;

      

        
        /// <summary>
        /// the function which sorted the picture recognitions array
        /// </summary>
        private void sortArray(double[] coordinates_balls_basket)
        {
            double[] basket_coordinates = new double[2];//coordinates of the basket, the first 2 members of the array
            double[] balls_coordinates = new double[10];//coordinates of the balls
            basket_coordinates[0] = coordinates_balls_basket[0];
            basket_coordinates[1] = coordinates_balls_basket[1];
            int m = 0;//control variable
            for (int n = 0; n < 5; n++)//fills the array that contains the coordinates of the balls
            {
                balls_coordinates[m] = coordinates_balls_basket[(n + 1) * 3];
                balls_coordinates[m + 1] = coordinates_balls_basket[(n + 1) * 3 + 1];
                m += 2;
            }
            double a;//temporary variables for the Pythagoras' theorem
            double b;
            double[] c = new double[5];//will contain the distance of the balls from the basket
            for (int n = 0; n < 10; n += 2)
            {
                a = Math.Abs(basket_coordinates[0] - balls_coordinates[n]);
                b = Math.Abs(basket_coordinates[1] - balls_coordinates[n + 1]);
                c[n / 2] = Math.Sqrt(a * a + b * b);

            }
            int temp;//temporary variable for the sorting
            int[] index = new int[5] { 0, 1, 2, 3, 4 };//vector of indexes
            for (int n = 0; n < 5; n++)
            {
                for (m = n + 1; m < 5; m++)
                {
                    if (c[index[n]] < c[index[m]])
                    {
                        temp = index[n];
                        index[n] = index[m];
                        index[m] = temp;
                    }
                }
            }
            double[] temp2 = coordinates_balls_basket; //temporary array to help replacing the original array with the sorted one
            for (int n = 0; n < 5; n++)
            {
                int n1 = (n + 1) * 3;//pointer for coordinates_balls_basket
                int n2 = (index[n] + 1) * 3;//pointer for temp2
                coordinates_balls_basket[n1] = temp2[n2];
                coordinates_balls_basket[n1 + 1] = temp2[n2 + 1];
                coordinates_balls_basket[n1 + 2] = temp2[n2 + 2];
            }
        }


        /// <summary>
        /// connecting the nxt, thats mean instantiates the control class
        /// </summary>

        public bool connection()
        {
            try
            {
                control = new NxtControl("", "");
            }
            catch (Exception )
            {
                con = false; 
            }
            return con;
        }

        /// <summary>
        /// the starting function
        /// </summary>

        public void Start()
        {
            picturerecognition = new PictureRecognition();
            gamestrategy = new GameStrategy();
            gamestrategy.setCoordinates();
            int[] throwCord = gamestrategy.Output();
            control.StartOperation(throwCord[0], throwCord[1], throwCord[2], throwCord[3]);
            picturerecognition = new PictureRecognition();
            dataForGS = picturerecognition.getCoordinates();
            //sort the cordinates array
            sortArray(dataForGS);
            gamestrategy.goToBasket();
            throwCord = gamestrategy.Output();
            control.StartOperation(throwCord[0], throwCord[1], throwCord[2], throwCord[3]);
            /// <summary>
            /// pick-up the balls, them go to basket
            /// </summary>
            for (int i = 0; i < 5; i++)
            {
                gamestrategy.goToBall();
                throwCord = gamestrategy.Output();
                control.StartOperation(throwCord[0], throwCord[1], throwCord[2], throwCord[3]);
                control.MagnetControl(true);

                gamestrategy.goToBasket();
                throwCord = gamestrategy.Output();
                control.StartOperation(throwCord[0], throwCord[1], throwCord[2], throwCord[3]);
                control.MagnetControl(false);
                
            }
            


        }

      

    }
}

