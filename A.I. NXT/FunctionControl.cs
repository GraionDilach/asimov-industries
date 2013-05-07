using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A.I.NXT
{
    class FunctionControl
    {
        double[,] arrayForDraw = new double[5, 2];
        GameStrategy gamestrategy;
        NxtControl control;
        PictureRecognition picturerecognition;
        double[] dataForGS = new double[18];
        int[] throwCord;
        public bool con = true;
        /// <summary>
        /// the function which sorted the picture recognitions array
        /// </summary>
        private void sortArray(double[] coordinates_balls_basket)
        {
            /// <summary>
            /// tempt 2 dimensional array
            /// </summary>
            double[,] cordinatsArray = new double[5, 2];


            int dataForGSPointer = 3;
            /// <summary>
            /// split the dataForGS array
            /// </summary>
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {


                    cordinatsArray[i, j] = dataForGS[dataForGSPointer];
                    dataForGSPointer++;
                }
                dataForGSPointer++;
            }




            /// <summary>
            /// segéd tömbök
            /// </summary>
            double[] hossz = new double[5];
            double kosX = dataForGS[0];
            double kosY = dataForGS[1];
            for (int i = 0; i < 5; i++)
            {

                double xcord = 0;
                double ycord = 0;
                double tav = 0;
                for (int j = 0; j < 2; j++)
                {
                    if (j % 2 == 0)
                    { xcord = cordinatsArray[i, j]; }
                    if (j % 2 == 1)
                    { ycord = cordinatsArray[i, j]; }
                }
                tav = ((xcord - kosX) * (xcord - kosX)) + ((ycord - kosY) * (ycord - kosY));
                hossz[i] = Math.Sqrt(tav);
            }
            int[] sorrend = new int[5] { 0, 1, 2, 3, 4 };

            

            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    int temp = 0;
                    if (hossz[sorrend[i]] < hossz[sorrend[j]])
                    {
                        temp = sorrend[i];
                        sorrend[i] = sorrend[j];
                        sorrend[j] = temp;
                    }
                }
            }


            /// <summary>
            /// dataForGS visszaalakítása
            /// </summary>
            dataForGSPointer = 3;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {

                    dataForGS[dataForGSPointer] = cordinatsArray[sorrend[i], j];
                    dataForGSPointer++;
                }
                dataForGS[dataForGSPointer] = 0;
                dataForGSPointer++;
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {

                    arrayForDraw[i,j] = cordinatsArray[sorrend[i], j];
                    
                }
                
            }
          
        }

        /// <summary>
        /// property function for U.I. map drawning
        /// </summary>
        public double[,] arrayForDrawn
        {
            get
            {
                return arrayForDraw;
            }
        }


        /// <summary>
        /// connecting the nxt, thats mean instantiates the control class
        /// </summary>

        public bool connection()
        {
            try
            {
                control = new NxtControl("Com11", "Com16");
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
            dataForGS = picturerecognition.getCoordinates();
            gamestrategy.Coordinates(dataForGS);
            gamestrategy.setCoordinates();
            throwCord = gamestrategy.Output();
            control.StartOperation(throwCord[0], throwCord[1], throwCord[2], throwCord[3]);
            // sort the cordinates array
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
                //System.Threading.Thread.Sleep(10000);
                control.StartOperation(throwCord[0], throwCord[1], throwCord[2], throwCord[3]);
                //control.MagnetControl(true);
                gamestrategy.returnToBasket();
                throwCord = gamestrategy.Output();
                //System.Threading.Thread.Sleep(10000);
                control.StartOperation(throwCord[0], throwCord[1], throwCord[2], throwCord[3]);
                // control.MagnetControl(false);

            }
            control.DisConnect();
        }
    }
}