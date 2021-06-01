using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SingleLayerNNetwork
{
    class NNetwork
    {
        public int iterations = 1;
        //public Neuron[] neurons = new Neuron[5];
        public int[][] Weights = new int[35][];
        //public int[] yOut = new int[5]; //Выходной вектор Y

        public int[] w0 = new int[5];
        //public int[] s = new int[5];

        public NNetwork()
        {
            for (int c = 0; c < 5; c++)
            {
                //yOut[c] = new int [5];
                //w0[c] = 0;
                //s[c] = 0;
                //neurons[c] = new Neuron();
                /*
                for (int i = 0; i < 5; i++)
                {
                    if (i == c)
                        yOut[c][i] = 1;
                    else
                        yOut[c][i] = 0;                   
                }
                */
            }
            for (int i = 0; i < 35; i++)
            {
                Weights[i] = new int[5];
            }
        }

        public int Output(int data)
        {
            int y = 0;
            if (data >= 0)
                y = 1;
            return y;
        }


        public int Learn(int[][] trainData, int[][] tOut, bool bias = false)
        {
            while (true)
            {
                int errors = 0;
                for (int k = 0; k < 5; k++)
                {
                    int[] yOut = new int[5];
                    int[] s = new int[5];
                    for (int i = 0; i < 5; i++)
                    {
                        s[i] = 0;
                        yOut[i] = 0;
                    };
                    for (int j = 0; j < 5; j++)
                    {
                        for (int i = 0; i < 35; i++)
                        {
                            s[j] += Weights[i][j] * trainData[k][i];
                            yOut[j] = Output(s[j]);
                        };
                    };
                    for (int i = 0; i < 5; i++)
                    {
                        if (tOut[k][i] != yOut[i])
                            errors += 1;
                    };
                    for (int i = 0; i < 35; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            Weights[i][j] += trainData[k][i] * (tOut[k][j] - yOut[j]);
                        }
                    }

                }
                if (errors == 0 || iterations==100)
                    break;
                else
                    iterations += 1;
            }
            return iterations;
        }

        public double CalculateWeightUsage()
        {
            int zeroWeightsAmount = 0;
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 35; i++)
                {
                    if (Weights[i][j] == 0)
                        zeroWeightsAmount += 1;
                }
            }
            double percentage = zeroWeightsAmount*100/ 175;
            return percentage;
        }

        public int [][] Recognise(int[][] recogniseData)
        {
            int[][] result = new int[5][];
            for (int c = 0; c < 5; c++)
            {
                result[c] = new int[5];   
            }

            for (int c = 0; c < 5; c++)
            {
                int[] yOut = new int[5];
                for (int i = 0; i < 5; i++)
                {
                    yOut[i] = 0;
                };
                for (int j = 0; j < 5; j++)
                {                  
                    for (int i = 0; i < 35; i++)
                    {
                        yOut[j] += Weights[i][j] * recogniseData[c][i]; 
                        yOut[j] = Output(yOut[j]);
                        result[c][j] = yOut[j];
                    }
                }
            }

            return result;
        }
    }
}
