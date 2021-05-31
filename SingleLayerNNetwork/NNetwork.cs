using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SingleLayerNNetwork
{
    class NNetwork
    {
        public Neuron[] neurons = new Neuron[5];

        public int[][] yOut = new int[5][]; //Выходной вектор Y

        public NNetwork()
        {
            for (int c = 0; c < 5; c++)
            {
                yOut[c] = new int [5];
                neurons[c] = new Neuron();
                for (int i = 0; i < 5; i++)
                {
                    if (i == c)
                        yOut[c][i] = 1;
                    else
                        yOut[c][i] = 0;
                    for (int j = 0; j < 7; j++)
                    {
                        neurons[c].Weights[i][j] = 0;
                    }
                }
            }
        }

        public bool Learn(int[][] Matrix, int rightAnswer)
        {
            int max = 0;

            //выходной вектор T
            int[] tOut = new int[5]; 

            for (int i = 0; i < 5; i++)
            {
                if (i == rightAnswer)
                    tOut[i] = 1;
                else
                    tOut[i] = 0;

                if (neurons[i].Output(Matrix) > neurons[max].Output(Matrix))
                    max = i;
            }
            if (max == rightAnswer)
                return true;
            else
            {                
                for (int c = 0; c < 5; c++)
                {
                    if (c != rightAnswer)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            for (int j = 0; j < 7; j++)
                            {
                                //корректировка весов
                                neurons[c].Weights[i][j] += Matrix[c][i] * (yOut[c][i] - tOut[i]);
                            }
                                
                        }
                    }      
                }
                return false;
            }
        }
    }
}
